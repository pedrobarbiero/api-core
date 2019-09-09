using System;
using Business.Models;
using Business.Validations.Documents;
using FluentValidation;

namespace Business.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser informado.")
                .Length(min: 2, max: 100).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.ProviderType == ProviderType.PhysicalPerson, () =>
            {
                RuleFor(f => f.Document.Length)
                    .Equal(CpfValidation.CpfLength)
                    .WithMessage("O Campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");
                RuleFor(f => CpfValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("O Documento fornecido é inválido");
            });
            
            When(f => f.ProviderType == ProviderType.LegalPerson, () =>
            {
                RuleFor(f => f.Document.Length)
                    .Equal(CnpjValidation.CpfLength)
                    .WithMessage("O Campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");
                RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("O Documento fornecido é inválido");
            });
        }
    }
}