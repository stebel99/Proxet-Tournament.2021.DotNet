using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Proxet.Tournament.BLL.DTOs;
using Proxet.Tournament.BLL.Services.Contracts;
using Proxet.Tournament.DAL.Entities;
using Proxet.Tournament.DAL.Repositories.Contracts;

namespace Proxet.Tournament.BLL.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        private readonly IGuidService _guidService;

        private readonly IMapper _mapper;

        private int _skip;
        private const int ShouldTakeInEachGroup = 3;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper, IGuidService guidService)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _guidService = guidService;
        }

        public async Task<List<PlayerDto>> AddPlayerAsync(PlayerDto playerDto, CancellationToken token = default)
        {
            Player player = _mapper.Map<Player>(playerDto);
            player.Id = _guidService.NewGuid;

            await _playerRepository.AddPlayerAsync(player, token);
            await _playerRepository.SaveChangesAsync(token);

            List<Player> lobby = await _playerRepository.GetPlayersAsync(token);

            return _mapper.Map<List<PlayerDto>>(lobby);
        }

        public async Task<TeamsDto> GetTeamsAsync(CancellationToken token = default)
        {
            List<Player> allPlayers = await _playerRepository.GetPlayersAsync(token);

            TeamsDto teams = new TeamsDto();
            List<Player> playersToDelete = new List<Player>();

            teams.FirstTeam = GetPlayersForTeam(allPlayers, playersToDelete);
            teams.SecondTeam = GetPlayersForTeam(allPlayers, playersToDelete);

            if (teams.FirstTeam.Count < 9 || teams.SecondTeam.Count < 9)
            {
                return null;
            }

            _playerRepository.Delete(playersToDelete);
            await _playerRepository.SaveChangesAsync(token);

            return teams;
        }

        private List<TeamMemberDto> GetPlayersForTeam(List<Player> allPlayers, List<Player> playersToDelete)
        {
            List<Player> newTeamPlayers = new List<Player>();

            var groupedPlayers = allPlayers.GroupBy(x => x.Type);

            foreach (var playersType in groupedPlayers)
            {
                var playersInType = playersType.Skip(_skip).Take(ShouldTakeInEachGroup).ToList();
                newTeamPlayers.AddRange(playersInType);
            }

            _skip += ShouldTakeInEachGroup;

            playersToDelete.AddRange(newTeamPlayers);

            return _mapper.Map<List<TeamMemberDto>>(newTeamPlayers);
        }
    }
}