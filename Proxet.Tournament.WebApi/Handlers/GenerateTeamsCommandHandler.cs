using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Proxet.Tournament.WebApi.Commands;
using System;
using System.Threading.Tasks;
using Proxet.Tournament.BLL.DTOs;
using Proxet.Tournament.BLL.Services.Contracts;

namespace Proxet.Tournament.WebApi.Handlers
{
    public class GenerateTeamsCommandHandler : BaseHandler, ICommandHandler<GenerateTeamsCommand, IActionResult>
    {
        private readonly IPlayerService _playerService;

        public GenerateTeamsCommandHandler(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        public async Task<IActionResult> ExecuteAsync(GenerateTeamsCommand command, IActionResult previousResult)
        {
            TeamsDto teams = await _playerService.GetTeamsAsync();

            if (teams == null)
            {
                return BadRequest("Not enough players");
            }

            return Ok(teams);
        }
    }
}