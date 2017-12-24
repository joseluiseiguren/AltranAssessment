namespace BackEnd.Infraestructure
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
                cfg.CreateMap<Policy, PolicyDTO>();
            });

            mapper = config.CreateMapper();
        }

        /// <summary>
        /// devuelve un clienteDTO a partir del modelo
        /// </summary>
        public static ClientDTO ToDto(this Client client)
        {
            if (client == null)
            {
                return null;
            }

            return mapper.Map<Client, ClientDTO>(client);
        }

        /// <summary>
        /// devuelve un policyDTO a partir del modelo
        /// </summary>
        public static PolicyDTO ToDto(this Policy policy)
        {
            if (policy == null)
            {
                return null;
            }

            return mapper.Map<Policy, PolicyDTO>(policy);
        }

        /// <summary>
        /// devuelve una lista de policyDTO a partir de un modelo
        /// </summary>
        public static IEnumerable<PolicyDTO> ToDto(this IEnumerable<Policy> policy)
        {
            if (policy == null)
            {
                return null;
            }

            return mapper.Map<IEnumerable<Policy>, IEnumerable<PolicyDTO>>(policy);
        }
    }
}