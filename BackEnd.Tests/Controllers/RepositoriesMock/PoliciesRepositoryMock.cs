namespace BackEnd.Tests.Controllers.RepositoriesMock
{
    using System;
    using System.Collections.Generic;
    using BackEnd.Models;
    using System.Linq;

    /// <summary>
    /// se mockea ek repositorio de plicies
    /// </summary>
    public class PoliciesRepositoryMock : IPoliciesRepository
    {
        private List<Policy> Policies;

        /// <summary>
        /// se construye la lista de policies en memoria
        /// </summary>
        public PoliciesRepositoryMock()
        {
            this.Policies = new List<Policy>();

            Policies.Add(new Policy()
            {
                Email = "joseph@gmail.com",
                ClientId = "999ceef9-3a01-49ae-a23b-3761b604800b",
                AmountInsured = 100,
                Id = "35a0d2f7-37cd-4c21-8dac-fe91b29bd22b",
                InceptionDate = new DateTime(2017, 12, 01),
                InstallmentPayment = true
            });

            Policies.Add(new Policy()
            {
                Email = "joseph@gmail.com",
                ClientId = "999ceef9-3a01-49ae-a23b-3761b604800b",
                AmountInsured = 954.36m,
                Id = "0eba1179-6155-41b5-bdd8-f80e1ac94cab",
                InceptionDate = new DateTime(2017, 12, 02),
                InstallmentPayment = false
            });

            Policies.Add(new Policy()
            {
                Email = "peter@gmail.com",
                ClientId = "888415d6-53ee-4481-994f-4bffa47b5239",
                AmountInsured = 0.87m,
                Id = "8a53d72e-c802-42ae-849b-11c6cf550a0d",
                InceptionDate = new DateTime(2017, 12, 03),
                InstallmentPayment = false
            });
        }

        /// <summary>
        /// se busca una policy por client id
        /// </summary>
        public List<Policy> GetByClientId(string clientId)
        {
            return this.Policies.Where(p => p.ClientId.Equals(clientId)).ToList();
        }

        /// <summary>
        /// se busca una policy por id
        /// </summary>
        public Policy GetById(string id)
        {
            return this.Policies.Where(p => p.Id.Equals(id)).FirstOrDefault();
        }
    }
}
