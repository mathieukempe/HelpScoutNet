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
                // 1. Conversations (включая все атрибуты)
                var conversations = client.ListConversations(mailbox.Id);
            }

            // 2. List of users (это кто от стороны компании отвечает на письма)
            var users = client.ListUsers();

            Console.WriteLine(mailboxes.Items);
            Console.ReadKey();
        }
    }
}
