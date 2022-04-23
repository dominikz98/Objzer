using api.Requests.Interfaces;
using AutoMapper;
using FluentValidation;

namespace api.ViewModels.Interface
{
    public class EditInterfaceVM : AddInterfaceVM
    {
        public Guid Id { get; set; }
    }

    public class EditInterfaceVMVMValidator : AbstractValidator<EditInterfaceVM>
    {
        public EditInterfaceVMVMValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }

    public class EditInterfaceVMProfile : Profile
    {
        public EditInterfaceVMProfile()
           => CreateMap<EditInterfaceVM, EditInterfaceRequest>();
    }
}
