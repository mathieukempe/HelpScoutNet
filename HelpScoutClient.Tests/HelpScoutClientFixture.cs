using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bogus;
using HelpScout.Conversations.Threads.Models.Create;
using HelpScout.Customers;
using Xunit;
using Xunit.Abstractions;
using ConversationCreate = HelpScout.Conversations.Models.Create;

namespace HelpScout.Tests
{
    public class HelpScoutClientFixture
    {
        public HelpScoutClientFixture()
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            this.Credentials = new SecuredCredentials();
            this.ApiClient = new HelpScoutApiClient(this.Credentials);
            ApiClient.GetToken(true).Wait();
            Faker = new Faker();
        }


        public ICredentials Credentials { get; }
        public HelpScoutApiClient ApiClient { get; }

        public Faker Faker { get; }

        public ConversationCreate.ConversationCreateRequest ConversationCreateRequest()
        {
            var subject = Faker.Commerce.ProductMaterial();
            var body = Faker.Rant.Review();
            var note = "NOTE: " + Faker.Rant.Review();

            var req = new ConversationCreate.ConversationCreateRequest
            {
                MailboxId = Constants.MailBoxId,
                Type = ConversationCreate.ConversationType.Email,
                Subject = subject,
                Tags = new List<string> { "high-priority" },
                Status = ConversationCreate.ConversationStatus.Pending,
                Customer = new ConversationCreate.Customer
                {
                    Email = "arjuns@selz.com"
                },
                Threads = new List<ConversationCreate.ThreadCreateRequest>
                {
                    new ConversationCreate.ThreadCreateRequest
                    {
                        Type = ConversationCreate.ThreadType.Note,
                        Text = note
                    },
                    new ConversationCreate.ThreadCreateRequest
                    {
                        Type = ConversationCreate.ThreadType.Customer,
                        Text = body,
                        Customer = new ConversationCreate.CreateConservationThreadCustomer
                        {
                            Email = "arjuns@selz.com"
                        },
                        Attachments = new List<Attachment>
                        {
                            GetAttachment().Result,
                            GetAttachment().Result
                        }
                        
                        
                    }
                }
            };
            return req;
        }

        public CustomerCreateRequest CreateCustomerRequest()
        {
            var person = Faker.Person;
            return new CustomerCreateRequest
            {
                Age = "41",
                Background = Faker.Rant.Review("customer"),
                CreatedAt = DateTime.UtcNow,
                Emails = new List<CustomerCreateRequest.Email>
                {
                    new CustomerCreateRequest.Email {Type = "work", Value = Faker.Internet.Email()},
                    new CustomerCreateRequest.Email {Type = "home", Value = Faker.Person.Email}
                },
                FirstName = person.FirstName,
                Gender = person.Gender.ToString(),
                JobTitle = "Sr. Business Developer",
                LastName = person.LastName,
                Location = Faker.Address.FullAddress(),
                Organization = Faker.Company.CompanyName(),
                PhotoType = Faker.Random.Enum<CustomerCreateRequest.CustomerPhotoType>(),
                PhotoUrl = Faker.Person.Avatar
            };
        }

        private async Task<Attachment> GetAttachment()
        {
            var picsumUrl = Faker.Image.PicsumUrl();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(picsumUrl);

            var ms = new MemoryStream();
            await response.Content.CopyToAsync(ms);
            return new Attachment
            {
                FileName = Faker.System.FileName(),
                MimeType = response.Content.Headers.GetValues("Content-Type").FirstOrDefault(),
                Data = Convert.ToBase64String(ms.ToArray())
            };
        }

    }
}