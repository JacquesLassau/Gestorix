using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestorixDLL.DML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Tipo { get; set; }

        [Required(ErrorMessage = "O preenchimento é obrigatório.", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Por favor insira um e-mail válido.")]
        [DisplayName("Email:")]
        [StringLength(255, ErrorMessage = "São permitidos até 255 caracteres.")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Digite entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma Senha:")]
        [Compare("Senha", ErrorMessage = "As senhas não combinam.")]
        public string ConfirmaSenha { get; set; }

        public Usuario() { }

        public Usuario(string email)
        {
            this.Email = email;
        }

        public Usuario(int id, string email, string tipo)
        {
            this.IdUsuario = id;
            this.Email = email;
            this.Tipo = tipo;   
        }
    }
}
