using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proxet.Tournament.DAL.DataContext;
using Proxet.Tournament.WebApi.Commands;
using Proxet.Tournament.WebApi.Extensions;
using Proxet.Tournament.WebApi.Handlers;
using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Proxet.Tournament.WebApi
{
    public class AppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            
            builder
                .Setup((serviceCollection, commandRegistry) =>
                {
                    serviceCollection.RegisterMapper();
                    serviceCollection.RegisterReposAndServices();

                    serviceCollection.AddDbContext<ProxetDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    serviceCollection.AddScoped<ProxetDbContext>();

                    commandRegistry.Discover<AppConfiguration>();
                })
                .DefaultHttpResponseHandler<HttpResponseHandler>()
                .Functions(functions => functions
                    .HttpRoute("/v1", route => route
                        .HttpFunction<HealthCheckQuery>("/healthcheck", HttpMethod.Get)
                        .HttpFunction<AddPlayerToLobbyCommand>("/lobby", HttpMethod.Post)
                        .HttpFunction<GenerateTeamsCommand>("/teams/generate", HttpMethod.Post)));
        }
    }
}