using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpScoutNet.Request;

namespace HelpScoutNet.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HelpScoutClient("a9fe3bcbef4d989dc7d05f1b8b1f90359a23d61f");
            var customersSearch =
                client.SearchCustomers(new SearchRequest { Query = "(customer:\"johnappleseed@gmail.com\")" });
            foreach (var searchresult in customersSearch.Items)
            {
                Console.WriteLine(searchresult.FirstName + searchresult.LastName);
            }

            Console.ReadKey();
        }
    }
}
