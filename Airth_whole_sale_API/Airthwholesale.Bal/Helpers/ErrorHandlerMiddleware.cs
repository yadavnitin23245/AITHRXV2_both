using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

      //  private readonly IexceptionHandlingLogic _IexceptionHandling;

        private readonly IServiceProvider _serviceProvider;

        //Middleware is always a singleton so you can't have scoped dependencies as constructor dependencies in the constructor of your middleware.

        public ErrorHandlerMiddleware(RequestDelegate next , IServiceProvider serviceProvider)
        {
            _next = next;
          //  _IexceptionHandling = IexceptionHandling;
            this._serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {


            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }


                JDPExceptionLoggingToDataBase objexception = new JDPExceptionLoggingToDataBase();
                objexception.ExceptionMsg = error.Message;
                objexception.ExceptionType = error.InnerException!=null?error.InnerException.ToString():"";
                objexception.ExceptionSource = error.StackTrace;
                objexception.ExceptionURL = error.Message;
                objexception.Logdate = DateTime.Now;

                //   _IexceptionHandling.InsertException(objexception);


                //Another way to get the instance of scoped dependency is to inject service provider (IServiceProvider) into the middleware constructor,
                //create scope in Invoke method and then get the required service from the scope:

                using (var dbcontext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AirthwholesaleDbContext>())
                {
                    if (dbcontext != null)
                    {

                        try
                        {

                            await dbcontext.JDPExceptionLoggingToDataBase.AddAsync(objexception);

                            await dbcontext.SaveChangesAsync();
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message});
                await response.WriteAsync(result);
            }
        }
    }
}
