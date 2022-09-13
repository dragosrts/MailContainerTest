using FluentAssertions;
using MailContainerTest.Abstractions;
using MailContainerTest.Services;
using MailContainerTest.Types;
using Moq;
using Xunit;

namespace MailContainerTest.Tests.Services
{
    public class MailTransferServiceTests
    {
        [Fact]
        public void MakeMailTransfer_WhenValidationIsTrue_ReturnsSuccess()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    TransferDate = DateTime.Now,
                    NumberOfMailItems = 1,
                    MailType = MailType.StandardLetter,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    MailContainerNumber = "",
                    Capacity = 1,
                    Status = MailContainerStatus.Operational,
                    AllowedMailType = AllowedMailType.StandardLetter,
                };

            var mailContainerValidatorMock = new Mock<IMailContainerValidator>();
            var containerDataStoreFactoryMock = new Mock<IMailContainerDataStoreFactory>();
            IMailTransferService transferService = new MailTransferService(containerDataStoreFactoryMock.Object, mailContainerValidatorMock.Object);

            var mailContainerDataStore = new Mock<IMailContainerDataStore>();
            mailContainerDataStore
                .Setup(x => x.GetMailContainer(It.IsAny<string>()))
                .Returns(mailContainer);

            containerDataStoreFactoryMock
                .Setup(x => x.CreateMailContainerDataStore())
                .Returns(mailContainerDataStore.Object);

            mailContainerValidatorMock
                .Setup(x => x.ValidateMailContainer(request, mailContainer))
                .Returns(true);

            // Act
            var result = transferService.MakeMailTransfer(request);

            // Assert
            result.Success.Should().BeTrue();
            mailContainer.Capacity.Should().Be(0);
            mailContainerDataStore.Verify(x => x.UpdateMailContainer(It.IsAny<MailContainer>()), Times.Once());
        }

        [Fact]
        public void MakeMailTransfer_WhenValidationIsFalse_ReturnsFailed()
        {
            // Arrange

            MakeMailTransferRequest request =
                new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.SmallParcel,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.StandardLetter,
                    Capacity = 1,
                    MailContainerNumber = "",
                    Status = MailContainerStatus.Unspecified,
                };

            var mailContainerValidatorMock = new Mock<IMailContainerValidator>();
            var containerDataStoreFactoryMock = new Mock<IMailContainerDataStoreFactory>();
            IMailTransferService transferService = new MailTransferService(containerDataStoreFactoryMock.Object, mailContainerValidatorMock.Object);

            var mailContainerDataStore = new Mock<IMailContainerDataStore>();
            mailContainerDataStore
                .Setup(x => x.GetMailContainer(It.IsAny<string>()))
                .Returns(mailContainer);

            containerDataStoreFactoryMock
                .Setup(x => x.CreateMailContainerDataStore())
                .Returns(mailContainerDataStore.Object);

            mailContainerValidatorMock
                .Setup(x => x.ValidateMailContainer(request, mailContainer))
                .Returns(false);

            // Act
            var result = transferService.MakeMailTransfer(request);

            // Assert
            result.Success.Should().BeFalse();
            mailContainer.Capacity.Should().Be(1);
            mailContainerDataStore.Verify(x => x.UpdateMailContainer(It.IsAny<MailContainer>()), Times.Never());
        }
    }
}