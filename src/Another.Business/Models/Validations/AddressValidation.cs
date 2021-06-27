using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Another.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("{PropertyName} must have a value");

            RuleFor(a => a.ZipCode)
                .NotEmpty().WithMessage("{PropertyName} must have a value")
                .Length(8).WithMessage("{PropertyName} must have {MaxLength}");

            RuleFor(a => a.Neighborhood)
                .NotEmpty().WithMessage("{PropertyName} must have a value");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("{PropertyName} must have a value");
        }
    }
}
