﻿namespace FrontEnd.Tests.Controllers.Api
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FrontEnd.Controllers.Api;
    using System.Collections.Generic;
    using System.Web.Http.Results;
    using FrontEnd.Dto;

    /// <summary>
    /// testing del controler de clientes
    /// </summary>
    [TestClass]
    public class ClientsControllerTest
    {
        /// <summary>
        /// obtiene todos los clientes sin filtros
        /// </summary>
        [TestMethod]
        public void GetNoFilter()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();
            
            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get();
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(clientRepository.Clients.Count, ((List<ClientDTO>)(contentResult.Content).Lista).Count);
        }

        /// <summary>
        /// se busca por id de cliente y no encuentra
        /// </summary>
        [TestMethod]
        public void GetByIdNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(id: "999999");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<ClientDTO>)(contentResult.Content).Lista).Count);
        }

        /// <summary>
        /// se busca por id de cliente y encuentra
        /// </summary>
        [TestMethod]
        public void GetByIdFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(id: clientRepository.Clients[0].Id);
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(clientRepository.Clients[0].Id, ((List<ClientDTO>)(contentResult.Content).Lista)[0].Id);
        }

        /// <summary>
        /// se busca por name de cliente y no encuentra
        /// </summary>
        [TestMethod]
        public void GetByNameNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(name: "999999");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<ClientDTO>)(contentResult.Content).Lista).Count);
        }

        /// <summary>
        /// se busca por name de cliente y encuentra
        /// </summary>
        [TestMethod]
        public void GetByNameFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(name: "rd");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, ((List<ClientDTO>)(contentResult.Content).Lista).Count);
        }

        /// <summary>
        /// se busca por email de cliente y no encuentra
        /// </summary>
        [TestMethod]
        public void GetByEmailNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(email: "999999");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<ClientDTO>)(contentResult.Content).Lista).Count);
        }

        /// <summary>
        /// se busca por email de cliente y encuentra
        /// </summary>
        [TestMethod]
        public void GetByEmailFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(email: "peter@");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, ((List<ClientDTO>)(contentResult.Content).Lista).Count);
        }

        /// <summary>
        /// se busca por rol de cliente y no encuentra
        /// </summary>
        [TestMethod]
        public void GetByRoleNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(role: "999999");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<ClientDTO>)(contentResult.Content).Lista).Count);
        }

        /// <summary>
        /// se busca por rol de cliente y encuentra
        /// </summary>
        [TestMethod]
        public void GetByRoleFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(role: "admin");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(((List<ClientDTO>)(contentResult.Content).Lista).Count > 0);
        }
    }
}
