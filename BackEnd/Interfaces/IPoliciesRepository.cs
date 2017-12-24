namespace BackEnd
{
    using BackEnd.Models;
    using System.Collections.Generic;

    /// <summary>
    /// firmas del repositorio de policies
    /// </summary>
    public interface IPoliciesRepository
    {
        /// <summary>
        /// obtiene una poliza por ID
        /// </summary>
        Policy GetById(string id);

        /// <summary>
        /// obtiene una lista de ´policies por id del cliente
        /// </summary>
        List<Policy> GetByClientId(string clientId);
    }
}