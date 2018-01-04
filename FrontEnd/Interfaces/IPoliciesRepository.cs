namespace FrontEnd.Interfaces
{
    using FrontEnd.Models;
    using System.Linq;

    /// <summary>
    /// repositorio de policies
    /// </summary>
    public interface IPoliciesRepository
    {
        /// <summary>
        /// obtiene una lista de policies para un cliente
        /// </summary>
        IQueryable<Policy> GetPolicies();
    }
}
