namespace BackEnd.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BackEnd.Controllers;
    using BackEnd.Tests.Controllers.RepositoriesMock;
    using BackEnd.Dto;

    /// <summary>
    /// Testing para el controler de clientes
    /// </summary>
    [TestClass]
    public class ClientsControllerTest
    {
        /// <summary>
        /// se busca un cliente por id, y lo encuentra
        /// </summary>
        [TestMethod]
        public void GetClientById_OK()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string clientId = "999ceef9-3a01-49ae-a23b-3761b604800b";

            ClientsController controller = new ClientsController(clientRepository, policyRepository);

            var result = controller.ById(clientId);
            var contentResult = result as System.Web.Http.Results.OkNegotiatedContentResult<ClientDTO>;
            
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(clientId, contentResult.Content.Id);
        }

        /// <summary>
        /// se busca un cliente por id, y no lo encuentra
        /// </summary>
        [TestMethod]
        public void GetClientById_NotFound()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string clientId = "000ceef9-3a01-49ae-a23b-3761b604800b";

            ClientsController controller = new ClientsController(clientRepository, policyRepository);

            var result = controller.ById(clientId);
            var contentResult = result as System.Web.Http.Results.NotFoundResult;

            Assert.IsNotNull(contentResult);
        }

        /// <summary>
        /// se busca un cliente por name, y lo encuentra
        /// </summary>
        [TestMethod]
        public void GetClientByUserName_OK()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string userName = "Joseph L Eiguren";

            ClientsController controller = new ClientsController(clientRepository, policyRepository);

            var result = controller.ByUserName(userName);
            var contentResult = result as System.Web.Http.Results.OkNegotiatedContentResult<ClientDTO>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(userName, contentResult.Content.Name);
        }

        /// <summary>
        /// se busca un cliente por name, y no lo encuentra
        /// </summary>
        [TestMethod]
        public void GetClientByUserName_NotFound()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string userName = "Richard Urios";

            ClientsController controller = new ClientsController(clientRepository, policyRepository);

            var result = controller.ByUserName(userName);
            var contentResult = result as System.Web.Http.Results.NotFoundResult;

            Assert.IsNotNull(contentResult);
        }

        /// <summary>
        /// se busca un cliente por policy number, y lo encuentra
        /// </summary>
        [TestMethod]
        public void GetClientByPolicyNumber_OK()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string policyNumber = "35a0d2f7-37cd-4c21-8dac-fe91b29bd22b";
            string clientId = "999ceef9-3a01-49ae-a23b-3761b604800b";

            ClientsController controller = new ClientsController(clientRepository, policyRepository);

            var result = controller.ByPolicyNumber(policyNumber);
            var contentResult = result as System.Web.Http.Results.OkNegotiatedContentResult<ClientDTO>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(clientId, contentResult.Content.Id);
        }

        /// <summary>
        /// se busca un cliente por policy number, y no lo encuentra
        /// </summary>
        [TestMethod]
        public void GetClientByPolicyNumber_NotFound()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string policyNumber = "0000d2f7-37cd-4c21-8dac-fe91b29bd22b";

            ClientsController controller = new ClientsController(clientRepository, policyRepository);

            var result = controller.ByPolicyNumber(policyNumber);
            var contentResult = result as System.Web.Http.Results.NotFoundResult;

            Assert.IsNotNull(contentResult);
        }
    }
}
