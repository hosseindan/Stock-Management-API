using Carsales.StockManagement.Models.VeiwModels;
using FluentValidation;

namespace Carsales.StockManagement.Models.Validators
{
    public class CreateCarValidator : AbstractValidator<CreateCarRequest>
    {
        public CreateCarValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Make).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model).NotEmpty().MinimumLength(1);
        }
    }
}
