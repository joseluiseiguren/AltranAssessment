namespace BackEnd.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BackEnd.Controllers;
    using BackEnd.Tests.Controllers.RepositoriesMock;
    using System.Collections.Generic;

    [TestClass]
    public class PoliciesControllerTest
    {
        [TestMethod]
        public void GetPoliciesByUserName_OK()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string userName = "Joseph L Eiguren";

            PoliciesController controller = new PoliciesController(clientRepository, policyRepository);

            var result = controller.ByUserName(userName);
            var contentResult = result as System.Web.Http.Results.OkNegotiatedContentResult<List<Models.Policy>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count);
        }

        [TestMethod]
        public void GetPoliciesByUserName_NotFound()
        {
            var clientRepository = new ClientsRepositoryMock();
            var policyRepository = new PoliciesRepositoryMock();

            string userName = "Richard Urios";

            PoliciesController controller = new PoliciesController(clientRepository, policyRepository);

            var result = controller.ByUserName(userName);
            var contentResult = result as System.Web.Http.Results.NotFoundResult;

            Assert.IsNotNull(contentResult);
        }
    }
}
