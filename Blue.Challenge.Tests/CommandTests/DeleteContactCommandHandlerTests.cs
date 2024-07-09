using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Handlers.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using FluentAssertions;
using MediatR;
using Moq;
using System.Net;

namespace Blue.Challenge.Tests.CommandTests
{
    public class DeleteContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly DeleteContactCommandHandler _handler;

        public DeleteContactCommandHandlerTests()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _mediatorMock = new Mock<IMediator>();

            _handler = new DeleteContactCommandHandler(
                _contactRepositoryMock.Object,
                _identityServiceMock.Object,
                _mediatorMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenContactNotFound()
        {
            // Arrange
            var command = new DeleteContactCommand { Id = 10 };

            _contactRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Contact)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().Be("Contato não encontrado");
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenContactDoesNotBelongToUser()
        {
            // Arrange
            var command = new DeleteContactCommand { Id = 5 };
            var contact = new Contact("umqualquer", "umai", "8779", 20);
            contact.User = new User("otro", "doisai", "1231");
            
            _contactRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                  .ReturnsAsync(contact);

            _identityServiceMock.Setup(service => service.GetUserId())
                                .Returns(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Data.Should().Be("Contato não pertence ao usuário");
        }       

    }
}
