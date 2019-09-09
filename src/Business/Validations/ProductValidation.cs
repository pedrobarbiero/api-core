using System;
using Business.Models;
using FluentValidation;

namespace Business.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(min: 2, max: 50).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(min: 2, max: 100).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(a => a.Value)
                .GreaterThan(0).WithMessage("O Campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}
