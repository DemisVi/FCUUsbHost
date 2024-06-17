using System;
using FCUUsbService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTcpServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/simcomdevices", () =>
{
    var provider = new SimComDeviceProvider();
    return provider.GetSimComDevices();
})
.WithName("FCUSimComDevices")
.WithOpenApi();

app.MapGet("/usb", () =>
{
    var provider = new UsbConfigurationProvider();
    return provider.GetUsbConfiguration();
})
.WithName("FCUUsbDevices")
.WithOpenApi();

app.Run();
