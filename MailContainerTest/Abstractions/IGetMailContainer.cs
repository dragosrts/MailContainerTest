using MailContainerTest.Types;

namespace MailContainerTest.Abstractions
{
    public interface IGetMailContainer
    {
        public MailContainer GetMailContainer(string mailContainerNumber);
    }
}