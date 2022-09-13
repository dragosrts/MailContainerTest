using MailContainerTest.Types;

namespace MailContainerTest.Abstractions
{
    public interface IMailContainerValidator
    {
        public bool ValidateMailContainer(MakeMailTransferRequest request, MailContainer mailContainer);
    }
}