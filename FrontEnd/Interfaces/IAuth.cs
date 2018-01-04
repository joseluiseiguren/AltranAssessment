namespace FrontEnd.Interfaces
{
    public interface IAuth
    {
        void SetAuthCookie(string username, bool remember);
    }
}