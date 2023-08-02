global using Delamain_backend.Services;
global using Delamain_backend.Data;
global using Delamain_backend.Models;
global using Microsoft.EntityFrameworkCore;
global using Delamain_backend.Hubs;
global using Microsoft.AspNetCore.Mvc;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.SignalR;
global using Delamain_backend.Controllers;
global using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Newtonsoft.Json;
using Delamain_backend.Services.userRequestService;
using Delamain_backend.Services.LoginService;
using Delamain_backend.Services.QueueWorkerInterface;
using Delamain_backend.Services.emailService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddRazorPages();


//Fucntions for handling data in the Hub service
builder.Services.AddResponseCompression(options =>
    options.MimeTypes = ResponseCompressionDefaults
    .MimeTypes
    .Concat(new[] { "application/octet-stream" })
    );

builder.Services.AddControllers();

builder.Services.AddHostedService<BackgroundWorkerService>();
builder.Services.AddHostedService<IcuDataWorkerService>();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{});

//Repository services
builder.Services.AddScoped<IuserRequestService , userRequestService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "CookieAuth";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(240);
    });


builder.Services.AddMvc()
    .AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCompression();
app.MapHub<NotifyUserHub>("/QueueUpdate");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

