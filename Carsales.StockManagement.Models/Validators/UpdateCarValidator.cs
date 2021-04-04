using Carsales.StockManagement.Models.VeiwModels;
using FluentValidation;

namespace Carsales.StockManagement.Models.Validators
{
    public class UpdateCarValidator : AbstractValidator<UpdateCarRequest>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.Make).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model).NotEmpty().MinimumLength(1);
        }
    }
}
