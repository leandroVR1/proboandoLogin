using System.ComponentModel.DataAnnotations;

namespace LoginJwt.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Email es requerido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Password es requerido.")]
        public string Password { get; set; }
        public string Documento { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdRol { get; set; }
    }
}
