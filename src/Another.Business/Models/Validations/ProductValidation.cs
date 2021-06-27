using FluentValidation;

namespace Another.Business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
                 .NotEmpty().WithMessage("{PropertyName} must have a value");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        }
    }
}
