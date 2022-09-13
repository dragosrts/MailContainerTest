using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        private readonly IMailContainerValidator mailContainerValidator;
        private readonly IMailContainerDataStoreFactory mailContainerDataStoreFactory;

        public MailTransferService(IMailContainerDataStoreFactory mailContainerDataStoreFactory, IMailContainerValidator mailContainerValidator)
        {
            this.mailContainerDataStoreFactory = mailContainerDataStoreFactory;
            this.mailContainerValidator = mailContainerValidator;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var mailContainerDataStore = mailContainerDataStoreFactory.CreateMailContainerDataStore();
            var mailContainer = mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);

            var result
                = new MakeMailTransferResult
                {
                    Success = mailContainerValidator.ValidateMailContainer(request, mailContainer),
                };

            if (result.Success)
            {
                mailContainer.Capacity -= request.NumberOfMailItems;
                mailContainerDataStore.UpdateMailContainer(mailContainer);
            }

            return result;
        }
    }
}