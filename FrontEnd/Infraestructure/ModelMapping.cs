namespace FrontEnd.Infraestructure
{
    using Dto;
    using AutoMapper;
    using Models;
    using System.Collections.Generic;

    /// <summary>
    /// clase para el manejo de mapeos de objetos
    /// </summary>
    public static class ModelMapping
    {
        private static IMapper mapper;

        static ModelMapping()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Client, ClientDTO>();
            });

            mapper = config.CreateMapper();
        }

        /// <summary>
        /// devuelve una lista de policyDTO a partir de un modelo
        /// </summary>
        public static IEnumerable<ClientDTO> ToDto(this IEnumerable<Client> client)
        {
            if (client == null)
            {
                return null;
            }

            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(client);
        }
    }
}