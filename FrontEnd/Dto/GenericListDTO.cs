namespace FrontEnd.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// Dto para devolver resultados de busquedas
    /// </summary>
    public class GenericListDTO<T>
    {
        /// <summary>
        /// cantidad de registros que devolvio la consulta
        /// </summary>
        public int CantidadRegistros { get; set; }

        /// <summary>
        /// cantidad de paginas que devolvio la consulta
        /// </summary>
        public int CantidadPaginas { get; set; }

        /// <summary>
        /// pagina actual de la consulta
        /// </summary>
        public int PaginaActual { get; set; }

        /// <summary>
        /// resultado de la consulta
        /// </summary>
        public IEnumerable<T> Lista { get; set; }
    }
}