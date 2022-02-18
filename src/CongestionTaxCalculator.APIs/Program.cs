using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Application.Common;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Persistence;
using CongestionTaxCalculator.Persistence.Extensions;
using FluentValidation.AspNetCore;
using MediatR;
using Serilog;
using System.Reflection;
using DependencyContainer = CongestionTaxCalculator.Application.DependencyContainer;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(DependencyContainer.ConfigureLogger);

builder.Services.AddControllers()
    .AddFluentValidation(options =>
        options.RegisterValidatorsFromAssemblies(new List<Assembly>() { typeof(ApplicationPointer).Assembly }))
    //.AddJsonOptions(opt =>
    //{
    //    opt.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
    //    opt.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
    //});
    .AddJsonOptions(opt => opt.UseDateOnlyTimeOnlyStringConverters());

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddMediatR(typeof(ApplicationPointer).Assembly, typeof(ApplicationPointer).Assembly);

builder.Services.AddAutoMapper(typeof(ApplicationPointer).Assembly);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.UseDateOnlyTimeOnlyStringConverters());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.AutomaticMigrations();

app.UseValidationHandler();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
