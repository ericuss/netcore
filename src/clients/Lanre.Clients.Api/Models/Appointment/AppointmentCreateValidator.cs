using FluentValidation;

namespace Lanre.Clients.Api.Models.Appointment
{
    public class AppointmentCreateValidator : AbstractValidator<AppointmentCreate>
    {
        public AppointmentCreateValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
