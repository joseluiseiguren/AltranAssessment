namespace FrontEnd.Dal.WebService
{
    using FrontEnd.Interfaces;
    using FrontEnd.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    public class ClientsRepository : IClientsRepository
    {
        /// <summary>
        /// DTO para recibir los datos de los clientes desde el web service externo
        /// </summary>
        private class ClientsDTO
        {
            public List<Client> clients { get; set; }
        }

        /// <summary>
        /// clase para cachear el listado completo de clientes (del web service externo)
        /// </summary>
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

            /// <summary>
            /// obtiene el listado completo de clientes del web service externo
            /// </summary>
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

        /// <summary>
        /// obtiene una lista de clientes
        /// </summary>
        public IQueryable<Client> GetClients()
        {
            return Store.clients.AsQueryable();
        }
    }
}