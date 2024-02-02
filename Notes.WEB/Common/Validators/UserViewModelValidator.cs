using FluentValidation;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Common.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(r => r.UserName).NotEmpty();
            RuleFor(r => r.Password).MinimumLength(8);
        }
    }
}
