using DevWorld.LaContessa.Command.Users;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Tests
{
    [TestFixture]
    public class CreateUserHandlerTests : UnitTestBase
    {   
        private CreateUserHandler _handler;
        private LaContessaDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Initialize the in-memory database context and the handler
            _dbContext = new LaContessaDbContext(
                new LaContessaDbContextOptions
                {
                    DatabaseName = "lacontessadb",
                    UseInMemoryProvider = true,
                });

            _handler = new CreateUserHandler(_dbContext);
        }

        [Test]
        public async Task Handle_GivenNewUser_ShouldCreateUser()
        {
            var exampleUser = UserTestFactory.Create();
            
            // Arrange
            var createUserRequest = new CreateUser
            {
                User = new CreateUser.UserDetail() { Email = exampleUser.Email, Name = exampleUser.Name }
            };

            // Act
            await _handler.Handle(createUserRequest, new CancellationToken());

            // Assert
            _dbContext.Users.ToList().Should().BeEquivalentTo(
                    new[] {exampleUser},
                    options => options
                        .Including(x => x.Name)
                        .Including(x => x.Email)
                        .ExcludingMissingMembers() 
                );
        }

        [Test]
        public void Handle_GivenExistingUserEmail_ShouldThrowUserAlreadyExistException()
        {
            // Arrange
            var existingUserEmail = "existingemail@example.com";
            var existingUser = new User() {Id= Guid.NewGuid(), Email = existingUserEmail, Name = "Test User" };
            var createUserRequest = new CreateUser
            {
                User = new CreateUser.UserDetail { Email = existingUserEmail, Name = "Test User" }
            };

            _dbContext.Users.Add(existingUser);

            _dbContext.SaveChangesAsync();

            // Seed the database with a user having the same email

            // Act & Assert
            Assert.ThrowsAsync<UserAlreadyExistException>(() => _handler.Handle(createUserRequest, new CancellationToken()));
        }
    }
}
