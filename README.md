#HelpScout API - Export mailbox and import into new account

##Main Docs:
List of APIs (official APIs are in Java and PHP) - http://developer.helpscout.net/help-desk-api/conversations/list/
API in .NET - https://github.com/Selz/HelpScoutNet
Examples of JSON Calls - http://developer.helpscout.net/help-desk-api/conversations/list/

###Here's a rough outline:

1. Export conversation history via API from old mailbox. Backup that data. (/zack-convos.json)
2. Turn off forwarder from email client and delete old mailbox this deletion should free up the email address in our database so you can use it in your new account. (The client is EST business time and respond pretty quickly)
3. Import conversation history into new account.

* 17 Mar 
    * Helpscout support used a convo export script and sent details of all the conversations from the old mailbox in json-format (zack-convos.json)
* 18 Mar
    * List of users (should be setted manually via the dashboard) - there 2 of them: support@dfwnerdherd.com and david@arawrpc.com
    * List of customers/clients - the list of all customers doesn't contain all the customers in the conversations doc - so not sure they should be created manually.
    * There is no way to use the same ID for any item - all the IDs will be created
    * Looks like "createdAt", "closedAt" attributes are not working for Post Calls - it creates all the conversation using current date (I have notified client about it)
    



# HelpScoutNet
HelpScoutNet is a .NET class library that provides an easy-to-use interface for the helpscout.net web api

##Methods Implemented

###Help Desk API

* Conversation
    * List Conversations
    * Get Conversations
    * Get Attachment Data
    * Create Conversation
    * Update Conversation
    * Create Thread
    * Delete Conversation
    * Delete Note
    * Create Attachment
    * Delete Attachment
* Customers
   * List Customers
   * List Mailbox Customers
   * Get Customer
   * Create Customer
   * Update Customer
* Mailboxes
    * List Mailboxes
    * Get Mailbox
    * Get Folders
* Search
    * Conversations
    * Customers
* Tags
    * List Tags
* Users
    * List Users
    * Get User
    * List Users ny Mailbox
* Workflows
    * List Workflows
* Reports
   * Busy Time Statistics
   * Customer Statistics
   * Handle Time Statistics
   * Multiple Time Range Statistics (Conversations)
   * Multiple Time Range Statistics (Productivity)
   * Multiple Time Range Statistics (Team)
   * Multiple Time Range Statistics (User)
   * New Conversation Statistics
   * Replies to Resolve Statistics
   * Response Time Statistics
   * Saved Reply Statistics
   * Tag Statistics
   * Time Range Statistics (Conversations)
   * Time Range Statistics (Productivity)
   * Time Range Statistics (Team)
   * Time Range Statistics (User)
   * User Statistics (Team)
   * Workflow Statistics


###Docs API

Nothing done yet

##Examples 

###Initialization of the client
```csharp
var client = new HelpScoutClient(ApiKey);
```
###Search customers
```csharp

var client = new HelpScoutClient(ApiKey);
var customersSearch = client.SearchCustomers(new SearchRequest{ Query = "(customer:\"johnappleseed@gmail.com\")"});
foreach (var searchresult in customersSearch.Items)
{
    Console.WriteLine(searchresult.FirstName + searchresult.LastName);   
}

```

###List Mailboxes
```csharp

var mailboxes = client.ListMailboxes();
foreach (var mailboxStub in mailboxes.Items)
{    
    Console.WriteLine(mailboxStub.Id);
}
  
```

###Create conversation
```csharp
var newConv = client.CreateConversation(new Conversation
                {
                    Type = ConversationType.email,
                    Subject = "Testing the Helpscout API Mathieu",
                    Mailbox = new MailboxRef
                    {
                        Id = 38556
                    },
                    Status = ConversationStatus.active,
                    Customer = new Person
                    {
                        Email = "johnappleseed@somemail.com",
                    },

                    Threads = new List<Thread>{
                        new Thread
                        {
                            Type = ThreadType.message,
                            Body = "This is the body of the email \n something else" + Environment.NewLine + "and again",
                            Status = ThreadStatus.active,
                            CreatedBy = new Person
                            {    
                                Id = 60895,
                                Type = PersonType.user,
                                Email = "mathieu@somemail.com"
                            } 
                        }
                     }

                });

```
### Delete a conversation
```csharp
client.DeleteConversation(111947647);
```

###Add a note, create thread
```csharp
var thread = client.CreateThread(newconv.Id, new Thread
                {
                    CreatedBy = new Person
                    {
                        Id = 60895,
                        Type = PersonType.user,
                        Email = "mathieu@somemail.com"
                    },
                    Type = ThreadType.note,
                    Body = "This is a note from API",
                    Status = ThreadStatus.active
                });
```

###Create an attachment
```csharp

Byte[] bytes = File.ReadAllBytes(@"C:\Users\mathieu.kempe.SELZ\Desktop\sift-logo.png");
String file = Convert.ToBase64String(bytes);

string fileHash = client.CreateAttachment(new CreateAttachmentRequest
                                          {
                                             FileName = "sift-logo.png",
                                             MimeType = "image/png",
                                             Data = file
                                          });

```

To add the attachment to a new conversation

```csharp
 var newConv = client.CreateConversation(new Conversation
  {
      ...
      Threads = new List<Thread>{
                        new Thread
                        {
                            ...
                            Attachments = new List<Attachment>
                            {
                                new Attachment
                                {
                                    Hash = fileHash
                                }
                            }
                        }
                     }
               });
               
```


###Field Selectors 

Each endpoint returns a default set of fields based upon the given request. However, you can override this behavior by supplying one or more field selectors to explicitly request the data you need.

Instead of returning a complete customer object, you could return just the ID and lastname.

```csharp

client.GetCustomer(123, new CustomerRequest {Fields = new[] {"id", "lastName"}});
  
```
