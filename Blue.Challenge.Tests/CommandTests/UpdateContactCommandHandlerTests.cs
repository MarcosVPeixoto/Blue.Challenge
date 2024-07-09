using AutoMapper;
using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Handlers.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using Moq;
using System.Net;

namespace Blue.Challenge.Tests.CommandTests
{
    public class UpdateContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IIdentityService> _mockIdentityService;
        private readonly Mock<IMapper> _mockMapper;

        public UpdateContactCommandHandlerTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockIdentityService = new Mock<IIdentityService>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ValidUpdate_ReturnsSuccess()
        {
            // Arrange
            var userId = 5;
            var contactId = 1;
            var user = new User ("asd", "asda@das", "123123");
            user.Id = userId;
            var request = new UpdateContactCommand
            {
                Id = contactId,
                Name = "Updated Name",
                Email = "updated@example.com",
                Phone = "123456789"
            };
            var existingContact = new Contact("alasd", "asda@1.", "1723781", userId);
            _mockIdentityService.Setup(s => s.GetUserId()).Returns(user.UserId);
            _mockUserRepository.Setup(r => r.GetByUserId(user.UserId)).ReturnsAsync(user);
            _mockContactRepository.Setup(r => r.GetByIdAsync(contactId)).ReturnsAsync(existingContact);

            var handler = new UpdateContactCommandHandler(_mockMapper.Object, _mockContactRepository.Object, _mockIdentityService.Object, _mockUserRepository.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _mockContactRepository.Verify(r => r.Update(existingContact), Times.Once);
            _mockContactRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<GetContactQueryResponse>(existingContact), Times.Once);
        }

        [Fact]
        public async Task Handle_ContactNotFound_ReturnsBadRequest()
        {
            // Arrange
            var contactId = 4;
            var request = new UpdateContactCommand { Id = contactId };
            _mockIdentityService.Setup(s => s.GetUserId()).Returns(Guid.NewGuid());
            _mockContactRepository.Setup(r => r.GetByIdAsync(contactId)).ReturnsAsync((Contact)null);

            var handler = new UpdateContactCommandHandler(_mockMapper.Object, _mockContactRepository.Object, _mockIdentityService.Object, _mockUserRepository.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Contato não encontrado", response.Data);
            _mockContactRepository.Verify(r => r.Update(It.IsAny<Contact>()), Times.Never);
            _mockContactRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
            _mockMapper.Verify(m => m.Map<GetContactQueryResponse>(It.IsAny<Contact>()), Times.Never);
        }

        [Fact]
        public async Task Handle_UnauthorizedUser_ReturnsBadRequest()
        {
            // Arrange            
            var contactId = 5;
            var request = new UpdateContactCommand { Id = contactId };
            var existingContact = new Contact("'213", "31@31312", "23123", 10);
            var user = new User("1312", "132131", "141231");
            user.Id = 12;
            _mockIdentityService.Setup(s => s.GetUserId()).Returns(user.UserId);
            _mockUserRepository.Setup(r => r.GetByUserId(user.UserId)).ReturnsAsync(user);
            _mockContactRepository.Setup(r => r.GetByIdAsync(contactId)).ReturnsAsync(existingContact);

            var handler = new UpdateContactCommandHandler(_mockMapper.Object, _mockContactRepository.Object, _mockIdentityService.Object, _mockUserRepository.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Contato não pertence ao usuário", response.Data);
            _mockContactRepository.Verify(r => r.Update(It.IsAny<Contact>()), Times.Never);
            _mockContactRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
            _mockMapper.Verify(m => m.Map<GetContactQueryResponse>(It.IsAny<Contact>()), Times.Never);
        }
    }
}
