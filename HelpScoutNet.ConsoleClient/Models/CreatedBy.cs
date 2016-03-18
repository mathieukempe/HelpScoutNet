using System.Collections.Generic;

namespace HelpScoutNet.ConsoleClient.Models
{
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
}