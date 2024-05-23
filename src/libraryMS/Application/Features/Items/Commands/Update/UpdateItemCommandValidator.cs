using FluentValidation;

namespace Application.Features.Items.Commands.Update;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(1).MaximumLength(50).Must(name => char.IsUpper(name[0])).WithMessage("First letter of the name must be uppercase.");  // Bunu setter içerisinde ctor ile yapadabiliriz.
        RuleFor(c => c.Type).NotEmpty().MinimumLength(2).MaximumLength(30);
        RuleFor(c => c.Isbn).NotEmpty().Must(isbn => isbn.Length.Equals(8) || isbn.Length.Equals(10) || isbn.Length.Equals(13)).WithMessage("ISBN-ISSN must be either 8, 10 or 13 characters long.");
        RuleFor(c => c.Category).NotEmpty().MinimumLength(2).MaximumLength(40);
        RuleFor(c => c.Genre).NotEmpty().MinimumLength(2).MaximumLength(40);
        RuleFor(c => c.PublicationDate).NotEmpty().Must(date => date <= DateTime.Today).WithMessage("Publication date must be today or in the past.");
        RuleFor(c => c.TotalPages).NotEmpty().GreaterThan((short)0).WithMessage("Total pages must be greater than zero.");
        RuleFor(c => c.Language).NotEmpty().MinimumLength(2).MaximumLength(40);
        RuleFor(c => c.Description).MinimumLength(2).MaximumLength(500);
        RuleFor(c => c.PublisherId).NotEmpty();
        RuleFor(c => c.LocationId).NotEmpty();
        RuleFor(c=>c.LibraryId).NotEmpty();
        RuleFor(i => i.InStock).NotEmpty().GreaterThan(0);
    }
}