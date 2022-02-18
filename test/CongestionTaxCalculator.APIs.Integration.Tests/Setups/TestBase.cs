using CongestionTaxCalculator.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;
using System.Net.Http;

namespace CongestionTaxCalculator.APIs.Integration.Tests.Setups
{
    public class TestBase
    {
        protected readonly HttpClient HttpClient;
        protected readonly CongestionTaxCalculatorContext Context;
        protected TestBase()
        {
            EnsureDataFilesContainer();
            var databaseConnection = $"DataSource=TestDatabases/Volvo.CongestionTaxCalculator.Test.{Guid.NewGuid()}.db";
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(CongestionTaxCalculatorContext));
                        services.RemoveAll(typeof(DbContextOptions<CongestionTaxCalculatorContext>));
                        services.AddDbContext<CongestionTaxCalculatorContext>(options =>
                        {
                            options.UseSqlite(databaseConnection);
                        });
                    });
                });

            HttpClient = application.CreateClient();
            var scope = application.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            Context = serviceProvider.GetRequiredService<CongestionTaxCalculatorContext>();
        }

        private static void EnsureDataFilesContainer()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            const string databaseContainerFolderName = "TestDatabases";
            var path = Path.Combine(currentDirectory, databaseContainerFolderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
