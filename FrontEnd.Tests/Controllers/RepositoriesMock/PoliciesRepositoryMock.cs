namespace FrontEnd.Tests.Controllers.RepositoriesMock
{
    using FrontEnd.Interfaces;
    using FrontEnd.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PoliciesRepositoryMock : IPoliciesRepository
    {
        public List<Policy> Policies;

        public PoliciesRepositoryMock()
        {
            this.Policies = new List<Policy>();

            this.Policies.Add(new Policy()
            {
                Email = "joseph@gmail.com",
                ClientId = "999ceef9-3a01-49ae-a23b-3761b604800b",
                AmountInsured = 100,
                Id = "0039b246-5ffa-4b90-B16f-Fc9f2d4033d6",
                InceptionDate = DateTime.Now,
                InstallmentPayment = true
            });

            this.Policies.Add(new Policy()
            {
                Email = "joseph@gmail.com",
                ClientId = "999ceef9-3a01-49ae-a23b-3761b604800b",
                AmountInsured = 856.32m,
                Id = "01adaadb-Ac33-4fe3-8e66-Bb162ae71083",
                InceptionDate = new DateTime(2017,05,25),
                InstallmentPayment = false
            });

            this.Policies.Add(new Policy()
            {
                Email = "joseph@gmail.com",
                ClientId = "999ceef9-3a01-49ae-a23b-3761b604800b",
                AmountInsured = 0.85m,
                Id = "033ba8cd-E2e2-47c8-8963-Dde166dec736",
                InceptionDate = new DateTime(2017, 05, 01),
                InstallmentPayment = false
            });

            this.Policies.Add(new Policy()
            {
                Email = "urios@gmail.com",
                ClientId = "888ceef9-3a01-49ae-a23b-3761b604800b",
                AmountInsured = 2000m,
                Id = "0bf7fc01-6ffb-44aa-8d68-324f0c3cbcee",
                InceptionDate = new DateTime(2017, 01, 12),
                InstallmentPayment = true
            });
        }

        /// <summary>
        /// obtiene una lista de policies para un cliente
        /// </summary>
        public IQueryable<Policy> GetPolicies()
        {
            return this.Policies.AsQueryable();
        }
    }
}