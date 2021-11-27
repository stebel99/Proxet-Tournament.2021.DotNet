using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Proxet.Tournament.DAL.Repositories.Contracts;
using Proxet.Tournament.WebApi.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Proxet.Tournament.WebApi.Handlers
{
    public class HealthCheckQueryHandler : BaseHandler, ICommandHandler<HealthCheckQuery, IActionResult>
    {
        private readonly IPlayerRepository _playerRepository;

        public HealthCheckQueryHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IActionResult> ExecuteAsync(HealthCheckQuery command, IActionResult previousResult)
        {
            try
            {
                await _playerRepository.SaveChangesAsync(CancellationToken.None);
            }
            catch (Exception)
            {
                return ServiceUnavailable();
            }

            return Ok();
        }
    }
}