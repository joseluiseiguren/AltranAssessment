namespace BackEnd
{
    using BackEnd.Models;
    using System.Collections.Generic;

    public interface IPoliciesRepository
    {
        Policy GetById(string id);

        List<Policy> GetByClientId(string clientId);
    }
}