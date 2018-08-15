
namespace Lanre.Tests.Core
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Xunit;

    public class IntegrationTestBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly string Url;

        protected IntegrationTestBase(string url) : this(url, null)
        {
        }

        protected IntegrationTestBase(string url, Action<IServiceCollection> configureServices)
        {
            Server = ServerFactory.Server(configureServices);
            Client = Server.CreateClient();
            Url = url;
        }

        protected async Task<ResultEntity<TResult>> GetAsync<TResult>(
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult>(Client.GetAsync, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        protected async Task<ResultEntity<TResult>> DeleteAsync<TResult, TData>(
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult>(Client.DeleteAsync, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        protected async Task<ResultEntity<TResult>> PostAsync<TResult, TData>(
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult, TData>(Client.PostAsync, data, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        protected async Task<ResultEntity<TResult>> PutAsync<TResult, TData>(
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult, TData>(Client.PutAsync, data, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        protected async Task<ResultEntity<TResult>> ActionAsync<TResult, TData>(
            Func<string, HttpContent, Task<HttpResponseMessage>> action,
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            url = this.FormatUrl(url);
            var serializedData = this.Serialize(data);
            var response = await action(url, serializedData);

            if (successStatusCode) response.EnsureSuccessStatusCode();

            if (expectedStatusCode.HasValue) Assert.Equal(expectedStatusCode, response.StatusCode);

            if (deserialize)
            {
                var entities = await this.Deserialize<TResult>(response);
                return new ResultEntity<TResult>(entities);
            }

            return null;
        }

        protected async Task<ResultEntity<TResult>> ActionAsync<TResult>(
            Func<string, Task<HttpResponseMessage>> action,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            url = this.FormatUrl(url);

            var response = await action(url);
            if (successStatusCode) response.EnsureSuccessStatusCode();

            if (expectedStatusCode.HasValue) Assert.Equal(expectedStatusCode, response.StatusCode);

            if (deserialize)
            {
                var entities = await this.Deserialize<TResult>(response);
                return new ResultEntity<TResult>(entities);
            }

            return null;
        }

        protected StringContent Serialize<TData>(TData data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        protected async Task<TResult> Deserialize<TResult>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<TResult>(responseString);
            return entities;
        }

        protected string FormatUrl(string url)
        {
            return string.IsNullOrEmpty(url) ? Url : url;
        }

    }
}
