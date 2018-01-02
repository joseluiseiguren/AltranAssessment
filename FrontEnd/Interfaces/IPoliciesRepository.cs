namespace FrontEnd.Interfaces
{
    using FrontEnd.Models;
    using System.Linq;

    public interface IPoliciesRepository
    {
        /// <summary>
        /// obtiene una lista de policies para un cliente
        /// </summary>
        IQueryable<Policy> GetPolicies();
    }
}
