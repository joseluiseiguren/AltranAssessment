namespace FrontEnd.Tests.Controllers.Api
{
    using FrontEnd.Controllers;
    using FrontEnd.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Web.Mvc;

    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void LoginOk()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();
            var securityMock = new SecurityMock.FormsAuthMock();

            UsersController controller = new UsersController(clientRepository, securityMock);

            UserLoginVm user = new UserLoginVm() { Email = clientRepository.Clients[0].Email };
            RedirectToRouteResult result = controller.Login(user) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteValues.ContainsValue("Search"));
            Assert.IsTrue(result.RouteValues.ContainsValue("Clients"));
        }

        [TestMethod]
        public void LoginFail()
        {
            var clientRepository = new RepositoriesMock.ClientsRepositoryMock();
            var securityMock = new SecurityMock.FormsAuthMock();

            UsersController controller = new UsersController(clientRepository, securityMock);

            UserLoginVm user = new UserLoginVm() { Email = "123456" };
            RedirectToRouteResult result = controller.Login(user) as RedirectToRouteResult;
            Assert.IsNull(result);
        }
    }
}
