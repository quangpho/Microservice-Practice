using Model;
using Moq;
using Repository.Interfaces;
using Services;
using Xunit;

namespace Test
{
    public class ClubServiceTests
    {
        private readonly Mock<IClubRepository> _mockClubRepository;
        private readonly ClubService _clubService;

        public ClubServiceTests()
        {
            _mockClubRepository = new Mock<IClubRepository>();
            _clubService = new ClubService(_mockClubRepository.Object);
        }

        [Fact]
        public async Task ClubExistsByNameAsync_ShouldReturnTrue_WhenClubExists()
        {
            // Arrange
            var clubName = "Test Group";
            _mockClubRepository.Setup(repo => repo.ExistsByNameAsync(clubName))
                .ReturnsAsync(true);

            // Act
            var result = await _clubService.ClubExistsByNameAsync(clubName);

            // Assert
            Assert.True(result);
            _mockClubRepository.Verify(repo => repo.ExistsByNameAsync(clubName), Times.Once);
        }

        [Fact]
        public async Task ClubExistsByNameAsync_ShouldReturnFalse_WhenClubDoesNotExist()
        {
            // Arrange
            var clubName = "Nonexistent Group";
            _mockClubRepository.Setup(repo => repo.ExistsByNameAsync(clubName))
                .ReturnsAsync(false);
        
            // Act
            var result = await _clubService.ClubExistsByNameAsync(clubName);
        
            // Assert
            Assert.False(result);
            _mockClubRepository.Verify(repo => repo.ExistsByNameAsync(clubName), Times.Once);
        }
        
        [Fact]
        public async Task CreateClubAsync_ShouldCreateClubSuccessfully()
        {
            // Arrange
            var clubName = "New Group";
            _mockClubRepository.Setup(repo => repo.AddAsync(It.IsAny<Group>()))
                .Returns(Task.CompletedTask);
        
            // Act
            var club = await _clubService.CreateClubAsync(clubName);
        
            // Assert
            Assert.NotNull(club);
            Assert.Equal(clubName, club.Name);
            Assert.NotEqual(Guid.Empty, club.Id);
            Assert.Empty(club.Members);
            _mockClubRepository.Verify(repo => repo.AddAsync(It.IsAny<Group>()), Times.Once);
        }
        
        [Fact]
        public async Task AddMemberToClubAsync_ShouldAddMemberSuccessfully()
        {
            // Arrange
            var player = new Member { MemberId = 123};
            var club = new Group
            {
                Id = Guid.NewGuid(),
                Name = "Test Group",
                Members = new List<Member>()
            };
        
            _mockClubRepository.Setup(repo => repo.UpdateAsync(club))
                .Returns(Task.CompletedTask);
        
            // Act
            await _clubService.AddMemberToClubAsync(player, club);
        
            // Assert
            Assert.Single(club.Members);
            Assert.Contains(player, club.Members);
            _mockClubRepository.Verify(repo => repo.UpdateAsync(club), Times.Once);
        }
        
        [Fact]
        public async Task GetClubInfo_ShouldReturnClubWithMembers()
        {
            // Arrange
            var clubId = Guid.NewGuid();
            var expectedClub = new Group
            {
                Id = clubId,
                Name = "Test Group",
                Members = new List<Member>
                {
                    new Member { MemberId = 123},
                    new Member { MemberId = 234},
                }
            };
        
            _mockClubRepository.Setup(repo => repo.GetWithMembersAsync(clubId))
                .ReturnsAsync(expectedClub);
        
            // Act
            var result = await _clubService.GetClubInfo(clubId);
        
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedClub.Id, result.Id);
            Assert.Equal(expectedClub.Name, result.Name);
            Assert.Equal(expectedClub.Members.Count, result.Members.Count);
            _mockClubRepository.Verify(repo => repo.GetWithMembersAsync(clubId), Times.Once);
        }
    }
}
