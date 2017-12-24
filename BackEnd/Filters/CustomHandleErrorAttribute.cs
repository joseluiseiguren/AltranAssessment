namespace BackEnd.Filters
{
    using System;
    using System.Web.Http.Filters;

    /// <summary>
    /// filtro encargado de manejar los errores
    /// </summary>
    public class CustomHandleErrorAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// loguea el detalle de las excepciones en disco, y devuelve un GUID de referencia
        /// </summary>
        public override void OnException(HttpActionExecutedContext context)
        {
            string errorGuid = Guid.NewGuid().ToString();
            WebApiApplication.Logger.Error(context.Exception, errorGuid, null);

            context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            context.Response.Content = new System.Net.Http.StringContent(errorGuid, System.Text.Encoding.Unicode);
        }
    }
}