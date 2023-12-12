using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.IOC.Extensions;
using System.Text;
using NLog.Web;
using Microsoft.OpenApi.Models;
using WebApi.Filters;
using WebApi.ActionResults;
using Domain.Configurations;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
using FluentAssertions.Common;
using System.Reflection;
using WebApi.Middleware;
using Ganss.Xss;
using Serilog;

//var myAllowSpecialOrigins = "_myAllowSpecificOrigins";
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
}).UseNLog();




// Add Database
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("ApplicationContext"));builder.Services.AddMemoryCache();


// Add Email Configurations
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddInfrastructure(builder.Configuration)
    .AddCors();
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
   o.TokenLifespan = TimeSpan.FromHours(3));



builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IHtmlSanitizer>(_ =>
{
    var sanitizer = new HtmlSanitizer();
    
    return sanitizer;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = builder.Configuration["JwtTokenSettings:TokenIssuer"],
        //ValidAudience = builder.Configuration["JwtTokenSettings:TokenIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenSettings:TokenKey"]))
    };
    options.RequireHttpsMetadata = false;
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));
builder.Services.AddMvc(options =>
{
    options.Filters.Add<RequestLoggingFilter>();
    options.Filters.Add<HttpGlobalExceptionFilter>();

})
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context => new ValidationFailedResult(context.ModelState);
});

builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TROO-X API",
        Version = "v1",
        Description = "TROO-X API"
    });
    c.OperationFilter<SwaggerHeaderFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    c.CustomSchemaIds(x => x.FullName);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TROO-X v1");
    c.RoutePrefix = string.Empty;
});

app.UseMiddleware<SanitizationMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();
