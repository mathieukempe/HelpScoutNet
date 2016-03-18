using HelpScoutNet.Model;

namespace HelpScoutNet.ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new HelpScoutClient("a9fe3bcbef4d989dc7d05f1b8b1f90359a23d61f");

            // We get Owner info from the Users List
            var users = client.ListUsers();

            // We get Mailbox info from the ListMailboxes
            var mailboxes = client.ListMailboxes();

            // We get Customer info from the Customer List
            var customer = client.ListCustomers();

            // Create new conversation
            var newConv = client.CreateConversation(new Conversation());
        }
    }
}