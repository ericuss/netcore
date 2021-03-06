﻿using System.Threading.Tasks;
using Lanre.Data.Context.Contexts;
using Lanre.Infrastructure.Cache;
using Microsoft.Extensions.Caching.Memory;

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
    using Microsoft.EntityFrameworkCore;

    public abstract class ControllerCoreBasicApi<TClass, TEntity, TCreateModel> : ControllerCore
    where TClass : ControllerCoreBasicApi<TClass, TEntity, TCreateModel>
    where TEntity : EntityCore
    where TCreateModel : class
    {
        //protected static IList<TEntity> Data = new List<TEntity>();
        protected readonly ILogger<TClass> _logger;
        protected readonly ICustomMemoryCache _cache;
        protected string _cacheKey_all;
        protected string _cacheKey_byId;
        protected readonly SchedulerContext _context;
        private readonly DbSet<TEntity> _query;

        protected ControllerCoreBasicApi(ILogger<TClass> logger, ICustomMemoryCache cache, SchedulerContext scheduleContext)
        {
            this._logger = logger;
            this._cache = cache;
            this._cacheKey_all = $"{typeof(TEntity)}_All";
            this._cacheKey_byId = $"{typeof(TEntity)}_byId:";
            this._context = scheduleContext;
            this._query = this._context.Set<TEntity>();
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Get entity", typeof(IEnumerable<Appointment>))]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"Get list of {typeof(TEntity)}");
            _cache.AddKey(_cacheKey_all);

            if (_cache.TryGetValue(_cacheKey_all, out IActionResult cachedResult))
            {
                Response.Headers.Add("cache", "cached");
                return cachedResult;
            }

            Response.Headers.Add("cache", "non-cached");
            var result = this.Ok(await this._query.ToListAsync());
            _cache.Set(_cacheKey_all, result);
            return result;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get entity by id", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"Get {typeof(TEntity)} by id: {id}");

            var key = $"{_cacheKey_byId}{id}";
            _cache.AddKey(key);
            if (_cache.TryGetValue(key, out IActionResult cacheResult))
            {
                Response.Headers.Add("cache", "cached");
                return cacheResult;
            }

            Response.Headers.Add("cache", "non-cached");

            var entity = await this._query.FirstOrDefaultAsync(x => x.Id == id);
            IActionResult result;
            if (entity == null)
            {
                _logger.LogWarning($"Not found: Get {typeof(TEntity)} by id: {id}");
                result = this.NotFound();
            }
            else
            {
                result = this.Ok(entity);
            }

            _cache.Set(key, result);
            return result;

        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Entity has created", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Argument is not valid", typeof(ArgumentException))]
        public async Task<IActionResult> Post([FromBody] TCreateModel objectTEntityoCreate)
        {
            _logger.LogInformation($"Create {typeof(TEntity)}: {objectTEntityoCreate}");
            if (!this.ModelState.IsValid || objectTEntityoCreate == null)
            {
                _logger.LogError($"Model is invalid or entity null: Create {typeof(TEntity)} {objectTEntityoCreate}");
                throw new ArgumentException();
            }

            var entityMapped = this.CreateEntity(objectTEntityoCreate);

            await this._query.AddAsync(entityMapped);
            await this._context.SaveChangesAsync();

            _logger.LogInformation($"Created {typeof(TEntity)}: {entityMapped}");
            _cache.Remove(_cacheKey_all);
            _cache.RemoveKey(_cacheKey_all);
            return this.Created($"Entity/{entityMapped.Id.ToString()}", entityMapped);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Entity has updated", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Argument is not valid", typeof(ArgumentException))]
        public async Task<IActionResult> Put(Guid id, [FromBody] TCreateModel objectTEntityoUpdate)
        {
            _logger.LogInformation($"Update {typeof(TEntity)} id: {id}, entity: {objectTEntityoUpdate}");
            if (!this.ModelState.IsValid)
            {
                _logger.LogError($"Model is invalid: Update {typeof(TEntity)}  id: {id}, entity: {objectTEntityoUpdate}");
                throw new ArgumentException();
            }

            var result = await this._query.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                _logger.LogError($"Not found: Update {typeof(TEntity)}  id: {id}, entity: {objectTEntityoUpdate}");
                return this.NotFound();
            }

            this.UpdateEntity(objectTEntityoUpdate, result);
            this._query.Update(result);
            await this._context.SaveChangesAsync();

            _logger.LogInformation($"Updated {typeof(TEntity)} id: {id}, entity: {objectTEntityoUpdate}");
            _cache.RemoveStartingBy(typeof(TEntity).ToString());
            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Entity has deleted", typeof(Appointment))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Delete {typeof(TEntity)} by id: {id}");
            var result = await this._query.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                _logger.LogError($"Not found: Delete {typeof(TEntity)} by id: {id}");
                return this.NotFound();
            }

            this._query.Remove(result);
            await this._context.SaveChangesAsync();

            _logger.LogInformation($"Deleted {typeof(TEntity)} by id: {id}");
            _cache.RemoveStartingBy(typeof(TEntity).ToString());
            return this.Ok(result);
        }

        protected abstract TEntity CreateEntity(TCreateModel objectTEntityoCreate);

        protected abstract void UpdateEntity(TCreateModel objectTEntityoUpdate, TEntity original);
    }
}
