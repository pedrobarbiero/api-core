using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DataTransferObjects
{
    public class ProviderDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}", MinimumLength = 11)]
        public string Document { get; set; }
        public int ProviderType { get; set; }

        public AddressDTO Address { get; set; }
        public bool Active { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
