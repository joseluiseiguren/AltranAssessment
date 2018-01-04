namespace FrontEnd.Tests.Controllers.SecurityMock
{
    using FrontEnd.Interfaces;

    /// <summary>
    /// se mockea la autorizacion de usuarios
    /// </summary>
    public class FormsAuthMock : IAuth
    {
        /// <summary>
        /// se mockea la generacion de cookies
        /// </summary>
        public void SetAuthCookie(string username, bool remember)
        {
            return;
        }
    }
}
