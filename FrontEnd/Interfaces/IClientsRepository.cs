namespace FrontEnd.Interfaces
{
    using FrontEnd.Models;
    using System.Linq;

    /// <summary>
    /// interfaz del repositorio de clientes
    /// </summary>
    public interface IClientsRepository
    {
        /// <summary>
        /// obtiene una lista de clientes
        /// </summary>
        IQueryable<Client> GetClients();
    }
}
