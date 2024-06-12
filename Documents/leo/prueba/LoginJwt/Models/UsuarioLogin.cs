using System.ComponentModel.DataAnnotations;
namespace LoginJwt.Models
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "El campo Email es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Password es requerido.")]
        public string Password { get; set; }
    }
}
