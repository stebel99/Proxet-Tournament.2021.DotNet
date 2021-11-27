using System.Collections.Generic;
using Proxet.Tournament.DAL.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Proxet.Tournament.DAL.Repositories.Contracts
{
    public interface IPlayerRepository
    {
        Task AddPlayerAsync(Player player, CancellationToken token);

        Task SaveChangesAsync(CancellationToken token);

        Task<List<Player>> GetPlayersAsync(CancellationToken token);

        void Delete(List<Player> players);
    }
}