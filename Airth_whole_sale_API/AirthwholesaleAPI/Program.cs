using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Bal.Logic;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Authorization;
using AirthwholesaleAPI.Controllers;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.WebHost.ConfigureKestrel(c =>
{
    c.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(15);
});

builder.Services.AddControllers()
        .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddCors();


//gettiing Jwt Authentication key from Appsetting
var key = Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JWT_Secret"].ToString());

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


//builder.Services.AddAuthentication(IISServerDefaults.AuthenticationScheme);

builder.Services.AddDbContext<AirthwholesaleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))).
    AddDbContext<JDPAPIDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("JDPapiconnection")));  //added two connection string




//Here I setup to read appsettings with DTO AppSetting now we can discard helper models.
builder.Services.Configure<AppSettingsDTO>(builder.Configuration.GetSection("AppSettings"));

builder.Services.Configure<RemovetextDTOConfig>(builder.Configuration.GetSection("RemovetextConfig"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//hangfire configuration

// Add Hangfire services.  

#region Add Dependency For Repository

//using for Identity
//builder.Services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordPolicy>();
//builder.Services.AddTransient<IUserValidator<AppUser>, CustomUsernameEmailPolicy>();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AirthwholesaleDbContext>().AddDefaultTokenProviders();

// Activation link for DTO
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<UserDTO>, Repository<UserDTO>>();
builder.Services.AddScoped<IRepository<CountryDTO>, Repository<CountryDTO>>();
builder.Services.AddScoped<IRepository<StateDTO>, Repository<StateDTO>>();
builder.Services.AddScoped<IRepository<CityDTO>, Repository<CityDTO>>();
builder.Services.AddScoped<IRepository<ResetPasswordDTO>, Repository<ResetPasswordDTO>>();
builder.Services.AddScoped<IRepository<iccbatchDTO>, Repository<iccbatchDTO>>();
builder.Services.AddScoped<IRepository<IICCBatchApiDTO>, Repository<IICCBatchApiDTO>>();
builder.Services.AddScoped<IRepository<iccApiResponsePageWiseDTO>, Repository<iccApiResponsePageWiseDTO>>();
builder.Services.AddScoped<IRepository<JDPVehicleInfoDTO>, Repository<JDPVehicleInfoDTO>>();
builder.Services.AddScoped<IRepository<JDPExtendedDescriptionsDTO>, Repository<JDPExtendedDescriptionsDTO>>();
builder.Services.AddScoped<IRepository<JDPListOfAppliedOffersDTO>, Repository<JDPListOfAppliedOffersDTO>>();
builder.Services.AddScoped<IRepository<JDPAPICallHistoryDTO>, Repository<JDPAPICallHistoryDTO>>();
builder.Services.AddScoped<IRepository<JDPListOfPhotosDTO>, Repository<JDPListOfPhotosDTO>>();
builder.Services.AddScoped<IRepository<JDPVehicleCommentsDTO>, Repository<JDPVehicleCommentsDTO>>();
builder.Services.AddScoped<IRepository<JDPDealerInfoDTO>, Repository<JDPDealerInfoDTO>>();
builder.Services.AddScoped<IRepository<JDPListOfOptionsDTO>, Repository<JDPListOfOptionsDTO>>();
builder.Services.AddScoped<IRepository<JDPSubOptionsDTO>, Repository<JDPSubOptionsDTO>>();
builder.Services.AddScoped<IRepository<JDPPremiumOptionsDTO>, Repository<JDPPremiumOptionsDTO>>();
builder.Services.AddScoped<IRepository<MapDetailDTO>, Repository<MapDetailDTO>>();
builder.Services.AddScoped<IRepository<JDPSearchDTO>, Repository<JDPSearchDTO>>();
builder.Services.AddScoped<IRepository<JDPPricingDTO>, Repository<JDPPricingDTO>>();
builder.Services.AddScoped<IRepository<JDPSoldVehiclesParametersDTO>, Repository<JDPSoldVehiclesParametersDTO>>();
builder.Services.AddScoped<IRepository<JDPSoldVehicleListDTO>, Repository<JDPSoldVehicleListDTO>>();
builder.Services.AddScoped<IRepository<AppUserDTO>, Repository<AppUserDTO>>();
builder.Services.AddScoped<IRepository<JDPZStoreValuesDTO>, Repository<JDPZStoreValuesDTO>>();
builder.Services.AddScoped<IRepository<JDPAPIKeyValuesDTO>, Repository<JDPAPIKeyValuesDTO>>();
builder.Services.AddScoped<IRepository<JDPCBBAPIResponseDTO>, Repository<JDPCBBAPIResponseDTO>>();
builder.Services.AddScoped<IRepository<CBBPricingAPIDetailDTO>, Repository<CBBPricingAPIDetailDTO>>();
builder.Services.AddScoped<IRepository<JDPCBBPricingDetailForEmailByDealerNamesDTO>, Repository<JDPCBBPricingDetailForEmailByDealerNamesDTO>>();

builder.Services.AddScoped<IRepository<CleanImagesDetailsDTO>, Repository<CleanImagesDetailsDTO>>();

builder.Services.AddScoped<IRepository<DGroup>, Repository<DGroup>>();
builder.Services.AddScoped<IRepository<DGroupDTO>, Repository<DGroupDTO>>();

//builder.Services.AddScoped<IRepository<AspNetRoles>, Repository<AspNetRoles>>();
//builder.Services.AddScoped<IRepository<AspNetRolesDTO>, Repository<AspNetRolesDTO>>();

//builder.Services.AddScoped<IRepository<AspNetUsers>, Repository<AspNetUsers>>();
//builder.Services.AddScoped<IRepository<AspNetUsersDTO>, Repository<AspNetUsersDTO>>();

builder.Services.AddScoped<IRepository<DealersList>, Repository<DealersList>>();
builder.Services.AddScoped<IRepository<DealersListDTO>, Repository<DealersListDTO>>();

builder.Services.AddScoped<IRepository<Subscription>, Repository<Subscription>>();
builder.Services.AddScoped<IRepository<SubscriptionDTO>, Repository<SubscriptionDTO>>();

builder.Services.AddScoped<IRepository<AithrPositions>, Repository<AithrPositions>>();
builder.Services.AddScoped<IRepository<AithrPositionsDTO>, Repository<AithrPositionsDTO>>();

// Activation link for models
builder.Services.AddScoped<IRepository<Address>, Repository<Address>>();
builder.Services.AddScoped<IRepository<BusinessInfo>, Repository<BusinessInfo>>();
builder.Services.AddScoped<IRepository<AppUser>, Repository<AppUser>>();
builder.Services.AddScoped<IRepository<JDPVehicleInfo>, Repository<JDPVehicleInfo>>();
builder.Services.AddScoped<IRepository<JDPExtendedDescriptions>, Repository<JDPExtendedDescriptions>>();
builder.Services.AddScoped<IRepository<JDPListOfAppliedOffers>, Repository<JDPListOfAppliedOffers>>();
builder.Services.AddScoped<IRepository<JDPAPICallHistory>, Repository<JDPAPICallHistory>>();
builder.Services.AddScoped<IRepository<JDPListOfPhotos>, Repository<JDPListOfPhotos>>();
builder.Services.AddScoped<IRepository<JDPVehicleComments>, Repository<JDPVehicleComments>>();
builder.Services.AddScoped<IRepository<JDPDealerInfo>, Repository<JDPDealerInfo>>();

builder.Services.AddScoped<IRepository<JDPSubOptions>, Repository<JDPSubOptions>>();
builder.Services.AddScoped<IRepository<JDPPremiumOptions>, Repository<JDPPremiumOptions>>();
builder.Services.AddScoped<IRepository<JDPListOfOptions>, Repository<JDPListOfOptions>>();
builder.Services.AddScoped<IRepository<JDPPricing>, Repository<JDPPricing>>();

builder.Services.AddScoped<IRepository<JDPExceptionLoggingToDataBase>, Repository<JDPExceptionLoggingToDataBase>>();
builder.Services.AddScoped<IRepository<CBBPricing>, Repository<CBBPricing>>();
builder.Services.AddScoped<IRepository<CBBPricingAPIDetail>, Repository<CBBPricingAPIDetail>>();

builder.Services.AddScoped<IRepository<JDPPhotoCleanedDetails>, Repository<JDPPhotoCleanedDetails>>();

builder.Services.AddScoped<IRepository<DashboardFilesDetailsViewModelDTO>, Repository<DashboardFilesDetailsViewModelDTO>>();


builder.Services.AddScoped<IRepository<DGroup>, Repository<DGroup>>();
// builder.Services.AddScoped<IRepository<AspNetRoles>, Repository<AspNetRoles>>();

// builder.Services.AddScoped<IRepository<AspNetUsers>, Repository<AspNetUsers>>();
builder.Services.AddScoped<IRepository<DealersList>, Repository<DealersList>>();
//created another repository for access the table from other database or connection string

//builder.Services.AddScoped<IJDPRepository<JDPVehicleInfo>, JDPRepository<JDPVehicleInfo>>();
//builder.Services.AddScoped<IJDPRepository<JDPExtendedDescriptions>, JDPRepository<JDPExtendedDescriptions>>();
//builder.Services.AddScoped<IJDPRepository<JDPListOfAppliedOffers>, JDPRepository<JDPListOfAppliedOffers>>();
//builder.Services.AddScoped<IJDPRepository<JDPAPICallHistory>, JDPRepository<JDPAPICallHistory>>();
//builder.Services.AddScoped<IJDPRepository<JDPListOfPhotos>, JDPRepository<JDPListOfPhotos>>();
//builder.Services.AddScoped<IJDPRepository<JDPVehicleComments>, JDPRepository<JDPVehicleComments>>();
//builder.Services.AddScoped<IJDPRepository<JDPDealerInfo>, JDPRepository<JDPDealerInfo>>();

//builder.Services.AddScoped<IJDPRepository<JDPSubOptions>, JDPRepository<JDPSubOptions>>();
//builder.Services.AddScoped<IJDPRepository<JDPPremiumOptions>, JDPRepository<JDPPremiumOptions>>();
//builder.Services.AddScoped<IJDPRepository<JDPListOfOptions>, JDPRepository<JDPListOfOptions>>();
//builder.Services.AddScoped<IJDPRepository<JDPPricing>, JDPRepository<JDPPricing>>();

// configure DI for application services







//builder.Services.AddTransient<IJobTestService, JobTestService>(x => new JobTestService().ReccuringJob(),TimeSpan.FromSeconds(10));

//RecurringJob.AddOrUpdate(() => homeCtrl.SendEmail(), Cron.Daily(hour, minute), TimeZoneInfo.Local);

//var manager = new RecurringJobManager();
//manager.AddOrUpdate("some-id", Job.FromExpression(() => Method()), Cron.Yearly());


#endregion

#region Add Dependency For Services
builder.Services.AddTransient<IUserLogic, UserLogic>();
builder.Services.AddTransient<ICommonLogic, CommonLogic>();
builder.Services.AddTransient<IStockVehiclesUpdatesLogic, StockVehiclesUpdatesLogic>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<IICCBatchLogic, ICCBatchLogic>();
builder.Services.AddTransient<IApiHistoryResponseLogic, Apihistorylogic>();
builder.Services.AddTransient<ISoldVehiclesLogic, SoldVehiclesLogic>();

builder.Services.AddTransient<IexceptionHandlingLogic, exceptionHandlingLogic>();


builder.Services.AddTransient<ISoldVehiclesLogic, SoldVehiclesLogic>();

builder.Services.AddTransient<IRemoveTextDashbaordLogic, RemoveTextDashbaordLogic>();

builder.Services.AddTransient<IAdminLogic, AdminLogic>();

//builder.Services.AddTransient<IJobTestService, JobTestService>();

//builder.Services.AddTransient<IJobTestService, JobTestService>(xc => new JobTestService().ReccuringJob(),TimeSpan.FromSeconds(10));

#endregion


#region Hangfirejob setting


//builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddHangfire(config =>
//            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//            .UseSimpleAssemblyNameTypeSerializer()
//            .UseRecommendedSerializerSettings()
//            .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new Hangfire.SqlServer.SqlServerStorageOptions
//            {
//                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//                QueuePollInterval = TimeSpan.Zero,
//                UseRecommendedIsolationLevel = true,
//                DisableGlobalLocks = true
//            }));



//builder.Services.AddHangfireServer();

//var sqlStorage = new SqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
//var options = new BackgroundJobServerOptions
//{
//    ServerName = "Test Server"
//};
//JobStorage.Current = sqlStorage;


//JDPSoldVehiclesParametersDTO objvehiclepara = new JDPSoldVehiclesParametersDTO();

//IICCBatchApiDTO Obj = new IICCBatchApiDTO();

////InvalidOperationException " JobStorage.Current property value has not been initialized"
//var storage = JobStorage.Current;


//RecurringJob.AddOrUpdate<ICCBatchApiController>(service => service.GetAllVehiclesInformationAllPages(Obj,"HangFire"), Cron.Daily(4, 59), TimeZoneInfo.Utc);



#endregion

// end code to add
// Add services to the container.

builder.Services.AddMvc();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseCustomMiddleware();
//app.UseMiddleware<ErrorHandlerMiddleware>();
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

// start code to add
// to get ip address
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


app.UseMiddleware<JwtMiddleware>();

app.UseMiddleware<ResponseTimeMiddleware>();

app.UseMiddleware<ErrorHandlerMiddleware>();

// app.UseHangfireDashboard("/dashboard");
//app.UseHangfireServer();

//app.UseHangfireDashboard("/hangfire", new DashboardOptions
//{
//    DashboardTitle = "Sample Jobs",
//    Authorization = new[]
//            {
//                new  HangfireAuthorizationFilter("admin")
//            }
//});

app.UseEndpoints(endpoints =>
{
   //endpoints.MapControllers();
   // endpoints.MapHangfireDashboard();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));
//using (var server = new BackgroundJobServer())
//{

//    Console.ReadLine();
//}
