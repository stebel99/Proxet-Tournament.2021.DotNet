using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using DeepEqual.Syntax;
using Microsoft.EntityFrameworkCore;
using Proxet.Tournament.DAL.DataContext;
using Proxet.Tournament.DAL.Entities;
using Proxet.Tournament.DAL.Repositories;
using Xunit;

namespace Proxet.Turnament.Tests.DAL
{
    public class PlayerRepositoryTests : IDisposable
    {
        private readonly ProxetDbContext _context;

        private readonly IFixture _fixture;

        private bool _disposed;

        public PlayerRepositoryTests()
        {
            DbContextOptions<ProxetDbContext> mockOptions = new DbContextOptionsBuilder<ProxetDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new ProxetDbContext(mockOptions);

            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddPlayerAsync_ValidData_AddExpected()
        {
            // Arrange
            Player player = _fixture.Create<Player>();

            PlayerRepository repository = new PlayerRepository(_context);

            // Act
            await repository.AddPlayerAsync(player, default);
            await repository.SaveChangesAsync(default);

            Player actualResult = (await repository.GetPlayersAsync(default)).FirstOrDefault();

            // Assert
            player.WithDeepEqual(actualResult).Assert();
        }

        [Fact]
        public async Task GetPlayersAsync_ValidData_ReturnsExpected()
        {
            // Arrange
            IEnumerable<Player> players = _fixture.CreateMany<Player>();

            PlayerRepository repository = new PlayerRepository(_context);

            await _context.Players.AddRangeAsync(players);
            await _context.SaveChangesAsync();

            // Act
            IEnumerable<Player> actualResult = await repository.GetPlayersAsync(default);

            // Assert
            players.WithDeepEqual(actualResult).Assert();
        }

        [Fact]
        public async Task Delete_ValidData_DeleteExpected()
        {
            // Arrange
            Player player = _fixture.Create<Player>();
            IEnumerable<Player> players = _fixture.CreateMany<Player>();

            PlayerRepository repository = new PlayerRepository(_context);

            await _context.Players.AddRangeAsync(players);
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            // Act
            repository.Delete(new List<Player> { player });
            await repository.SaveChangesAsync(default);

            IEnumerable<Player> actualResult = await _context.Players.ToListAsync(default);

            // Assert
            players.WithDeepEqual(actualResult).Assert();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}