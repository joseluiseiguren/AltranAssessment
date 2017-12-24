namespace BackEnd.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BackEnd.Controllers;
    using BackEnd.Tests.Controllers.RepositoriesMock;
    using System.Collections.Generic;
    using BackEnd.Dto;
    using System.Linq;

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
            var contentResult = result as System.Web.Http.Results.OkNegotiatedContentResult<IEnumerable<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.ToList().Count);
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
