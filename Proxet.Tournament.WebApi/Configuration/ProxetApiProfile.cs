using AutoMapper;
using Proxet.Tournament.BLL.DTOs;
using Proxet.Tournament.WebApi.Commands;

namespace Proxet.Tournament.WebApi.Configuration
{
    public class ProxetApiProfile : Profile
    {
        public ProxetApiProfile()
        {
            CreateMap<AddPlayerToLobbyCommand, PlayerDto>();
        }
    }
}