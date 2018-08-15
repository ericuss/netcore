
using System;
using System.Collections.Generic;
using System.Reflection;
using Lanre.Clients.Api.Controllers.Core;
using Lanre.Clients.Api.Controllers.V1;
using Lanre.Data.Context.Contexts;
using Lanre.Infrastructure.Cache;
using Lanre.Infrastructure.Entities;
using Lanre.Infrastructure.Entities.Core;
using Lanre.Tests.Core.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;

namespace Lanre.Tests.Core
{
    public class TestBaseApi<TController, TEntity, TCreateModel>
        where TController : ControllerCoreBasicApi<TController, TEntity, TCreateModel>
        where TEntity : EntityCore
        where TCreateModel : class

    {
        protected Mock<ICustomMemoryCache> _cache;
        protected Mock<ILogger<AppointmentsController>> _logger;
        protected Mock<SchedulerContext> _context;
        protected TController _controller;

        public TestBaseApi()
        {
            this.GenerateMocks();
            this.CreateController();

        }

        protected virtual void GenerateMocks()
        {
            this._cache = ICustomMemoryCacheMock.Mocked;
            this._logger = ILoggerMock.Mocked<AppointmentsController>();

            this._context = SchedulerContextMock.Mocked(ctx =>
            {
                //ctx.Setup(x => x.).ReturnsDbSet(new List<Appointment>());
                ctx.Setup(x => x.Set<TEntity>()).ReturnsDbSet(GenerateData());
            });
        }


        protected virtual List<TEntity> GenerateData()
        {
            return new List<TEntity>();
        }

        protected virtual void CreateController()
        {
            //var controller = Activator.CreateInstance<TController>();
            ConstructorInfo constructor = typeof(TController).GetConstructor(new[]
            {
                typeof(ILogger<TController>),
                typeof(ICustomMemoryCache),
                typeof(SchedulerContext)
            });

            var argValues = new object[] {
                _logger.Object,
                _cache.Object,
                _context.Object
            };

            this._controller = (TController)constructor.Invoke(argValues);

            this._controller.ControllerContext.HttpContext = HttpContextMock.Mocked.Object;
        }
    }
}
