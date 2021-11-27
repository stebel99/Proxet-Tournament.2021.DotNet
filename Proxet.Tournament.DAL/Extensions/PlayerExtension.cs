using Microsoft.EntityFrameworkCore;
using Proxet.Tournament.DAL.Entities;

namespace Proxet.Tournament.DAL.Extensions
{
    public static class PlayerExtension
    {
        public static void BuildPlayerModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Player>()
                .Property(i => i.Id)
                .HasMaxLength(38)
                .ValueGeneratedNever();

            modelBuilder.Entity<Player>()
                .Property(i => i.Name)
                .IsRequired();

            modelBuilder.Entity<Player>()
                .Property(i => i.Type)
                .IsRequired();
        }
    }
}