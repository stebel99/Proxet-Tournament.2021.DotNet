using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Proxet.Tournament.BLL.Configuration;
using Proxet.Tournament.BLL.Services;
using Proxet.Tournament.BLL.Services.Contracts;
using Proxet.Tournament.DAL.Repositories;
using Proxet.Tournament.DAL.Repositories.Contracts;
using Proxet.Tournament.WebApi.Configuration;

namespace Proxet.Tournament.WebApi.Extensions
{
    public static class DependencyRegistration
    {
        public static void RegisterReposAndServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPlayerRepository, PlayerRepository>();
            serviceCollection.AddTransient<IPlayerService, PlayerService>();
            serviceCollection.AddTransient<IGuidService, GuidService>();
        }

        public static void RegisterMapper(this IServiceCollection serviceCollection)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<ProxetApiProfile>();
                c.AddProfile<ProxetProfile>();
            });

            serviceCollection.AddSingleton(s => mapperConfiguration.CreateMapper());
        }
    }
}