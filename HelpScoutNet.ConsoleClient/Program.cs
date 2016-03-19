using System;
using System.Collections.Generic;
using System.IO;
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
            //Testing account
            var client = new HelpScoutClient("a9fe3bcbef4d989dc7d05f1b8b1f90359a23d61f");
            // Create new converstion
            var newConv = client.CreateConversation(new Conversation
            {
                Type = ConversationType.email,
                Subject = "New Call from WIRELESS CALLER (618) 967-6453",
                Mailbox = new MailboxRef
                {
                    // Constant data
                    Id = 69294,
                    Name = "info"
                },
                // The status can be: active, pending, closed,spam
                Status = ConversationStatus.active,
                Customer = new Person
                {
                    Email = "mefa@sbcglobalabc.net",
                },

                Threads = new List<Thread>{
                        new Thread
                        {
                            Type = ThreadType.message,
                            Body = "Hey! How're you doing? When can I pickup my phone?",
                            Status = ThreadStatus.active,
                            CreatedBy = new Person
                            {
                                // Customer is from our system. If we use ID of the user (112364) it will work.
                                Id = 76391108,
                                // It can be: user, customer
                                Type = PersonType.customer,
                                FirstName = "Azure",
                                LastName = "Azure",
                                Phone = "null"
                            }
                        },
                        new Thread
                        {
                            Type = ThreadType.message,
                            Body = "I will notify you tomorrow morning once it has been made available for pickup. Thank you for the business!",
                            Status = ThreadStatus.active,
                            CreatedBy = new Person
                            {
                                // User from our system
                                Id = 112364,
                                Type = PersonType.user,
                                Email = "a@alinadev.com",
                                FirstName = "Alice",
                                LastName = "Jonson",
                                Phone = "null"
                            }
                        },
                        new Thread
                        {
                            Type = ThreadType.message,
                            Body = "Ok, thank you! Waiting for your updates",
                            Status = ThreadStatus.active,
                            CreatedBy = new Person
                            {
                                // Customer is from our system. If we use ID of the user (112364) it will work.
                                Id = 76391108,
                                Type = PersonType.customer,
                                FirstName = "Azure",
                                LastName = "Azure",
                                Phone = "null"                            
                            }
                        },
                        new Thread
                        {
                            Type = ThreadType.message,
                            Body = "Great news, your device is now available for pickup. <br/><br/>M-F 8-7p, Sat 9a-5p<br/><br/>Thank you again for the business. Please tell your friends about us. <br/>",
                            Status = ThreadStatus.active,
                            CreatedBy = new Person
                            {
                                // User from our system
                                Id = 112364,
                                Type = PersonType.user,
                                Email = "a@alinadev.com",
                                FirstName = "Alice",
                                LastName = "Jonson",
                                Phone = "null"
                            }
                        }

                    }
            });
        }
    }
}