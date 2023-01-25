using System.Text;
using ChargeShare.UserService.Controllers;
using ChargeShare.UserService.DAL.Configurations;
using ChargeShare.UserService.DAL.Contexts;
using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.DAL.Repositories;
using ChargeShare.UserService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var jwtConfig = configuration.GetSection("JWT");
var secretKey = jwtConfig["secret"];
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddIdentity<ChargeSharedUserModel, IdentityRole<int>>().AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<UserContext>(options =>
{
    /*options.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=peopledb3; Trusted_Connection=True; trustServerCertificate=true");*/
    options.UseSqlServer("Server=.; Database=peopledb3; Trusted_Connection=True; trustServerCertificate=true");
});

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("post", policy =>
    {
        policy.AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.Configure<IdentityOptions>(opts =>
{
    //No requirements for password (pls only use for testing purposes)
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequiredLength = 1;
    opts.Password.RequiredUniqueChars = 0;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["validIssuer"],
        ValidAudience = jwtConfig["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our api...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/api/**")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapControllers();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("post");

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.Run();