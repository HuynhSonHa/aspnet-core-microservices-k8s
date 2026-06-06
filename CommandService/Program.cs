using CommandService.AsyncDataServices;
using CommandService.Data;
using CommandService.EventProcessing;
using CommandService.Interfaces;
using CommandService.Repositories;
using CommandService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("InMem")
);

builder.Services.AddScoped<ICommandRepository, CommandRepository>();

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();   

builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

PrepDb.PrepPopulation(app);

app.Run();

