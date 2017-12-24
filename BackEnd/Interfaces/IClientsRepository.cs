namespace BackEnd.Interfaces
{
    using BackEnd.Models;

    public interface IClientsRepository
    {
        Client GetById(string id);

        Client GetByName(string name);
    }
}