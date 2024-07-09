using AutoMapper;
using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Handlers.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using System.Net;

namespace Blue.Challenge.Tests.CommandTests
{
    public class CreateContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateContactCommandHandler _handler;

        public CreateContactCommandHandlerTests()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _mapperMock = new Mock<IMapper>();

            _handler = new CreateContactCommandHandler(
                _contactRepositoryMock.Object,
                _userRepositoryMock.Object,
                _identityServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            // Arrange
            var command = new CreateContactCommand("", "", "");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().BeOfType(typeof(List<ValidationFailure>));
        }

        [Fact]
        public async Task Handle_ShouldCreateContact_WhenRequestIsValid()
        {
            // Arrange
            var command = new CreateContactCommand("blue", "blue@blue.com", "123456789");
            var user = new User("green", "green@green", "123");
            user.Id = 10;
            var contact = new Contact(command.Name, command.Email, command.Phone, 10);
            var getContactResponse = new GetContactQueryResponse();

            _identityServiceMock.Setup(s => s.GetUserId()).Returns(user.UserId);
            _userRepositoryMock.Setup(r => r.GetByUserId(user.UserId)).ReturnsAsync(user);
            _contactRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);
            _contactRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<GetContactQueryResponse>(It.IsAny<Contact>())).Returns(getContactResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Data.Should().Be(getContactResponse);
            _contactRepositoryMock.Verify(r => r.AddAsync(It.Is<Contact>(c =>
                c.Name == command.Name &&
                c.Email == command.Email &&
                c.Phone == command.Phone &&
                c.UserId == user.Id
            )), Times.Once);
            _contactRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}