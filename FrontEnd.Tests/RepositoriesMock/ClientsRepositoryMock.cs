namespace FrontEnd.Tests.RepositoriesMock
{
    using Interfaces;
    using System.Collections.Generic;
    using Models;
    using System.Linq;

    public class ClientsRepositoryMock : IClientsRepository
    {
        public List<Client> Clients;

        public ClientsRepositoryMock()
        {
            this.Clients = new List<Client>();

            Clients.Add(new Client()
            {
                Email = "joseph@gmail.com",
                Id = "999ceef9-3a01-49ae-a23b-3761b604800b",
                Name = "Joseph L Eiguren",
                Role = "admin"
            });

            Clients.Add(new Client()
            {
                Email = "peter@gmail.com",
                Id = "888415d6-53ee-4481-994f-4bffa47b5239",
                Name = "Peter Alfonso",
                Role = "user"
            });

            Clients.Add(new Client()
            {
                Email = "roger@gmail.com",
                Id = "7772ae47-d077-4f74-9166-56a6577e31b9",
                Name = "Roger Federerd",
                Role = "user"
            });

            Clients.Add(new Client()
            {
                Email = "xxx@gmail.com",
                Id = "9872ae47-d077-4f74-9166-56a6577e31b9",
                Name = "Richard Urios",
                Role = "user"
            });
        }

        /// <summary>
        /// obtiene una lista de clientes
        /// </summary>
        public IQueryable<Client> GetClients()
        {
            return this.Clients.AsQueryable();
        }
    }
}
