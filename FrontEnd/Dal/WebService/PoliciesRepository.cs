namespace FrontEnd.Dal.WebService
{
    using FrontEnd.Interfaces;
    using FrontEnd.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// repositorio de policies atravez de web service externo
    /// </summary>
    public class PoliciesRepository : IPoliciesRepository
    {
        /// <summary>
        /// DTO para recibir los datos de las policies desde el web service externo
        /// </summary>
        private class PoliciesDTO
        {
            public List<Policy> policies { get; set; }
        }

        /// <summary>
        /// clase para cachear el listado completo de policies (del web service externo)
        /// </summary>
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

            /// <summary>
            /// obtiene el listado completo de policies del web service externo
            /// </summary>
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

        /// <summary>
        /// obtiene una lista de policies
        /// </summary>
        public IQueryable<Policy> GetPolicies()
        {
            return Store.policies.AsQueryable();
        }
    }
}