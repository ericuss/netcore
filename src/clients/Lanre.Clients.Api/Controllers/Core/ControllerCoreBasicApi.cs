namespace Lanre.Clients.Api.Controllers.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Lanre.Infrastructure.Entities.Core;

    public abstract class ControllerCoreBasicApi<TEntity, TCreateModel> : ControllerCore
    where TEntity : EntityCore
    {
        protected static IList<TEntity> Data = new List<TEntity>();

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(Data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null) this.NotFound();

            return this.Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TCreateModel objectTEntityoCreate)
        {
            if (objectTEntityoCreate == null) this.NotFound();

            var entityMapped = this.CreateEntity(objectTEntityoCreate);

            Data.Add(entityMapped);

            return this.Ok(entityMapped);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TCreateModel objectTEntityoUpdate)
        {
            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null) this.NotFound();

            this.UpdateEntity(objectTEntityoUpdate, result);

            return this.Ok(result);
        }

        [HttpDelete("{id}")]
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
