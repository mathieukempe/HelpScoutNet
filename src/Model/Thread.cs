using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HelpScoutNet.Model
{
    public class Thread
    {
        public double Id { get; set; }
        public Person AssignedTo { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ThreadStatus Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Person CreatedBy { get; set; }
        public Source Source { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ThreadType Type { get; set; }
        public ThreadState State { get; set; }
        public Person Customer { get; set; }
        public MailboxRef FromMailbox { get; set; }
        public string Body { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<string> Tags { get; set; }
    }

    public enum ThreadState
    {
        published,
        draft,
        underreview,
        hidden
    }

    public enum ThreadStatus
    {
        nochange,
        active,
        pending,
        closed,
        spam
    }

    public enum ThreadType
    {
        lineitem,
        note,
        message,
        chat,
        customer,
        forwardparent,
        forwardchild,
        phone
    }
}
