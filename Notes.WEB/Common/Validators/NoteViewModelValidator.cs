using FluentValidation;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Common.Validators
{
    public class NoteViewModelValidator : AbstractValidator<NoteViewModel>
    {
        public NoteViewModelValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Title).MaximumLength(15);
            RuleFor(x => x.Content).MaximumLength(50);
        }
    }
}
