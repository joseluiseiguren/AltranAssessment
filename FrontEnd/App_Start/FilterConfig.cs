namespace FrontEnd
{
    using System.Web.Mvc;

    /// <summary>
    /// filtros generales de toda la aplicacion
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// filtros generales de toda la aplicacion
        /// <paramref name="filters"/>
        /// </summary>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
