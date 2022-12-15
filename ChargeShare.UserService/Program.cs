using ChargeShare.UserService.Controllers;
using ChargeShare.UserService.DAL.Configurations;
using ChargeShare.UserService.DAL.Contexts;
using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.DAL.Repositories;
using ChargeShare.UserService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddIdentity<ChargeSharedUserModel, IdentityRole<int>>().AddEntityFrameworkStores<UserContext>();

builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=peopledb3; Trusted_Connection=True; trustServerCertificate=true");
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