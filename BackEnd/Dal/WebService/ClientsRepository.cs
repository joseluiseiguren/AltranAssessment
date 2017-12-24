namespace BackEnd.Dal.WebService
{
    using BackEnd.Models;
    using System.Net;
    using System.IO;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Linq;
    using BackEnd.Interfaces;

    public class ClientsRepository : IClientsRepository
    {
        private class ClientsDTO
        {
            public List<Client> clients { get; set; }
        }

        private static class Store
        {
            private static List<Client> _clients;
            public static List<Client> clients
            {
                get
                {
                    if (_clients == null)
                    {
                        _clients = LoadAllClients();
                    }

                    return _clients;
                }
            }

            private static List<Client> LoadAllClients()
            {
                WebClient client = new WebClient();
                Stream rawData = client.OpenRead(@"http://www.mocky.io/v2/5808862710000087232b75ac");
                StreamReader reader = new StreamReader(rawData);
                string data = reader.ReadToEnd();
                rawData.Close();
                reader.Close();

                var lstClients = JsonConvert.DeserializeObject<ClientsDTO>(data) as ClientsDTO;

                return lstClients?.clients;
            }
        }

        public Client GetById(string id)
        {
            return Store.clients.Where(p => p.Id.Equals(id)).FirstOrDefault();
        }

        public Client GetByName(string name)
        {
            return Store.clients.Where(p => p.Name.Equals(name)).FirstOrDefault();
        }
    }
}