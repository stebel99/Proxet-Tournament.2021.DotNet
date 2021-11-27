using Microsoft.EntityFrameworkCore;
using Proxet.Tournament.DAL.Entities;
using Proxet.Tournament.DAL.Extensions;

namespace Proxet.Tournament.DAL.DataContext
{
    public class ProxetDbContext :DbContext
    {
        public ProxetDbContext(DbContextOptions<ProxetDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildPlayerModel();
        }
    }
}