using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpScoutNet.Model;
using HelpScoutNet.Request;

namespace HelpScoutNet.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HelpScoutClient("a9fe3bcbef4d989dc7d05f1b8b1f90359a23d61f");


            // 0. Get mailboxes
            var mailboxes = client.ListMailboxes();

            foreach (var mailbox in mailboxes.Items)
            {
                Console.WriteLine(mailbox.Email);

                // 1. Conversations (включая все атрибуты)
                var conversations = client.ListConversations(mailbox.Id);

                foreach (var conversation in conversations.Items)
                {
                    Console.WriteLine("Converstation with Preview {0} and with Status {1} which Created By {2} {3}", conversation.Preview, conversation.Status, conversation.CreatedBy.FirstName, conversation.CreatedBy.LastName);
                }
            }

            // 2. List of users (это кто от стороны компании отвечает на письма)
            var users = client.ListUsers();

            Console.ReadKey();
        }
    }
}
