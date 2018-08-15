namespace Lanre.Tests.Core.Mocks
{
    using Microsoft.AspNetCore.Http;
    using Moq;

    public class HttpContextMock

    {
        public static Mock<HttpContext> Mocked
        {
            get
            {
                var httpCtxStub = new Mock<HttpContext>();
                var httpResponse = new Mock<HttpResponse>();
                var httpReaderResponse = new Mock<IHeaderDictionary>();

                httpResponse.Setup(x => x.Headers).Returns(httpReaderResponse.Object);
                httpCtxStub.Setup(x => x.Response).Returns(httpResponse.Object);

                return httpCtxStub;
            }
        }
    }
}
