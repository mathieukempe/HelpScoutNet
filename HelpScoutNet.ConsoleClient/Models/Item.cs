using System.Collections.Generic;

namespace HelpScoutNet.ConsoleClient.Models
{
    public class Item
    {
        public int id { get; set; }
        public string type { get; set; }
        public int folderId { get; set; }
        public bool isDraft { get; set; }
        public int number { get; set; }
        public string Owner { get; set; }
        public Mailbox Mailbox { get; set; }
        public Customer Customer { get; set; }
        public int threadCount { get; set; }
        public string status { get; set; }
        public string subject { get; set; }
        public string preview { get; set; }
        public string CreatedBy { get; set; }
        public string createdAt { get; set; }
        public string modifiedAt { get; set; }
        public object closedAt { get; set; }
        public object closedBy { get; set; }
        public Source Source { get; set; }
        public List<object> cc { get; set; }
        public List<object> bcc { get; set; }
        public object tags { get; set; }
        public List<Thread> threads { get; set; }
    }
}