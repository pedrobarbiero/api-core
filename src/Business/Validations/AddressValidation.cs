using System;
using Business.Models;
using FluentValidation;

namespace Business.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Place)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(min: 2, max: 50).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(a => a.Neighborhood)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(min: 2, max: 50).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(a => a.ZipCode)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(8).WithMessage("O Campo {PropertyName} precisa ter {MaxLength} caracteres.");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(min: 2, max: 50).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(min: 2, max: 50).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("O Campo {PropertyName precisa ser fornecido}")
                .Length(min: 1, max: 10).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}
