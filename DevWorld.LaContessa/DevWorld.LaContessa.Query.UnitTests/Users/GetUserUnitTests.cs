using NUnit.Framework;
using FluentAssertions;
using DevWorld.LaContessa.Query;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetUserUnitTests : UnitTestBase
    {
        private GetUserHandler _handler;
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

            _handler = new GetUserHandler(_dbContext);
        }

        [Test]
        public async Task GetUserHandler_ReturnsCorrectSubscription()
        {
            // Arrange
            var expectedUser = UserTestFactory.Create();

            _dbContext.Users.Add(expectedUser);
            await _dbContext.SaveChangesAsync();

            var request = new GetUser(expectedUser.Id) { Id = expectedUser.Id };

            // Act
            var response = await _handler.Handle(request, new CancellationToken());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.User);

            response.User
                .Should()
                .BeEquivalentTo(
                    new GetUser.Response.UserDetail
                    {
                        Id = expectedUser.Id,
                        Name = expectedUser.Name
                    },
                    options => options.ExcludingMissingMembers() // Exclude fields that are not part of the response
                );
        }
    }
}
