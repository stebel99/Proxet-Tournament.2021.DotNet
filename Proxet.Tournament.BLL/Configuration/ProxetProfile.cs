using AutoMapper;
using Proxet.Tournament.BLL.DTOs;
using Proxet.Tournament.DAL.Entities;

namespace Proxet.Tournament.BLL.Configuration
{
    public class ProxetProfile : Profile
    {
        public ProxetProfile()
        {
            CreateMap<PlayerDto, Player>()
                .ForMember(d => d.Type, map => map.MapFrom(t => t.VehicleType))
                .ForMember(d => d.Name, map => map.MapFrom(t => t.UserName))
                .ReverseMap();

            CreateMap<Player, TeamMemberDto>()
                .ForMember(d => d.VehicleType, map => map.MapFrom(t => t.Type))
                .ForMember(d => d.Nickname, map => map.MapFrom(t => t.Name));
        }
    }
}