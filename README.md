[![Build status](https://ci.appveyor.com/api/projects/status/yoxiw6cx9ehbgf66?svg=true)](https://ci.appveyor.com/project/SelzEngineer/helpscout)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FSelz%2FHelpScoutNet.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FSelz%2FHelpScoutNet?ref=badge_shield)



# HelpScoutNet
HelpScoutNet is a .NET class library that provides an easy-to-use interface for the helpscout.net v2.0 web api

## Endpoints Implemented

### Help Desk API

* **Conversation**
* **Customers**
* **Mailboxes**
* **Tags**
* **Users**
* **Workflows**

### Docs API

Nothing done yet

## Examples 

### Initialization of the client
```csharp
var client=new HelpScoutApiClient("clientId","clientSecret");
//initialize token from the given credentials
await client.GetToken(true);
```
### Customer Endpoints

**List Customer**

```csharp
//search customer by their first name, pass null to list 
var searchQuery = new CustomerSearchQuery(){FirstName = "Otis" };
var pagedCustomer = await client.Customers.List(searchQuery);
foreach (var customer in pagedCustomer.Items)
{
    Console.WriteLine("First Name :{0}, Last Name: {1}",customer.FirstName,customer.LastName);
}
```

**Get Customer Detail**

```csharp
//search customer by their first name, pass null to list 
var customerDetail= await client.Customers.Get(id);
Console.WriteLine($"First Name:{customerDetail.FirstName}");
foreach (var email in customerDetail.Embedded.Emails)
{
    Console.WriteLine($"Email:{email.Value}, Type:{email.Type}");
           
}
```

### List Mailboxes
```csharp
  var mailBoxList = await client.Mailboxes.List();
  foreach (var mailBox in mailBoxList.Items)
  {
    Console.WriteLine($"{mailBox.Name}");
  }
```

### Create conversation
```csharp
 var req = new ConversationCreateRequest
       {
                MailboxId = 177024,
                Type = ConversationType.Email,
                Subject = "Hey there!",
                Tags = new List<string> { "high-priority" },
                Status = ConversationStatus.Pending,
                Customer = new Customer
                {
                    Email = "arjuns@selz.com"
                },
                Threads = new List<ThreadCreateRequest>
                {
                   //add a note to this conversation
                    new ThreadCreateRequest
                    {
                        Type = ThreadType.Note,
                        Text = "Can you please resolve this issue asap.",
                    },
                    new ThreadCreateRequest
                    {
                        Type = ThreadType.Customer,
                        Text = "This issue keeps happening.",
                        Customer = new CreateConservationThreadCustomer
                        {
                            Email = "arjuns@selz.com"
                        }
                    }
                }
            };
 var conversationId = await client.Conversations.Create(req);
 Console.WriteLine($"Created Conversation Id: {conversationId}");

```
### Get conversation detail
```csharp
  var conversationDetail = await client.Conversations.Get(conversationId);
  Console.WriteLine($"Subject: {conversationDetail.Subject}");
```

### Delete a conversation
```csharp
await client.Conversations.Delete(conversationId);
```

### Creating Threads
```csharp
var threadEndpoint = client.Conversations.Endpoints.Threads(conversationId);
var customerThread = new CreateThreadRequest()
{
 Text = "This is a customer thread"
};
await threadEndpoint.CreateCustomerThread(customerThread,customerId);
//Similarly, Chat, Phone, Reply threads can be created
 

 var noteThread = new CreateThreadRequest()
{
 Text = "This is a note thread"
 
 Attachments=new List<Attachment>{
     new Attachment{
         FileName="Some attachment name",
         MimeType="image/png",
         Data=Convert.ToBase64String(File.ReadAllBytes("C:\\some-attachment.png"))
     }
 }
};
//note thread does not require customerId 
 await threadEndpoint.CreateNoteThread(noteThread);

  
```


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FSelz%2FHelpScoutNet.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FSelz%2FHelpScoutNet?ref=badge_large)