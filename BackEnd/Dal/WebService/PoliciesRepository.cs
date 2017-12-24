namespace BackEnd.Dal.WebService
{
    using BackEnd.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    public class PoliciesRepository : IPoliciesRepository
    {
        private class PoliciesDTO
        {
            public List<Policy> policies { get; set; }
        }

        private static class Store
        {
            private static List<Policy> _policies;
            public static List<Policy> policies
            {
                get
                {
                    if (_policies == null)
                    {
                        _policies = LoadAllPolicies();
                    }

                    return _policies;
                }
            }

            private static List<Policy> LoadAllPolicies()
            {
                WebClient client = new WebClient();
                Stream rawData = client.OpenRead(@"http://www.mocky.io/v2/580891a4100000e8242b75c5");
                StreamReader reader = new StreamReader(rawData);
                string data = reader.ReadToEnd();
                rawData.Close();
                reader.Close();

                var lstClients = JsonConvert.DeserializeObject<PoliciesDTO>(data) as PoliciesDTO;

                return lstClients?.policies;
            }
        }

        public Policy GetById(string id)
        {
            return Store.policies.Where(p => p.Id.Equals(id)).FirstOrDefault();
        }

        public List<Policy> GetByClientId(string clientId)
        {
            return Store.policies.Where(p => p.ClientId.Equals(clientId)).ToList();
        }
    }
}