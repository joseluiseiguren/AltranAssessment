namespace FrontEnd.Tests.Controllers.Api
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FrontEnd.Controllers.Api;
    using System.Collections.Generic;
    using System.Web.Http.Results;

    [TestClass]
    public class ClientsControllerTest
    {
        [TestMethod]
        public void GetNoFilter()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();
            
            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get();
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Dto.ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(clientRepository.Clients.Count, ((List<Dto.ClientDTO>)contentResult.Content).Count);
        }

        [TestMethod]
        public void GetByIdNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(id: "999999");
            var contentResult = result as NotFoundResult;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void GetByIdFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(id: clientRepository.Clients[0].Id);
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Dto.ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(clientRepository.Clients[0].Id, ((List<Dto.ClientDTO>)contentResult.Content)[0].Id);
        }

        [TestMethod]
        public void GetByNameNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(name: "999999");
            var contentResult = result as NotFoundResult;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void GetByNameFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(name: "rd");
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Dto.ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, ((List<Dto.ClientDTO>)contentResult.Content).Count);
        }

        [TestMethod]
        public void GetByEmailNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(email: "999999");
            var contentResult = result as NotFoundResult;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void GetByEmailFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(email: "peter@");
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Dto.ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, ((List<Dto.ClientDTO>)contentResult.Content).Count);
        }

        [TestMethod]
        public void GetByRoleNotFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(role: "999999");
            var contentResult = result as NotFoundResult;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void GetByRoleFound()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();

            ClientsController controller = new ClientsController(clientRepository);

            var result = controller.Get(role: "admin");
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Dto.ClientDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(((List<Dto.ClientDTO>)contentResult.Content).Count > 0);
        }
    }
}
