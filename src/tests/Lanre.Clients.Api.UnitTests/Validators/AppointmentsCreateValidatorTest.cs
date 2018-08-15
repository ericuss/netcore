
namespace Lanre.Clients.Api.UnitTests.Validators
{
    using System;   
    using System.Threading.Tasks;
    using Xunit;
    using Models.Appointment;

    public class AppointmentsCreateValidatorTest
    {
        [Fact]
        public async Task Validate_Correct_Entity()
        {
            var expected_errors = 0;
            var dummy_entity = new AppointmentCreate() { Description = "this is a description" };
            var validator = new AppointmentCreateValidator();
            var result = await validator.ValidateAsync(dummy_entity);

            Assert.True(result.IsValid);
            Assert.Equal(expected_errors, result.Errors.Count);
        }

        [Fact]
        public async Task Validate_Empty_Description_Return_Is_Invalid()
        {
            var expected_errors = 1;
            var dummy_entity = new AppointmentCreate() { Description = String.Empty };
            var validator = new AppointmentCreateValidator();
            var result = await validator.ValidateAsync(dummy_entity);

            Assert.False(result.IsValid);
            Assert.Equal(expected_errors, result.Errors.Count);
        }
    }
}
