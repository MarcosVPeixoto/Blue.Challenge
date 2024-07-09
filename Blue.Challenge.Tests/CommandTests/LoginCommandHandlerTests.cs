using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Handlers.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net;

namespace Blue.Challenge.Tests.CommandTests
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _configurationMock = new Mock<IConfiguration>();
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Jwt:Secret", "jKmHO1tXZ6h/v8sf8+gQ7T8fPLxFxHXLCJ6iVdQx/jQcDOswCJcOoA9Jp8ykY1wUYcTFng5ED9Zc8g4ZP7yM4w=="},
                {"Jwt:AccessTokenExpireTime", "600"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _handler = new LoginCommandHandler(_userRepositoryMock.Object, configuration);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var command = new LoginCommand { Email = "", Password = "" };

            //// Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().BeOfType<List<ValidationFailure>>();
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenUserNotFound()
        {
            // Arrange
            var command = new LoginCommand { Email = "test@example.com", Password = "password" };

            _userRepositoryMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().Be("Email inválido");
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenPasswordIsIncorrect()
        {
            // Arrange
            var command = new LoginCommand { Email = "test@example.com", Password = "wrongpassword" };
            var user = new User("Test User", command.Email, PasswordHasher.HashPassword("hashedpassword"));

            _userRepositoryMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);            

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().Be("Senha incorreta");
        }
    }
}
