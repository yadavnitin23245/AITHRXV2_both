using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;

namespace AirthwholesaleAPI.Authorization
{
    public class ResponseTimeMiddleware
    {
        private readonly IServiceProvider _serviceProvider;
        // Name of the Response Header, Custom Headers starts with "X-"  
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";
        // Handle to the next Middleware in the pipeline  
        private readonly RequestDelegate _next;
        public ResponseTimeMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;


        }
        public Task InvokeAsync(HttpContext context, IApiHistoryResponseLogic IApiResponseLogic)
        {
            JDPAPICallHistory apihistoryobj = new JDPAPICallHistory();
            var clientIP = GetClientIPAddress(context);
            // Start the Timer using Stopwatch  
            var watch = new Stopwatch();
            watch.Start();

            apihistoryobj.InitiatedTimeStamp = DateTime.Now;

            context.Response.OnStarting(async () => {
                // Stop the timer information and calculate the time   
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                // Add the Response time information in the Response headers.   
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                apihistoryobj.CompletedTimeStamp = DateTime.Now;
                apihistoryobj.APIName = context.Request.Path.Value;
                apihistoryobj.Status = Task.CompletedTask.IsCompleted.ToString();
                apihistoryobj.InitiatedFromIP = clientIP;
                //Another way to get the instance of scoped dependency is to inject service provider (IServiceProvider) into the middleware constructor,
                //create scope in Invoke method and then get the required service from the scope:
                using (var dbcontext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AirthwholesaleDbContext>())
                {
                    if (dbcontext != null)
                    {

                        try
                        {

                            await dbcontext.JDPAPICallHistory.AddAsync(apihistoryobj);
                            await dbcontext.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            });
            // Call the next delegate/middleware in the pipeline   
            return this._next(context);
        }

        public static string GetClientIPAddress(HttpContext context)
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
            {
                ip = context.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
