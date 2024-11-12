using DataAccess.AutoMapper;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using DataAccess.Repository.SQLServerServices;
using SystemAPI;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, builder.Environment);