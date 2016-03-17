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
            var conversations = client.ListConversations();
            foreach (var conversation in ConversationRequest.Id)
            {
                var mailboxId = 69294;
                Console.WriteLine(Conversation.Owner, Conversation.Number, Conversation.thread);
                Console.ReadKey();
            }
        }
    }
}
