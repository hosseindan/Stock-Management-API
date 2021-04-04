using Carsales.StockManagement.Models.VeiwModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

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
    public class UpdateCarValidator : AbstractValidator<UpdateCarRequest>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.Make).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model).NotEmpty().MinimumLength(1);
        }
    }
}
