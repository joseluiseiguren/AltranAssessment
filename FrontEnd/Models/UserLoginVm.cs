namespace FrontEnd.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginVm
    {
        /// <summary>
        /// email del usuario, sera solicitado paara ingresar a la aplicacion
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage ="Invalid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}