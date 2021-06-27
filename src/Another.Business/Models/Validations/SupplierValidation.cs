using FluentValidation;

namespace Another.Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} must have a value");
        }
    }
}
