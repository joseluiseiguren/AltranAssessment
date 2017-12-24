namespace BackEnd.Tests.Controllers.RepositoriesMock
{
    using BackEnd.Interfaces;
    using BackEnd.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ClientsRepositoryMock : IClientsRepository
    {
        private List<Client> Clients;

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
                Name = "Roger Federer",
                Role = "user"
            });
        }

        public Client GetById(string id)
        {
            return this.Clients.Where(p => p.Id.Equals(id)).FirstOrDefault();
        }

        public Client GetByName(string name)
        {
            return this.Clients.Where(p => p.Name.Equals(name)).FirstOrDefault();
        }
    }
}
