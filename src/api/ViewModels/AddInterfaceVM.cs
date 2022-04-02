using api.Requests;
using AutoMapper;
using FluentValidation;

namespace api.ViewModels
{
    public class AddInterfaceVM
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> ChildrenIds { get; set; } = new List<Guid>();
    }

    public class AddInterfaceVMValidator : AbstractValidator<AddInterfaceVM>
    {
        public AddInterfaceVMValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }

    public class AddInterfaceVMProfile : Profile
    {
        public AddInterfaceVMProfile()
           => CreateMap<AddInterfaceVM, AddInterfaceRequest>();
    }
}
