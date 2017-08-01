using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Text;
using HelpScoutNet.Model;
using HelpScoutNet.Model.Report;
using HelpScoutNet.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using HelpScoutNet.Request.Report;
using System.Threading.Tasks;
using System.Net;

namespace HelpScoutNet
{
    public sealed class HelpScoutClient
    {
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.helpscout.net/v1/";
        private int timeoutSeconds = 0;

        private JsonSerializerSettings _serializerSettings
        {
            get
            {
                var serializer = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                };
                serializer.Converters.Add(new StringEnumConverter { CamelCaseText = true });

                return serializer;
            }
        }

        public HelpScoutClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public HelpScoutClient(string apiKey, int timeoutSeconds)
        {
            _apiKey = apiKey;
            this.timeoutSeconds = timeoutSeconds;
        }

        #region Mailboxes
        public Paged<Mailbox> ListMailboxes(PageRequest requestArg = null)
        {
            return Get<Paged<Mailbox>>("mailboxes.json", requestArg);
        }

        public Mailbox GetMailbox(int mailboxId, FieldRequest requestArg = null)
        {
            var singleItem = Get<Mailbox>(string.Format("mailboxes/{0}.json", mailboxId), requestArg);

            return singleItem;
        }

        public Paged<Folder> GetFolder(int folderId, PageRequest requestArg = null)
        {
            return Get<Paged<Folder>>(string.Format("/mailboxes/{0}/folders.json", folderId), requestArg);
        }
        #endregion

        #region Conversations

        public Paged<Conversation> ListConversations(int mailboxId, ConversationRequest requestArg = null)
        {

            string endpoint = string.Format("mailboxes/{0}/conversations.json", mailboxId);

            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Paged<Conversation> ListConversationsInFolder(int mailboxId, int folderId, ConversationRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/folders/{1}/conversations.json", mailboxId, folderId);

            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Paged<Conversation> ListConversationsForCustomer(int mailboxId, int customerId, ConversationRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/customers/{1}/conversations.json", mailboxId, customerId);

            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Paged<Conversation> ListConversationsForUser(int mailboxId, int userId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/customers/{1}/conversations.json", mailboxId, userId);
            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Conversation GetConversation(int conversationId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("conversations/{0}.json", conversationId);
            return Get<SingleItem<Conversation>>(endpoint, requestArg).Item;
        }

        public Attachment GetAttachement(int conversationId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("attachments/{0}/data.json", conversationId);
            return Get<SingleItem<Attachment>>(endpoint, requestArg).Item;
        }

        public Conversation CreateConversation(Conversation conversation, bool imported = false, bool autoReply = false, bool reload = true)
        {
            string endpoint = "conversations.json";
            var result = Post(endpoint, conversation, new CreateCustomerRequest { AutoReply = autoReply, Reload = reload, Imported = imported });

            return result?.Item;
        }

        public Conversation UpdateConversation(Conversation conversation, bool reload = true)
        {
            string endpoint = string.Format("conversations/{0}.json", conversation.Id);
            var result = Put(endpoint, conversation, new PostOrPutRequest { Reload = reload });

            return result?.Item;
        }

        public void DeleteConversation(int id)
        {
            string endpoint = string.Format("conversations/{0}.json", id);

            Delete(endpoint);
        }

        public void DeleteNote(int id)
        {
            string endpoint = string.Format("notes/{0}.json", id);

            Delete(endpoint);
        }

        public Thread CreateThread(int conversationId, Thread thread, bool imported = false, bool reload = true)
        {
            string endpoint = string.Format("conversations/{0}.json", conversationId);

            var result = Post(endpoint, thread, new PostOrPutRequest { Reload = reload, Imported = imported });

            return result?.Item;
        }

        public string CreateAttachment(CreateAttachmentRequest request)
        {
            string endpoint = "attachments.json";

            return PostAttachment(endpoint, request);
        }

        public void DeleteAttachment(string id)
        {
            string endpoint = string.Format("attachments/{0}.json", id);

            Delete(endpoint);
        }

        #endregion

        #region Customers

        public Paged<Customer> ListCustomers(CustomerRequest requestArg = null)
        {
            string endpoint = "customers.json";

            return Get<Paged<Customer>>(endpoint, requestArg);
        }

        public Paged<Customer> ListCustomers(int mailboxId, CustomerRequest requestArg = null)
        {
            string endpoint = string.Format("mailbox/{0}/customers.json", mailboxId);

            return Get<Paged<Customer>>(endpoint, requestArg);
        }

        public Customer GetCustomer(int customerId, CustomerRequest requestArg = null)
        {
            string endpoint = string.Format("customers/{0}.json", customerId);

            return Get<SingleItem<Customer>>(endpoint, requestArg).Item;
        }

        /// <summary>
        /// Create customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="reload">if true return the new customer otherwise return the original customer</param>
        /// <returns>if reload is true return the new customer otherwise return the original customer</returns>
        public Customer CreateCustomer(Customer customer, bool reload = true)
        {
            string endpoint = "customers.json";

            var result = Post(endpoint, customer, new PostOrPutRequest { Reload = reload });

            return result?.Item;
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customerId">the id of the customer to update</param>
        /// <param name="customer">customer data to update</param>
        /// <param name="reload">if true return the new customer otherwise return the original customer</param>
        /// <returns>if reload is true return the new customer otherwise return the original customer</returns>
        public Customer UpdateCustomer(int customerId, Customer customer, bool reload = true)
        {
            string endpoint = string.Format("customers/{0}.json", customerId);

            var result = Put(endpoint, customer, new PostOrPutRequest { Reload = reload });

            return result?.Item;
        }
        #endregion

        #region Search

        public Paged<SearchConversation> SearchConversations(SearchRequest requestArg = null)
        {
            string endpoint = "search/conversations.json";

            return Get<Paged<SearchConversation>>(endpoint, requestArg);
        }

        public Paged<SearchCustomer> SearchCustomers(SearchRequest requestArg = null)
        {
            string endpoint = "search/customers.json";

            return Get<Paged<SearchCustomer>>(endpoint, requestArg);
        }


        #endregion

        #region Tag

        public Paged<HelpScoutNet.Model.Tag> ListTags(PageRequest requestArg = null)
        {
            string endpoint = "tags.json";

            return Get<Paged<HelpScoutNet.Model.Tag>>(endpoint, requestArg);
        }

        #endregion

        #region Users

        public Paged<HelpScoutNet.Model.User> ListUsers(PageRequest requestArg = null)
        {
            string endpoint = "users.json";

            return Get<Paged<HelpScoutNet.Model.User>>(endpoint, requestArg);
        }

        public HelpScoutNet.Model.User GetUser(int userId, FieldRequest requestArg)
        {
            string endpoint = string.Format("users/{0}.json", userId);

            return Get<SingleItem<User>>(endpoint, requestArg).Item;
        }

        public HelpScoutNet.Model.User GetMe(FieldRequest requestArg)
        {
            string endpoint = "users/me.json";

            return Get<SingleItem<User>>(endpoint, requestArg).Item;
        }

        public Paged<HelpScoutNet.Model.User> ListUserPerMailbox(int mailboxId, FieldRequest requestArg)
        {
            string endpoint = string.Format("mailboxes/{0}/users.json", mailboxId);

            return Get<Paged<HelpScoutNet.Model.User>>(endpoint, requestArg);
        }

        #endregion

        #region Workflows

        public Paged<Workflow> ListWorkflows(int mailboxId, FieldRequest requestArg)
        {
            string endpoint = string.Format("mailboxes/{0}/workflows.json", mailboxId);

            return Get<Paged<Workflow>>(endpoint, requestArg);
        }

        #endregion

        #region Reports

        #region Users
        public Model.Report.User.UserReports.UserReport GetUserOverallReport(Request.Report.User.UserRequest requestArg)
        {
            string endpoint = string.Format("reports/user.json");
            return Get<SingleItem<Model.Report.User.UserReports.UserReport>>(endpoint, requestArg).Item;
        }

        public Model.Report.PagedReport<Model.Report.User.ConversationStats> GetUserConversationHistory(Request.Report.User.UserPagedRequest requestArg)
        {
            string endpoint = string.Format("reports/user/conversation-history.json");
            return Get<SingleItem<Model.Report.PagedReport<Model.Report.User.ConversationStats>>>(endpoint, requestArg).Item;
        }

        public Model.Report.Common.CustomersDatesAndCounts GetUserCustomersHelped(Request.Report.User.UserViewByRequest requestArg)
        {
            string endpoint = string.Format("reports/user/customers-helped.json");
            return Get<SingleItem<Model.Report.Common.CustomersDatesAndCounts>>(endpoint, requestArg).Item;
        }

        public Model.Report.Common.RepliesDatesAndCounts GetUserReplies(Request.Report.User.UserViewByRequest requestArg)
        {
            string endpoint = string.Format("reports/user/replies.json");
            return Get<SingleItem<Model.Report.Common.RepliesDatesAndCounts>>(endpoint, requestArg).Item;
        }

        public Model.Report.Common.ResolvedDatesAndCounts GetUserResolved(Request.Report.User.UserViewByRequest requestArg)
        {
            string endpoint = string.Format("reports/user/resolutions.json");
            return Get<SingleItem<Model.Report.Common.ResolvedDatesAndCounts>>(endpoint, requestArg).Item;
        }

        public Model.Report.User.UserHappiness GetUserHappiness(Request.Report.User.UserRequest requestArg)
        {
            string endpoint = string.Format("reports/user/happiness.json");
            return Get<SingleItem<Model.Report.User.UserHappiness>>(endpoint, requestArg).Item;
        }

        public Model.Report.PagedReport<Model.Report.Common.Rating> GetUserRatings(Request.Report.User.UserRatingsRequest requestArg)
        {
            string endpoint = string.Format("reports/user/ratings.json");
            return Get<PagedReport<Model.Report.Common.Rating>>(endpoint, requestArg);
        }

        private void GetUserDrillDown()
        {
            //Not Implimented
        }

        #endregion

        #region Conversations

        public Model.Report.Conversations.ConversationsReport GetConversationsOverall(Request.Report.CompareRequest requestArg)
        {
            string endpoint = string.Format("reports/conversations.json");
            return Get<SingleItem<Model.Report.Conversations.ConversationsReport>>(endpoint, requestArg).Item;
        }

        private void GetNewConversations()
        {
            //Not Implimented
        }

        private void GetConversationsDrillDown()
        {
            //Not Implimented
        }

        private void GetConversationsDrillDownByField()
        {
            // not Implimented
        }

        private void GetNewConversationsDrillDown()
        {
            // not Implimented
        }

        #endregion

        #region Team

        public Model.Report.Team.TeamReport GetTeamOverall(Request.Report.CompareRequest requestArg)
        {
            string endpoint = string.Format("reports/team.json");
            return Get<SingleItem<Model.Report.Team.TeamReport>>(endpoint, requestArg).Item;
        }

        private void GetTeamCustomersHelped()
        {
            //Not Implimented
        }

        private void GetTeamDrillDown()
        {
            //Not Implimented
        }

        #endregion

        #region Happiness

        private Model.Report.Happiness.HappinessReport GetHappinessOverall(Request.Report.CompareRequest requestArg)
        {
            string endpoint = string.Format("reports/happiness.json");
            return Get<SingleItem<Model.Report.Happiness.HappinessReport>>(endpoint, requestArg).Item;
        }

        private Paged<Model.Report.Common.Rating> GetHappinessRatings(Request.Report.PagedRatingsRequest requestArg)
        {
            string endpoint = string.Format("reports/happiness/ratings.json");
            return Get<Paged<Model.Report.Common.Rating>>(endpoint, requestArg);
        }

        #endregion

        #region Productivity

        private void GetProductivityOverall()
        {
            //Not Implimented
        }

        private void GetProductivityFirstResponseTime()
        {
            //Not Implimented
        }


        private void GetProductivityRepliesSent()
        {
            //Not Implimented
        }

        private void GetProductivityResolved()
        {
            
        }

        private void GetProductivityResolutionTime()
        {
            //Not Implimented
        }

        private void GetProductivityResponseTime()
        {
            //Not Implimented
        }

        private void GetProductivityDrillDown()
        {
            //Not Implimented
        }

        #endregion

        #endregion


        private T Get<T>(string endpoint, IRequest request) where T : class
        {
            var client = InitHttpClient();

            HttpResponseMessage response = client.GetAsync(BaseUrl + endpoint + ToQueryString(request)).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                T result = JsonConvert.DeserializeObject<T>(body, _serializerSettings);

                return result;
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private string PostAttachment(string endpoint, CreateAttachmentRequest request)
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonPayload = JsonConvert.SerializeObject(request, _serializerSettings);

            HttpResponseMessage response = client.PostAsync(BaseUrl + endpoint, new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic file = JsonConvert.DeserializeObject(body);

                return file.item.hash;
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private SingleItem<T> Post<T>(string endpoint, T payload, IPostOrPutRequest request)
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

            HttpResponseMessage response = client.PostAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                if (request.Reload)
                {
                    return JsonConvert.DeserializeObject<SingleItem<T>>(body);                    
                }

                return null;
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private SingleItem<T> Put<T>(string endpoint, T payload, IPostOrPutRequest request)
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

            HttpResponseMessage response = client.PutAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                if (request.Reload)
                {
                    return JsonConvert.DeserializeObject<SingleItem<T>>(body);                 
                }

                return null;
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private void Delete(string endpoint)
        {
            var client = InitHttpClient();

            var response = client.DeleteAsync(BaseUrl + endpoint).Result;

            if (!response.IsSuccessStatusCode)
            {
                string body = response.Content.ReadAsStringAsync().Result;
                var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
                throw new HelpScoutApiException(error, body);
            }
        }

        private HttpClient InitHttpClient()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _apiKey, "X"))));
            if (timeoutSeconds != 0)
                client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            return client;
        }

        private static string ToQueryString(IRequest request)
        {
            NameValueCollection nvc = null;
            if (request != null)
            {
                nvc = request.ToNameValueCollection();
            }

            if (nvc == null)
                return string.Empty;

            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
