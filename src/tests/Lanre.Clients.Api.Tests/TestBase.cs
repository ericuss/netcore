
namespace Lanre.Clients.Api.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.TestHost;
    using Newtonsoft.Json;
    using Xunit;

    public class TestBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly string Url;

        internal TestBase(string url)
        {
            Server = ServerFactory.Server;
            Client = Server.CreateClient();
            Url = url;
        }

        internal async Task<ResultEntity<TResult>> GetAsync<TResult>(
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult>(Client.GetAsync, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        internal async Task<ResultEntity<TResult>> DeleteAsync<TResult, TData>(
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult>(Client.DeleteAsync, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        internal async Task<ResultEntity<TResult>> PostAsync<TResult, TData>(
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult, TData>(Client.PostAsync, data, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        internal async Task<ResultEntity<TResult>> PutAsync<TResult, TData>(
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
        {
            var entities = await this.ActionAsync<TResult, TData>(Client.PutAsync, data, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        internal async Task<ResultEntity<TResult>> ActionAsync<TResult, TData>(
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

        internal async Task<ResultEntity<TResult>> ActionAsync<TResult>(
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

        internal StringContent Serialize<TData>(TData data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        internal async Task<TResult> Deserialize<TResult>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<TResult>(responseString);
            return entities;
        }

        internal string FormatUrl(string url)
        {
            return string.IsNullOrEmpty(url) ? Url : url;
        }

    }

    internal class ResultEntity<TResult>
    {
        public ResultEntity(TResult result) => Result = result;

        public TResult Result { get; set; }
    }
}
