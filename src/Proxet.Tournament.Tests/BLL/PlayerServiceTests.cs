using AutoFixture;
using AutoMapper;
using FakeItEasy;
using Proxet.Tournament.BLL.Configuration;
using Proxet.Tournament.BLL.DTOs;
using Proxet.Tournament.BLL.Services;
using Proxet.Tournament.BLL.Services.Contracts;
using Proxet.Tournament.DAL.Entities;
using Proxet.Tournament.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Proxet.Turnament.Tests.BLL
{
    public class PlayerServiceTests
    {
        private readonly IPlayerRepository _playerRepository;

        private readonly IGuidService _guidService;

        private readonly IMapper _mapper;

        private readonly Fixture _fixture;

        public PlayerServiceTests()
        {
            _playerRepository = A.Fake<IPlayerRepository>();
            _guidService = A.Fake<IGuidService>();

            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<ProxetProfile>(); })
                .CreateMapper();

            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddPlayerAsync_ValidData_ReturnsPlayers()
        {
            // Arrange
            PlayerDto player = _fixture.Create<PlayerDto>();
            List<Player> players = _fixture.CreateMany<Player>().ToList();

            IPlayerService service = CreateService();

            A.CallTo(() => _playerRepository.GetPlayersAsync(A<CancellationToken>._))
                .Returns(players);

            // Act
            List<PlayerDto> result = await service.AddPlayerAsync(player, A<CancellationToken>._);

            // Assert
            Assert.NotNull(result);

            A.CallTo(() => _playerRepository.AddPlayerAsync(A<Player>._, A<CancellationToken>._))
                .MustHaveHappened();

            A.CallTo(() => _playerRepository.SaveChangesAsync(A<CancellationToken>._))
                .MustHaveHappened();

            A.CallTo(() => _playerRepository.GetPlayersAsync(A<CancellationToken>._))
                .MustHaveHappened();
        }

        private IPlayerService CreateService()
        {
            return new PlayerService(_playerRepository, _mapper, _guidService);
        }
    }
}