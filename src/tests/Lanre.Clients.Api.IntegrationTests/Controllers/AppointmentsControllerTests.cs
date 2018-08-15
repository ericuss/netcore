namespace Lanre.Clients.Api.IntegrationTests.Controllers
{
    using System;
    using System.Net;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using Infrastructure.Entities;
    using Lanre.Tests.Core;
    using Models.Appointment;

    public class AppointmentsControllerTests : IntegrationTestBase
    {
        public AppointmentsControllerTests() : base("/api/appointments") { }

        [Fact]

        public async Task Get_All_And_Return_Ok()
        {
            var entities = await this.GetAsync<IEnumerable<Appointment>>();
        }

        [Fact]
        public async Task Get_By_Id_And_Return_Not_Found()
        {
            var randomId = Guid.NewGuid();

            var entity = await this.GetAsync<string>($"{this.Url}/{randomId}",
                successStatusCode: false,
                expectedStatusCode: HttpStatusCode.NotFound,
                deserialize: false
            );
        }

        [Fact]
        public async Task Get_By_Id_And_Return_Correct_Entity()
        {
            const string expected_description_create = "This is description";
            var dummy_entity_create = new AppointmentCreate() { Description = expected_description_create };

            var entityCreated = await this.PostAsync<Appointment, AppointmentCreate>(
                data: dummy_entity_create,
                expectedStatusCode: HttpStatusCode.Created
            );

            Assert.NotNull(entityCreated);
            Assert.Equal(expected_description_create, entityCreated.Result.Description);

            var entity = await this.GetAsync<Appointment>($"{this.Url}/{entityCreated.Result.Id}",
                successStatusCode: true,
                deserialize: true
            );

            Assert.NotNull(entity);
            Assert.Equal(expected_description_create, entity.Result.Description);
        }

        [Fact]
        public async Task Create_Appointment_And_Return_Argument_Exception()
        {
            var dummy_entity = new AppointmentCreate() { Description = string.Empty };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.PostAsync<AppointmentCreate, AppointmentCreate>(
                     data: dummy_entity,
                     url: $"{this.Url}",
                     expectedStatusCode: HttpStatusCode.Created
                 )
            );
        }
        [Fact]
        public async Task Create_Appointment_And_Return_Created()
        {
            const string expected_description = "This is description";
            var dummy_entity = new AppointmentCreate() { Description = expected_description };

            var entity = await this.PostAsync<AppointmentCreate, AppointmentCreate>(
                data: dummy_entity,
                url: $"{this.Url}",
                expectedStatusCode: HttpStatusCode.Created
            );

            Assert.NotNull(entity);
            Assert.Equal(expected_description, entity.Result.Description);
        }

        [Fact]
        public async Task Update_Appointment_And_Return_Entity_Updated()
        {
            const string expected_description_create = "This is description";
            const string expected_description_update = "This is description Updated";
            var dummy_entity_create = new AppointmentCreate() { Description = expected_description_create };
            var dummy_entity_update = new AppointmentCreate() { Description = expected_description_update };

            var entity = await this.PostAsync<Appointment, AppointmentCreate>(
                data: dummy_entity_create,
                expectedStatusCode: HttpStatusCode.Created
            );

            Assert.NotNull(entity);
            Assert.Equal(expected_description_create, entity.Result.Description);

            var id = entity.Result.Id;

            var entityUpdated = await this.PutAsync<Appointment, AppointmentCreate>(
                data: dummy_entity_update,
                url: $"{this.Url}/{id}"
            );

            Assert.NotNull(entityUpdated);
            Assert.Equal(expected_description_update, entityUpdated.Result.Description);
        }

        [Fact]
        public async Task Update_Appointment_And_Return_Not_Found()
        {
            const string expected_description_update = "This is description Updated";
            var dummy_entity_update = new AppointmentCreate() { Description = expected_description_update };

            var id = Guid.NewGuid();

            var entityUpdated = await this.PutAsync<Appointment, AppointmentCreate>(
                data: dummy_entity_update,
                url: $"{this.Url}/{id}",
                successStatusCode: false,
                expectedStatusCode: HttpStatusCode.NotFound,
                deserialize: false
            );

            Assert.Null(entityUpdated);
        }

        [Fact]
        public async Task Update_Appointment_And_Return_Argument_Exception()
        {
            var dummy_entity_update = new AppointmentCreate() { Description = string.Empty };

            var id = Guid.NewGuid();

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.PutAsync<Appointment, AppointmentCreate>(
                    data: dummy_entity_update,
                    url: $"{this.Url}/{id}",
                    successStatusCode: false,
                    expectedStatusCode: HttpStatusCode.NotFound,
                    deserialize: false
                )
            );
        }

        [Fact]
        public async Task Delete_Appointment_And_Return_Success()
        {
            const string expected_description = "This is description";
            var dummy_entity_create = new AppointmentCreate() { Description = expected_description };

            var entity = await this.PostAsync<Appointment, AppointmentCreate>(
                data: dummy_entity_create,
                expectedStatusCode: HttpStatusCode.Created
            );

            Assert.NotNull(entity);
            Assert.Equal(expected_description, entity.Result.Description);

            var id = entity.Result.Id;

            var entityUpdated = await this.DeleteAsync<Appointment, AppointmentCreate>(
                url: $"{this.Url}/{id}"
            );

            Assert.NotNull(entityUpdated);
            Assert.Equal(expected_description, entityUpdated.Result.Description);
        }

        [Fact]
        public async Task Delete_Appointment_And_Return_Not_Found()
        {

            var id = Guid.NewGuid();

            var entityUpdated = await this.DeleteAsync<Appointment, AppointmentCreate>(
                url: $"{this.Url}/{id}",
                successStatusCode: false,
                deserialize: false,
                expectedStatusCode: HttpStatusCode.NotFound
            );

            Assert.Null(entityUpdated);
        }

    }
}
