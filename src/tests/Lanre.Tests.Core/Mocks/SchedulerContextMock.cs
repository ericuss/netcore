
namespace Lanre.Tests.Core.Mocks
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Data.Context.Contexts;
    using Moq;

    public class SchedulerContextMock
    {
        public static DbContextOptions<SchedulerContext> ScheduleContextOptionMocked
            => new DbContextOptionsBuilder<SchedulerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        public static Mock<SchedulerContext> Mocked(Action<Mock<SchedulerContext>> action)
        {
            var dbOptions = ScheduleContextOptionMocked;
            var env = IHostingEnvironmentMock.Mocked;
            var context = new Mock<SchedulerContext>(dbOptions, env.Object);
            action(context);
            return context;
        }
    }
}
