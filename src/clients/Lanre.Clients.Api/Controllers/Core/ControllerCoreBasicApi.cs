
namespace Lanre.Clients.Api.Controllers.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Swashbuckle.AspNetCore.Annotations;
    using Lanre.Infrastructure.Entities.Core;
    using Infrastructure.Entities;

    public abstract class ControllerCoreBasicApi<TEntity, TCreateModel> : ControllerCore
    where TEntity : EntityCore
    {
        protected static IList<TEntity> Data = new List<TEntity>();

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Get entity", typeof(IEnumerable<Appointment>))]
        public IActionResult Get()
        {
            return this.Ok(Data);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get entity by id", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null) this.NotFound();

            return this.Ok(result);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Entity has created", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] TCreateModel objectTEntityoCreate)
        {
            if (objectTEntityoCreate == null) this.NotFound();

            var entityMapped = this.CreateEntity(objectTEntityoCreate);

            Data.Add(entityMapped);

            return this.Created($"Entity/{entityMapped.Id.ToString()}", entityMapped);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Entity has updated", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult Put(Guid id, [FromBody] TCreateModel objectTEntityoUpdate)
        {
            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null) this.NotFound();

            this.UpdateEntity(objectTEntityoUpdate, result);

            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Entity has deleted", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null) this.NotFound();

            Data.Remove(result);

            return this.Ok(result);
        }

        protected abstract TEntity CreateEntity(TCreateModel objectTEntityoCreate);

        protected abstract void UpdateEntity(TCreateModel objectTEntityoUpdate, TEntity original);
    }
}
