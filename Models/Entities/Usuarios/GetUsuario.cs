using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities.Usuarios
{
    public class GetUsuario
    {

        [EmailAddress, Required(AllowEmptyStrings = false), MaxLength(255), MinLength(6)]
        public string email { get; set; } = "";
        [Required(AllowEmptyStrings = false), MaxLength(64), MinLength(8)]
        public string senha { get; set; } = "";
        [Required]
        public bool manterLogin { get; set; } = false;

        public void TrimAll()
        {
            email = email.Trim();
            senha = senha.Trim();
        }

        public bool VerificaCampos()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                return false;
            return true;
        }
    }
}
