using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Proxet.Tournament.WebApi.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Proxet.Tournament.BLL.DTOs;
using Proxet.Tournament.BLL.Services.Contracts;

namespace Proxet.Tournament.WebApi.Handlers
{
    public class AddPlayerToLobbyCommandHandler : BaseHandler, ICommandHandler<AddPlayerToLobbyCommand, IActionResult>
    {
        private readonly IPlayerService _playerService;

        private readonly IMapper _mapper;
        
        public AddPlayerToLobbyCommandHandler(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> ExecuteAsync(AddPlayerToLobbyCommand command, IActionResult previousResult)
        {
            PlayerDto player = _mapper.Map<PlayerDto>(command);

            List<PlayerDto> players = await _playerService.AddPlayerAsync(player);

            return Created(players);
        }
    }
}