namespace FrontEnd.Security
{
    using FrontEnd.Interfaces;
    using System.Web.Security;

    public class FormsAuth : IAuth
    {
        public void SetAuthCookie(string username, bool remember)
        {
            FormsAuthentication.SetAuthCookie(username, remember);
        }
    }
}