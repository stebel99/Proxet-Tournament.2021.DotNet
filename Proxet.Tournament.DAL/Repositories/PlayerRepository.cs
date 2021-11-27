using Microsoft.EntityFrameworkCore;
using Proxet.Tournament.DAL.DataContext;
using Proxet.Tournament.DAL.Entities;
using Proxet.Tournament.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Proxet.Tournament.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ProxetDbContext _context;

        public PlayerRepository(ProxetDbContext context)
        {
            _context = context;
        }

        public async Task AddPlayerAsync(Player player, CancellationToken token)
        {
            await _context.Players.AddAsync(player, token);
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }

        public async Task<List<Player>> GetPlayersAsync(CancellationToken token)
        {
            return await _context.Players.AsNoTracking().ToListAsync(token);
        }

        public void Delete(List<Player> players)
        {
            _context.RemoveRange(players);
        }
    }
}