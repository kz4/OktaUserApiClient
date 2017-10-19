using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = "";
            HttpClientWrapper wrapper = new HttpClientWrapper(token);
            List<string> pagesOfResponse = wrapper.Invoke("GET", "https://eci.okta.com/api/v1/users?limit=200", "");
            List<OktaUser> oktaUsers = GetOktaUsers(pagesOfResponse);
        }

        private static List<OktaUser> GetOktaUsers(List<string> pagesOfResponse)
        {
            List<OktaUser> oktaUsers = new List<OktaUser>();
            foreach (var page in pagesOfResponse)
            {
                List<OktaUser> users = JsonConvert.DeserializeObject<List<OktaUser>>(page);
                oktaUsers.AddRange(users);
            }
            return oktaUsers;
        }
    }
}
