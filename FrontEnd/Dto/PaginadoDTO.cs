namespace FrontEnd.Dto
{
    using System;

    /// <summary>
    /// Objeto de paginado
    /// </summary>
    public class PaginadoDTO
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Paginado.
        /// </summary>
        public PaginadoDTO()
        {
            this.Pagina = 1;
            this.FilasPorPagina = 10;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Paginado.
        /// </summary>
        /// <param name="pagina">pagina solicitada.</param>
        /// <param name="filas">filas por pagina.</param>
        public PaginadoDTO(int pagina, int filas)
        {
            this.Pagina = pagina;
            this.FilasPorPagina = filas;
        }

        /// <summary>
        /// Pagina solicitada.
        /// </summary>
        public int Pagina { get; set; }

        /// <summary>
        /// Filas por pagina.
        /// </summary>
        public int FilasPorPagina { get; set; }
    }
}