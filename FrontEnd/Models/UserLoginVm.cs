namespace FrontEnd.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// view model de login
    /// </summary>
    public class UserLoginVm
    {
        /// <summary>
        /// email del usuario, sera solicitado paara ingresar a la aplicacion
        /// </summary>
        [Required(ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Invalid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}