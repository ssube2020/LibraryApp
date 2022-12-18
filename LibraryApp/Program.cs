using Application;
using AutoMapper;
using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.DataAccess;
using Infrastructure.Repostitories;
using LibraryAppAPI.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

try
{
    var builder = WebApplication.CreateBuilder(args);

    string connstr = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connstr);
    });

    builder.Services.AddControllers();

    builder.Services.RegisterApplication();
    builder.Services.RegisterInfrastructure();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
} catch (Exception ex)
{
    throw new Exception(ex.Message);
}


