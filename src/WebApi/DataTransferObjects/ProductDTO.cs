using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Extensions;

namespace WebApi.DataTransferObjects
{
    [ModelBinder(typeof(JsonWithFileFormDataModelBinder), Name = "product")]
    public class ProductDTO
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter tamanho entre {1} e {2}.", MinimumLength = 2)]
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageUpload { get; set; }
        public IFormFile ImageUploadFile { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime InsertedDate { get; set; }
        public bool Active { get; set; }

        [ScaffoldColumn(false)]
        public string ProviderName { get; set; }
    }
}
