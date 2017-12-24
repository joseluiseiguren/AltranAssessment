namespace BackEnd.Interfaces
{
    using BackEnd.Models;

    /// <summary>
    /// firmas del repositorio de clientes
    /// </summary>
    public interface IClientsRepository
    {
        /// <summary>
        /// obtiene un cliente por ID
        /// </summary>
        Client GetById(string id);

        /// <summary>
        /// obtiene un cliente por username
        /// </summary>
        Client GetByName(string name);
    }
}