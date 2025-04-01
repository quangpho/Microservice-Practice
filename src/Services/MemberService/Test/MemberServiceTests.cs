using Application.Interfaces;
using Application.Services;
using Domain;
using Moq;
using Xunit;

namespace Test
{
    public class MemberServiceTests
    {
        private readonly Mock<IRepository<Member>> _mockPlayerRepository;
        private readonly MemberService _memberService;

        public MemberServiceTests()
        {
            _mockPlayerRepository = new Mock<IRepository<Member>>();
            _memberService = new MemberService(_mockPlayerRepository.Object);
        }

        [Fact]
        public async Task CreatePlayerAsync_ShouldCreatePlayerSuccessfully()
        {
            // Arrange
            long id = 123;
            _mockPlayerRepository.Setup(repo => repo.AddItemAsync(It.IsAny<Member>()))
                .Returns(Task.CompletedTask);

            // Act
            var member = await _memberService.CreateMemberAsync(id, "name");

            // Assert
            Assert.NotNull(member);
            Assert.Equal(id.ToString(), member.Id);
            _mockPlayerRepository.Verify(repo => repo.AddItemAsync(It.IsAny<Member>()), Times.Once);
        }

        // [Fact]
        // public async Task HasClub_ShouldReturnTrue_WhenPlayerHasClub()
        // {
        //     // Arrange
        //     long playerId = 123;
        //     var player = new Member
        //     {
        //         MemberId = playerId,
        //         Club = new Group { Id = System.Guid.NewGuid(), Name = "Test Group" }
        //     };
        //
        //     _mockPlayerRepository.Setup(repo => repo.GetPlayerInfo(playerId))
        //         .ReturnsAsync(player);
        //
        //     // Act
        //     var result = await _memberService.HasClub(playerId);
        //
        //     // Assert
        //     Assert.True(result);
        //     _mockPlayerRepository.Verify(repo => repo.GetPlayerInfo(playerId), Times.Once);
        // }
        //
        // [Fact]
        // public async Task GetPlayerAsync_ShouldReturnPlayer_WhenPlayerExists()
        // {
        //     // Arrange
        //     long playerId = 123;
        //     var expectedPlayer = new Member { MemberId = playerId };
        //
        //     _mockPlayerRepository.Setup(repo => repo.GetPlayerInfo(playerId))
        //         .ReturnsAsync(expectedPlayer);
        //
        //     // Act
        //     var result = await _memberService.GetPlayerAsync(playerId);
        //
        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal(playerId, result.MemberId);
        //     _mockPlayerRepository.Verify(repo => repo.GetPlayerInfo(playerId), Times.Once);
        // }
        //
        // [Fact]
        // public async Task GetPlayerAsync_ShouldReturnNull_WhenPlayerDoesNotExist()
        // {
        //     // Arrange
        //     long playerId = 123;
        //
        //     _mockPlayerRepository.Setup(repo => repo.GetPlayerInfo(playerId))
        //         .ReturnsAsync((Member)null);
        //
        //     // Act
        //     var result = await _memberService.GetPlayerAsync(playerId);
        //
        //     // Assert
        //     Assert.Null(result);
        //     _mockPlayerRepository.Verify(repo => repo.GetPlayerInfo(playerId), Times.Once);
        // }
    }
}