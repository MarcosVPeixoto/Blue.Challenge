using AutoMapper;
using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Handlers.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using System.Net;
namespace Blue.Challenge.Tests.CommandTests
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new CreateUserCommandHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "",
                Email = "",
                Password = ""
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().BeOfType<List<ValidationFailure>>();
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenEmailIsAlreadyInUse()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "password123"
            };
            var existingUser = new User("acdc", "blue@blue", "teste");
            _userRepositoryMock.Setup(repo => repo.GetByEmail(It.IsAny<string>())).ReturnsAsync(existingUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().Be("Email em uso");
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenUserIsCreatedSuccessfully()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "password123"
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _userRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().BeOfType<Guid>();
        }
    }
}