using FluentAssertions;
using MailContainerTest.Types;
using MailContainerTest.Validators;
using Xunit;

namespace MailContainerTest.Tests.Validators
{
    public class MailContainerValidatorTests
    {
        [Fact]
        public void ValidateMailContainer_WhenRequestNull_ThenReturnsFalse()
        {
            // Arrange
            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.StandardLetter
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(null, mailContainer);

            // Assert
            validationResult.Should().BeFalse();
        }

        [Fact]
        public void ValidateMailContainer_WhenMailContainerNull_ThenReturnsFalse()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.StandardLetter,
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(request, null);

            // Assert
            validationResult.Should().BeFalse();
        }

        [Fact]
        public void ValidateMailContainer_WhenRequestMailTypeUnspecified_ThenReturnsFalse()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.Unspecified,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.StandardLetter
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(request, mailContainer);

            // Assert
            validationResult.Should().BeFalse();
        }

        [Fact]
        public void ValidateMailContainer_WhenMailContainerAllowedMailTypeUnspecified_ThenReturnsFalse()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.StandardLetter,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.Unspecified
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(request, mailContainer);

            // Assert
            validationResult.Should().BeFalse();
        }

        [Fact]
        public void ValidateMailContainer_WhenRequestMailTypeStandardLetter_AndMailContainerAllowedMailTypeStandardLetter_ThenReturnsTrue()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.StandardLetter,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.StandardLetter
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(request, mailContainer);

            // Assert
            validationResult.Should().BeTrue();
        }

        [Fact]
        public void ValidateMailContainer_WhenRequestMailTypeStandardLetter_AndMailContainerAllowedMailTypeLargeLetter_ThenReturnsFalse()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.StandardLetter,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.LargeLetter,
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(request, mailContainer);

            // Assert
            validationResult.Should().BeFalse();
        }

        [Fact]
        public void ValidateMailContainer_WhenMailTypeIsLargeLetter_AndMailItemsEqualCapacity_ThenReturnsTrue()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.LargeLetter,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.LargeLetter,
                    Capacity = 1,
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(request, mailContainer);

            // Assert
            validationResult.Should().BeTrue();
        }

        [Fact]
        public void ValidateMailContainer_WhenMailTypeIsSmallParcel_AndMailContainerStatusIsOperational_ThenReturnsTrue()
        {
            // Arrange
            MakeMailTransferRequest request
                = new MakeMailTransferRequest
                {
                    SourceMailContainerNumber = "",
                    DestinationMailContainerNumber = "",
                    NumberOfMailItems = 1,
                    TransferDate = DateTime.Now,
                    MailType = MailType.LargeLetter,
                };

            MailContainer mailContainer
                = new MailContainer
                {
                    AllowedMailType = AllowedMailType.LargeLetter,
                    Capacity = 1,
                    Status = MailContainerStatus.Operational,
                };

            var mailContainerValidator = new MailContainerValidator();

            // Act
            var validationResult = mailContainerValidator.ValidateMailContainer(request, mailContainer);

            // Assert
            validationResult.Should().BeTrue();
        }
    }
}