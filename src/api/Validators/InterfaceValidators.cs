using Core.ViewModels.Interfaces;
using FluentValidation;

namespace Api.Validators;

public class InterfaceValidators
{
    public class AddInterfaceVMValidator : AbstractValidator<AddInterfaceVM>
    {
        public AddInterfaceVMValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}

public class EditInterfaceVMVMValidator : AbstractValidator<InterfaceVM>
{
    public EditInterfaceVMVMValidator()
        => RuleFor(x => x.Id).NotEmpty();
}

public class AddInterfacePropertyVMValidator : AbstractValidator<InterfacePropertyVM>
{
    public AddInterfacePropertyVMValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
