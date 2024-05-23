using FluentValidation;

namespace Application.Features.Reports.Commands.Create;

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.ReportTitle).NotEmpty().MinimumLength(4).MaximumLength(100);
        RuleFor(c => c.ReportDate).NotEmpty();
        RuleFor(c => c.ReportContent).NotEmpty().MinimumLength(2).MaximumLength(500);
        RuleFor(c => c.Status).NotEmpty();
    }
}