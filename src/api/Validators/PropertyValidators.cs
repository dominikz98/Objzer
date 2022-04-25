using Core.ViewModels.Properties;
using FluentValidation;

namespace Api.Validators
{
    public class AddPropertyVMValidator : AbstractValidator<PropertyVM>
    {
        public AddPropertyVMValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
