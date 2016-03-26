using System.Collections.Generic;

namespace HelpScoutNet.ConsoleClient.Models
{
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
}