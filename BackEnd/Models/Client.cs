namespace BackEnd.Models
{
    /// <summary>
    /// modelo de clientes
    /// </summary>
    public class Client
    {
        /// <summary>
        /// ide del cliente
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// nombre del cliente
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// email del cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// rol del cliente
        /// </summary>
        public string Role { get; set; }
    }
}