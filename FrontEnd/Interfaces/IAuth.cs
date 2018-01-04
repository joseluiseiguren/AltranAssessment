namespace FrontEnd.Interfaces
{
    /// <summary>
    /// interfaz para login del site
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// generacion de cookie para login exitoso
        /// </summary>
        void SetAuthCookie(string username, bool remember);
    }
}