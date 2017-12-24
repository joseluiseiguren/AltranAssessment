namespace BackEnd.Infraestructure
{
    using Dto;
    using AutoMapper;
    using Models;
    using System.Collections.Generic;

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

        public static ClientDTO ToDto(this Client client)
        {
            if (client == null)
            {
                return null;
            }

            return mapper.Map<Client, ClientDTO>(client);
        }

        public static PolicyDTO ToDto(this Policy policy)
        {
            if (policy == null)
            {
                return null;
            }

            return mapper.Map<Policy, PolicyDTO>(policy);
        }

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