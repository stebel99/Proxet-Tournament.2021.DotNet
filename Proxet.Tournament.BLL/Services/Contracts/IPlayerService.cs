using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proxet.Tournament.BLL.DTOs;

namespace Proxet.Tournament.BLL.Services.Contracts
{
    public interface IPlayerService
    {
        Task<List<PlayerDto>> AddPlayerAsync(PlayerDto player, CancellationToken token = default);

        Task<TeamsDto> GetTeamsAsync(CancellationToken token = default);
    }
}