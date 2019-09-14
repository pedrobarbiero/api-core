using System.ComponentModel.DataAnnotations;

namespace WebApi.DataTransferObjects
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo {0} inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatória")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Campo {0} deve conter entre {1} e {2} caracteres")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [Compare("Password", ErrorMessage = "Senha e Confirmação são diferentes")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo {0} inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório")]     
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Campo {0} deve conter entre {1} e {2} caracteres")]
        public string Password { get; set; }
    }
}
