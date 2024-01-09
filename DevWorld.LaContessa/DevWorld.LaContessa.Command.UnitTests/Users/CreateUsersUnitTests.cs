//using DevWorld.LaContessa.Command.Abstractions.Exceptions;
//using DevWorld.LaContessa.Command.Abstractions.Users;
//using DevWorld.LaContessa.Command.Users;
//using DevWorld.LaContessa.Persistance;
//using DevWorld.LaContessa.TestUtils.TestFactories;
//using DevWorld.LaContessa.TestUtils.Utils;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;

//namespace DevWorld.LaContessa.Command.UnitTests.Users;

//[TestFixture]
//public class CreateUserHandlerTests : UnitTestBase
//{
//    [SetUp]
//    public void Setup()
//    {
//        // Initialize the in-memory database context and the handler
//        _dbContext = new LaContessaDbContext(
//            new LaContessaDbContextOptions
//            {
//                DatabaseName = Guid.NewGuid().ToString(),
//                UseInMemoryProvider = true
//            });

//        _handler = new CreateUserHandler(_dbContext);
//    }

//    private CreateUserHandler _handler;
//    private LaContessaDbContext _dbContext;

//    [Test]
//    public async Task Handle_GivenNewUser_ShouldCreateUser()
//    {
//        var exampleUser = UserTestFactory.Create();

//        // Arrange
//        var createUserRequest = new CreateUser
//        {
//            User = new CreateUser.UserDetail
//            {
//                Email = exampleUser.Email,
//                Name = exampleUser.Name,
//                Surname = exampleUser.Surname,
//                CardNumber = exampleUser.CardNumber,
//                IsPro = exampleUser.IsPro,
//                ImageProfile = exampleUser.ImageProfile,
//                Password = exampleUser.Password
//            }
//        };

//        // Act
//        await _handler.Handle(createUserRequest, new CancellationToken());

//        // Assert
//        var user = await _dbContext.Users.FirstOrDefaultAsync();

//        Assert.That(user, Is.Not.Null);
//        user.Should().BeEquivalentTo(
//            exampleUser,
//            options => options
//                .Excluding(x => x.Id)
//                .Excluding(x => x.Password)
//        );

//        bool isPasswordCorrect = PasswordManager.VerifyPassword(exampleUser.Password, user.Password);
//        Assert.That(isPasswordCorrect, Is.True);
//    }

//    [Test]
//    public void Handle_GivenExistingUserEmail_ShouldThrowUserAlreadyExistException()
//    {
//        // Arrange
//        var existingUser = UserTestFactory.Create();
//        var createUserRequest = new CreateUser
//        {
//            User = new CreateUser.UserDetail
//            {
//                Email = existingUser.Email,
//                Name = existingUser.Name,
//                Surname = existingUser.Surname,
//                CardNumber = existingUser.CardNumber,
//                IsPro = existingUser.IsPro,
//                Password = existingUser.Password,
//                ImageProfile = existingUser.ImageProfile
//            }
//        };

//        _dbContext.Users.Add(existingUser);

//        _dbContext.SaveChangesAsync();

//        // Seed the database with a user having the same email

//        // Act & Assert
//        Assert.ThrowsAsync<UserAlreadyExistException>(() =>
//            _handler.Handle(createUserRequest, new CancellationToken()));
//    }
//}