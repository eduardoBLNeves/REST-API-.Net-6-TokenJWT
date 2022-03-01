using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities.Usuarios
{
    public class PostUsuario
    {

        [EmailAddress, Required(AllowEmptyStrings = false), MaxLength(255), MinLength(6)]
        public string email { get; set; } = "";
        [Required(AllowEmptyStrings = false), MaxLength(64), MinLength(8)]
        public string senha { get; set; } = "";

        [Required(AllowEmptyStrings = false), MaxLength(64), MinLength(5)]
        public string nome { get; set; } = "";

        [Required(AllowEmptyStrings = false), MaxLength(1), MinLength(1)]
        public string sexo { get; set; } = "";

        [Required(AllowEmptyStrings = false), MaxLength(2), MinLength(1)]
        public string dia { get; set; } = "";

        [Required(AllowEmptyStrings = false), MaxLength(2), MinLength(1)]
        public string mes { get; set; } = "";

        [Required(AllowEmptyStrings = false), MaxLength(4), MinLength(4)]
        public string ano { get; set; } = "";

        public void TrimAll()
        {
            email = email.Trim();
            senha = senha.Trim();
            nome = nome.Trim();
            sexo = sexo.Trim();
            mes = mes.Trim();
            dia = dia.Trim();
            ano = ano.Trim();
        }

        public bool VerificaCampos()
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) ||
                string.IsNullOrEmpty(sexo) || string.IsNullOrEmpty(dia) || string.IsNullOrEmpty(mes) || string.IsNullOrEmpty(ano))
                return false;
            return true;
        }


    }
}
