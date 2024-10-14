using System;
using FCUUsbService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
.WithName("FCU SimCom Devices")
.WithOpenApi();

app.MapGet("/usb", () =>
{
    var provider = new UsbConfigurationProvider();
    return provider.GetUsbConfiguration();
})
.WithName("FCU Usb Devices")
.WithOpenApi();

app.MapGet("/dhcpclients", () =>
{
    var provider = new DhcpClientProvider();
    return provider.GetDhcpClients();
})
.WithName("Dhcp Clients")
.WithOpenApi();

app.Run();
