namespace FrontEnd.Dto
{
    using System;

    /// <summary>
    /// Objeto de ordenamiento
    /// </summary>
    public class OrdenamientoDTO
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase ordenamiento.
        /// </summary>
        public OrdenamientoDTO()
        {
            this.Asc = true;
            this.ColumnaOrdenamiento = string.Empty;
        }

        /// <summary>
        /// true: ordenamiento ascendente, false: ordenamiento descendente
        /// </summary>
        public bool Asc { get; set; }

        /// <summary>
        /// columna de ordenamiento
        /// </summary>
        public string ColumnaOrdenamiento { get; set; }

        /// <summary>
        /// columna de ordenamiento
        /// </summary>
        public string GetSentidoOrdenamiento()
        {
            return (this.Asc) ? "ASC" : "DESC";
        }
    }
}