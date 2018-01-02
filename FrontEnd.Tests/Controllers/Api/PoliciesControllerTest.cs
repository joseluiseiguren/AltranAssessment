namespace FrontEnd.Tests.Controllers.Api
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FrontEnd.Controllers.Api;
    using System.Collections.Generic;
    using System.Web.Http.Results;
    using FrontEnd.Dto;

    [TestClass]
    public class PoliciesControllerTest
    {
        [TestMethod]
        public void GetNoFilter()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get();
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(policyRepository.Policies.Count, ((List<PolicyDTO>)(contentResult.Content).Lista).Count);
        }

        [TestMethod]
        public void GetByPolicyIdNotFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(policyId: "999999");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<PolicyDTO>)(contentResult.Content).Lista).Count);
        }

        [TestMethod]
        public void GetByPolicyIdFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(policyId: policyRepository.Policies[0].Id);
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(policyRepository.Policies[0].Id, ((List<PolicyDTO>)(contentResult.Content).Lista)[0].Id);
        }

        [TestMethod]
        public void GetByClientIdNotFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();


            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(clientId: "999999");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<PolicyDTO>)(contentResult.Content).Lista).Count);
        }

        [TestMethod]
        public void GetByClientIdFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(clientId: policyRepository.Policies[0].ClientId);
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(policyRepository.Policies[0].ClientId, ((List<PolicyDTO>)(contentResult.Content).Lista)[0].ClientId);
        }

        [TestMethod]
        public void GetByAmountInsuredNotFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(amountInsured: 0.01m);
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<PolicyDTO>)(contentResult.Content).Lista).Count);
        }

        [TestMethod]
        public void GetByAmountInsuredFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(amountInsured: policyRepository.Policies[0].AmountInsured);
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(policyRepository.Policies[0].AmountInsured, ((List<PolicyDTO>)(contentResult.Content).Lista)[0].AmountInsured);
        }

        [TestMethod]
        public void GetByEmailNotFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(email: "999999");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<PolicyDTO>)(contentResult.Content).Lista).Count);
        }

        [TestMethod]
        public void GetByEmailFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(email: "urios@");
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, ((List<PolicyDTO>)(contentResult.Content).Lista).Count);
        }

        [TestMethod]
        public void GetByInceptionDateNotFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(inceptionDate: new System.DateTime(1980, 1, 1));
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, ((List<PolicyDTO>)(contentResult.Content).Lista).Count);
        }

        [TestMethod]
        public void GetByInceptionDateFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(inceptionDate: policyRepository.Policies[0].InceptionDate);
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(policyRepository.Policies[0].InceptionDate, ((List<PolicyDTO>)(contentResult.Content).Lista)[0].InceptionDate);
        }

        [TestMethod]
        public void GetByInstallmentPaymentFound()
        {
            var policyRepository = new RepositoriesMock.PoliciesRepositoryMock();

            PoliciesController controller = new PoliciesController(policyRepository);

            var result = controller.Get(installmentPayment: policyRepository.Policies[0].InstallmentPayment);
            var contentResult = result as OkNegotiatedContentResult<GenericListDTO<PolicyDTO>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(policyRepository.Policies[0].InstallmentPayment, ((List<PolicyDTO>)(contentResult.Content).Lista)[0].InstallmentPayment);
        }
    }
}
