namespace FrontEnd.Security
{
    using FrontEnd.Interfaces;
    using System.Web.Security;

    /// <summary>
    /// implementacion de interfaz de auttorizacion
    /// </summary>
    public class FormsAuth : IAuth
    {
        /// <summary>
        /// generacion de cookie para login exitoso
        /// </summary>
        public void SetAuthCookie(string username, bool remember)
        {
            FormsAuthentication.SetAuthCookie(username, remember);
        }
    }
}