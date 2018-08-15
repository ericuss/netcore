
using Microsoft.AspNetCore.Mvc;

namespace Lanre.Clients.Api.UnitTests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using Tests.Core;
    using Lanre.Clients.Api.Controllers.V1;

    public class ValuesControllerTests : TestBase
    {
        [Fact]
        public async Task Get_All_And_Return_String_Array()
        {
            // Arrange
            const int expected_results = 2;
            var controller = new ValuesController();

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var entities = Assert.IsType<string[]>(okResult.Value);

            Assert.NotNull(entities);
            Assert.Equal(expected_results, entities.Count());
        }
    }
}
