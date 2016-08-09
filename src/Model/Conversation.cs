using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class MailboxRef
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Person
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public PersonType Type { get; set; }
    }

    public enum PersonType
    {
        user,
        customer
    }

    public class Source
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public SourceType Type { get; set; }
        public string Via { get; set; }
    }

    public enum SourceType
    {
        email,
        web,
        notification,
        emailfwd,
        api,
        chat,
        workflows,
        docs
    }
    
    public class Attachment
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Hash { get; set; }
        public string MimeType { get; set; }
        public string Filename { get; set; }
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
    }



    public enum ConversationType
    {
        email,
        chat,
        phone
    }

    public enum ConversationStatus
    {
        active,
        pending,
        closed,
        spam
    }

    public class Conversation
    {
        public int Id { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ConversationType Type { get; set; }
        public string Folder { get; set; }
        public string IsDraft { get; set; }
        public int Number { get; set; }
        public Person Owner { get; set; }
        public MailboxRef Mailbox { get; set; }
        public Person Customer { get; set; }
        public int ThreadCount { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ConversationStatus Status { get; set; }
        public string Subject { get; set; }
        public string Preview { get; set; }
        public Person CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public Person ClosedBy { get; set; }
        public Source Source { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public List<string> Tags { get; set; }
        public List<Thread> Threads { get; set; }
    }
}
