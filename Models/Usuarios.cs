using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table(name: "usuarios")]
    public class Usuarios
    {

        [Key]
        public Guid Usuario_Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(64), MinLength(5)]
        public string nome { get; set; } = "";

        [EmailAddress, Required(AllowEmptyStrings = false), MaxLength(255), MinLength(6)]
        public string email { get; set; } = "";

        [Required(AllowEmptyStrings = false), MaxLength(255), MinLength(10)]
        public string senha { get; set; } = "";

        [Required(AllowEmptyStrings = false), MaxLength(1), MinLength(1)]
        public string sexo { get; set; } = "";

        [Required]
        public DateTime nascimento { get; set; }

    }
}
