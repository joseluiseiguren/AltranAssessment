namespace FrontEnd.Interfaces
{
    using FrontEnd.Models;
    using System.Linq;
    using System.Collections.Generic;

    public interface IClientsRepository
    {
        /// <summary>
        /// obtiene una lista de clientes
        /// </summary>
        IQueryable<Client> GetClients();
    }
}
