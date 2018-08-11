namespace Lanre.Clients.Api.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ControllerCoreBasicApiTests : TestBase
    {
        public ControllerCoreBasicApiTests() : base("/api/values") { }

        [Fact]
        public async Task Get_All_And_Return_String_Array()
        {
            const int expected_results = 2;

            var entities = await this.GetAsync<IEnumerable<string>>();

            Assert.NotNull(entities);
            Assert.Equal(expected_results, entities.Result.Count());
        }
    }
}
