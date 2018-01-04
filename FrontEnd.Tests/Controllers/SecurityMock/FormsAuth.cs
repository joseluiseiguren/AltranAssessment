namespace FrontEnd.Tests.Controllers.SecurityMock
{
    using FrontEnd.Interfaces;

    public class FormsAuthMock : IAuth
    {
        public void SetAuthCookie(string username, bool remember)
        {
            return;
        }
    }
}
