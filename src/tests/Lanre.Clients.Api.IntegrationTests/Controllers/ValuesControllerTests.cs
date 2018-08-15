
namespace Lanre.Clients.Api.IntegrationTests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using Tests.Core;

    public class ControllerCoreBasicApiTests : IntegrationTestBase
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
