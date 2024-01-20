using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SporcialAPI.Context;
using SporcialAPI.Endpoints;
using SporcialAPI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IActivityService, ActivityService>();

string postgresConnection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Can't find 'DefaultConnection' string");

builder.Services.AddDbContext<SporcialDbContext>(opt => opt.UseNpgsql(postgresConnection));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<SporcialDbContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
        .ExecuteAsync(statusCodeContext.HttpContext));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/auth/").MapIdentityApi<IdentityUser>();

app.RegisterActivitiesEndpoints();

app.Run();