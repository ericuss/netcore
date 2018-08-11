namespace Lanre.Clients.Api.Controllers.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Swashbuckle.AspNetCore.Annotations;
    using Lanre.Infrastructure.Entities.Core;
    using Infrastructure.Entities;

    public abstract class ControllerCoreBasicApi<TClass, TEntity, TCreateModel> : ControllerCore
    where TClass : ControllerCoreBasicApi<TClass, TEntity, TCreateModel>
    where TEntity : EntityCore
    where TCreateModel : class
    {
        protected static IList<TEntity> Data = new List<TEntity>();
        protected readonly ILogger<TClass> _logger;

        protected ControllerCoreBasicApi(ILogger<TClass> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Get entity", typeof(IEnumerable<Appointment>))]
        public IActionResult Get()
        {
            _logger.LogInformation($"Get list of {typeof(TEntity)}");
            return this.Ok(Data);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get entity by id", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation($"Get {typeof(TEntity)} by id: {id}");
            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                _logger.LogWarning($"Not found: Get {typeof(TEntity)} by id: {id}");
                return this.NotFound();
            }

            return this.Ok(result);

        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Entity has created", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Argument is not valid", typeof(ArgumentException))]
        public IActionResult Post([FromBody] TCreateModel objectTEntityoCreate)
        {
            _logger.LogInformation($"Create {typeof(TEntity)}: {objectTEntityoCreate}");
            if (!this.ModelState.IsValid || objectTEntityoCreate == null)
            {
                _logger.LogError($"Model is invalid or entity null: Create {typeof(TEntity)} {objectTEntityoCreate}");
                throw new ArgumentException();
            }

            var entityMapped = this.CreateEntity(objectTEntityoCreate);

            Data.Add(entityMapped);

            _logger.LogInformation($"Created {typeof(TEntity)}: {entityMapped}");
            return this.Created($"Entity/{entityMapped.Id.ToString()}", entityMapped);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Entity has updated", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Argument is not valid", typeof(ArgumentException))]
        public IActionResult Put(Guid id, [FromBody] TCreateModel objectTEntityoUpdate)
        {
            _logger.LogInformation($"Update {typeof(TEntity)} id: {id}, entity: {objectTEntityoUpdate}");
            if (!this.ModelState.IsValid)
            {
                _logger.LogError($"Model is invalid: Update {typeof(TEntity)}  id: {id}, entity: {objectTEntityoUpdate}");
                throw new ArgumentException();
            }

            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                _logger.LogError($"Not found: Update {typeof(TEntity)}  id: {id}, entity: {objectTEntityoUpdate}");
                return this.NotFound();
            }

            this.UpdateEntity(objectTEntityoUpdate, result);

            _logger.LogInformation($"Updated {typeof(TEntity)} id: {id}, entity: {objectTEntityoUpdate}");
            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Entity has deleted", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation($"Delete {typeof(TEntity)} by id: {id}");
            var result = Data.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                _logger.LogError($"Not found: Delete {typeof(TEntity)} by id: {id}");
                return this.NotFound();
            }

            Data.Remove(result);

            _logger.LogInformation($"Deleted {typeof(TEntity)} by id: {id}");
            return this.Ok(result);
        }

        protected abstract TEntity CreateEntity(TCreateModel objectTEntityoCreate);

        protected abstract void UpdateEntity(TCreateModel objectTEntityoUpdate, TEntity original);
    }
}
