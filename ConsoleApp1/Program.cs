using Microsoft.Rest;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BackEnd.Client.BackEndClient cliente = new BackEnd.Client.BackEndClient();
            HttpOperationResponse<BackEnd.Client.Models.ClientDTO> result = cliente.Clients.ByIdWithHttpMessagesAsync("a0ece5db-cd14-4f21-812f-966633e7be86").Result;
            BackEnd.Client.Models.ClientDTO res = result.Body;

            



        }
    }
}
