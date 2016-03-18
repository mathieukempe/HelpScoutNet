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
            // Create new conversation
            var newConv = client.CreateConversation(new Conversation
            {
                // We get Owner info from the Users List
                var users = client.ListUsers();
                {
                    public class Owner 
                    {
                        public int id { get; set; }
                        public string firstName { get; set; }
                        public string lastName { get; set; }
                        public string email { get; set; }
                        public object phone { get; set; }
                        public string type { get; set; }
                    }
                }

                // We get Mailbox info from the ListMailboxes
                var mailboxes = client.ListMailboxes();
                {
                    public class Mailbox
                    {
                        public int id { get; set; }
                        public string name { get; set; }
                    }
                }

                // We get Customer info from the Customer List
                var customer = client.ListCustomers();
                {
                    public class Customer
                    {
                        public int id { get; set; }
                        public string firstName { get; set; }
                        public string lastName { get; set; }
                        public string email { get; set; }
                        public object phone { get; set; }
                        public string type { get; set; }
                        public List<string> emails { get; set; }
                    }
                }

                public class CreatedBy
                {
                    public int id { get; set; }
                    public string firstName { get; set; }
                    public string lastName { get; set; }
                    public string email { get; set; }
                    public object phone { get; set; }
                    public string type { get; set; }
                    public List<string> emails { get; set; }
                }

                public class Source
                {
                    public string type { get; set; }
                    public string via { get; set; }
                }

                public class AssignedTo
                {
                    public int id { get; set; }
                    public string firstName { get; set; }
                    public string lastName { get; set; }
                    public string email { get; set; }
                    public object phone { get; set; }
                    public string type { get; set; }
                }

                public class Thread
                {
                    public int id { get; set; }
                    public string type { get; set; }
                    public string AssignedTo { get; set; }
                    public string status { get; set; }
                    public string createdAt { get; set; }
                    public object openedAt { get; set; }
                    public object actionType { get; set; }
                    public object actionSourceId { get; set; }
                    public object fromMailbox { get; set; }
                    public string state { get; set; }
                    public object customer { get; set; }
                    public string body { get; set; }
                    public object to { get; set; }
                    public List<object> cc { get; set; }
                    public List<object> bcc { get; set; }
                    public object attachments { get; set; }
                    public int savedReplyId { get; set; }
                    public bool createdByCustomer { get; set; }
                }

                public class Item
                {
                    public int id { get; set; }
                    public string type { get; set; }
                    public int folderId { get; set; }
                    public bool isDraft { get; set; }
                    public int number { get; set; }
                    public string Owner { get; set; }
                    public Mailbox { get; set; }
                    public Customer { get; set; }
                    public int threadCount { get; set; }
                    public string status { get; set; }
                    public string subject { get; set; }
                    public string preview { get; set; }
                    public string CreatedBy { get; set; }
                    public string createdAt { get; set; }
                    public string modifiedAt { get; set; }
                    public object closedAt { get; set; }
                    public object closedBy { get; set; }
                    public Source { get; set; }
                    public List<object> cc { get; set; }
                    public List<object> bcc { get; set; }
                    public object tags { get; set; }
                    public List<Thread> threads { get; set; }
                }
        });
        }
}
