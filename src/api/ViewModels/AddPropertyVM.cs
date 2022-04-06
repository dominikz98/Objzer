using api.Models;
using AutoMapper;
using FluentValidation;

namespace api.ViewModels
{
    public class AddPropertyVM
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PropertyTypes Type { get; set; } = PropertyTypes.String;
        public bool Required { get; set; } = true;
    }

    public class AddPropertyVMValidator : AbstractValidator<AddPropertyVM>
    {
        public AddPropertyVMValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class AddPropertyVMProfile : Profile
    {
        public AddPropertyVMProfile()
           => CreateMap<AddPropertyVM, CTInterfaceProperty>();
    }
}
