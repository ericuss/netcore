
using Moq;

namespace Lanre.Clients.Api.UnitTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;
    using Infrastructure.Entities;
    using Models.Appointment;
    using Tests.Core;
    using Lanre.Clients.Api.Controllers.V1;

    public class AppointmentsControllerTests : TestBaseApi<AppointmentsController, Appointment, AppointmentCreate>
    {
        protected override List<Appointment> GenerateData()
        {
            return new List<Appointment>()
            {
                new Appointment{ Id = Guid.Parse("7dc436a5-29b9-475d-a394-f97a691f74f3"), Description = "Descripcion 1" },
                new Appointment{ Id = Guid.Parse("e4fb1be6-4ba7-40c7-beef-e2b8c32751b6"), Description = "Descripcion 2" },
                new Appointment{ Id = Guid.Parse("4da90981-6a19-48a1-a6d0-c1f413dc748d"), Description = "Descripcion 3" },
                new Appointment{ Id = Guid.Parse("1849dfa8-9fac-4b0c-a63f-e3198935dc7b"), Description = "Descripcion 4" },
                new Appointment{ Id = Guid.Parse("dc8fabd1-09b6-427f-8454-2066a70d2828"), Description = "Descripcion 5" },
                new Appointment{ Id = Guid.Parse("c66f9fae-ea7c-43cf-b5bf-54f9164489f8"), Description = "Descripcion 6" },
            };
        }

        [Fact]
        public void GetInfo_Return_String_information()
        {
            // Arrange
            const string expected_result = "information";

            // Act
            var result = this._controller.Info();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var mappedResult = Assert.IsType<string>(okResult.Value);

            //Assert.NotNull(result);
            Assert.Equal(expected_result, mappedResult);
        }

        [Fact]

        public async Task Get_All_And_Return_Ok()
        {
            // Arrange
            // Act
            var result = await this._controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]

        public async Task Get_All_And_Return_6_items()
        {
            // Arrange
            var expected_count = 6;
            // Act
            var result = await this._controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var entities = Assert.IsType<List<Appointment>>(okResult.Value);
            Assert.Equal(expected_count, entities.Count);
        }

        [Fact]
        public async Task Get_By_Id_And_Return_Not_Found()
        {
            // Arrange
            var randomId = Guid.NewGuid();

            // Act
            var result = await this._controller.Get(randomId);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_By_Id_And_Return_Correct_Entity()
        {
            // Arrange
            const string expected_description = "Descripcion 1";
            var id_item = Guid.Parse("7dc436a5-29b9-475d-a394-f97a691f74f3");

            // Act
            var result = await this._controller.Get(id_item);

            // Assert

            var okResult = Assert.IsType<OkObjectResult>(result);
            var entity = Assert.IsType<Appointment>(okResult.Value);

            Assert.NotNull(entity);
            Assert.Equal(expected_description, entity.Description);
        }

        [Fact]
        public async Task Create_Appointment_And_Return_Argument_Exception()
        {
            var dummy_entity = new AppointmentCreate() { Description = string.Empty };

            this._controller.ModelState.AddModelError("key", "error message");

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await this._controller.Post(dummy_entity)
                );

        }

        [Fact]
        public async Task Create_Appointment_And_Return_Created()
        {
            // Arrange
            const string expected_description_create = "This is description";
            var dummy_entity_create = new AppointmentCreate() { Description = expected_description_create };

            // Act
            var result = await this._controller.Post(dummy_entity_create);

            // Assert

            var okResult = Assert.IsType<CreatedResult>(result);
            var entity = Assert.IsType<Appointment>(okResult.Value);

            Assert.NotNull(entity);
            Assert.Equal(expected_description_create, entity.Description);
        }

        [Fact]
        public async Task Update_Appointment_And_Return_Entity_Updated()
        {
            // Arrange
            const string expected_description = "Descripcion 1aksdlkasjd";
            var id_item = Guid.Parse("7dc436a5-29b9-475d-a394-f97a691f74f3");
            var dummy_entity_update = new AppointmentCreate() { Description = expected_description };

            // Act
            var result = await this._controller.Put(id_item, dummy_entity_update);

            // Assert

            var okResult = Assert.IsType<OkObjectResult>(result);
            var entity = Assert.IsType<Appointment>(okResult.Value);

            Assert.NotNull(entity);
            Assert.Equal(expected_description, entity.Description);
        }

        [Fact]
        public async Task Update_Appointment_And_Return_Not_Found()
        {

            // Arrange
            const string expected_description = "Descripcion 1aksdlkasjd";
            var id_item = Guid.NewGuid();
            var dummy_entity_update = new AppointmentCreate() { Description = expected_description };

            // Act
            var result = await this._controller.Put(id_item, dummy_entity_update);

            // Assert

            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_Appointment_And_Return_Argument_Exception()
        {
            var dummy_entity_update = new AppointmentCreate() { Description = string.Empty };

            var id = Guid.NewGuid();

            this._controller.ModelState.AddModelError("key", "error message");

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await this._controller.Put(id, dummy_entity_update)
            );
        }

        [Fact]
        public async Task Delete_Appointment_And_Return_Success()
        {
            // Arrange
            const string expected_description = "Descripcion 1";
            var id_item = Guid.Parse("7dc436a5-29b9-475d-a394-f97a691f74f3");

            // Act
            var result = await this._controller.Delete(id_item);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var entity = Assert.IsType<Appointment>(okResult.Value);
            Assert.NotNull(entity);
            Assert.Equal(expected_description, entity.Description);
        }


        [Fact]
        public async Task Delete_Appointment_And_Return_Not_Found()
        {

            var id = Guid.NewGuid();

            // Act
            var result = await this._controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

    }
}
