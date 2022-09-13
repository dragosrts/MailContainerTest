using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Validators
{
    public class MailContainerValidator : IMailContainerValidator
    {
        public bool ValidateMailContainer(MakeMailTransferRequest request, MailContainer mailContainer)
        {
            if (request is null)
                return false;

            if (request.MailType == MailType.Unspecified)
                return false;

            if (mailContainer is null)
                return false;

            if (mailContainer.AllowedMailType == AllowedMailType.Unspecified)
                return false;

            return request.MailType switch
            {
                MailType.StandardLetter =>
                    mailContainer.AllowedMailType.HasFlag(AllowedMailType.StandardLetter),

                MailType.LargeLetter =>
                    mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) &&
                    mailContainer.Capacity >= request.NumberOfMailItems,

                MailType.SmallParcel =>
                    mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) &&
                    mailContainer.Status == MailContainerStatus.Operational,

                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}