using FluentValidation;

namespace Application.Features.Reservations.Commands.Delete;

public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
{
    public DeleteReservationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}