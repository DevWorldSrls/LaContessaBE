using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetUsersUnitTests : UnitTestBase
    {
        private GetUsersHandler _handler;
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

            _handler = new GetUsersHandler(_dbContext);
        }

        [Test]
        public async Task GetUsersHandler_ReturnsCorrectSubscriptions()
        {
            // Arrange
            var expectedUser1 = UserTestFactory.Create();
            var expectedUser2 = UserTestFactory.Create();

            _dbContext.Users.Add(expectedUser1);
            _dbContext.Users.Add(expectedUser2);
            await _dbContext.SaveChangesAsync();

            // Act
            var response = await _handler.Handle(new GetUsers(), new CancellationToken());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Users);

            response.Users
                .Should()
                .BeEquivalentTo(
                    new[] { expectedUser1, expectedUser2 },
                    options => options.Including(x => x.Id)
                                      .Including(x => x.Name)
                                      .Including(x => x.Email)
                                      .ExcludingMissingMembers() // Exclude fields that are not part of the response
                );
        }
    }
}
