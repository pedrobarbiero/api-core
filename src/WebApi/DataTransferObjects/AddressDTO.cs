using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DataTransferObjects
{
    public class AddressDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 2)]
        public string Place { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 1)]
        public string Number { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 2)]
        public string Complement { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 8)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 2)]        
        public string City { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 2)]        
        public string State { get; set; }
        
        public Guid ProviderId { get; set; }
    }
}
