using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Dto
{
    public class GenericListDTO<T>
    {
        public int CantidadRegistros { get; set; }

        public int CantidadPaginas { get; set; }

        public int PaginaActual { get; set; }

        public IEnumerable<T> Lista { get; set; }
    }
}