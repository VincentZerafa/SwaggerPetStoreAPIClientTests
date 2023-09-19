using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPIClient.Clients
{
    public abstract class APIClient
    {
        public APIClient() : base()
        {
            Configuration = Config.Init();
            BaseUrl = new Uri(Configuration.GetSection("testSettings:BaseUrl").Value);
        }

        public Uri BaseUrl { get; set; }
        public IConfiguration Configuration { get; set; }

        public HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = BaseUrl;
            client.DefaultRequestHeaders.Add("api_key", "special-key");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<string> CallGetAPI(string url)
        {
            HttpClient client = CreateHttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CallDeleteAPI(string url)
        {
            HttpClient client = CreateHttpClient();
            HttpResponseMessage response = await client.DeleteAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CallPostAPIWithFormData(string url, List<KeyValuePair<string, string>> formData)
        {
            HttpClient client = CreateHttpClient();

            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            foreach (KeyValuePair<string, string> field in formData)
            {
                if (!string.IsNullOrEmpty(field.Value))
                    content.Add(new StringContent(field.Value), field.Key);
            }

            HttpResponseMessage response = await client.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CallPostAPIWithFile(string url, string additionalMetadata, byte[] file)
        {
            HttpClient client = CreateHttpClient();

            MultipartFormDataContent content = new MultipartFormDataContent("--");
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "multipart/form-data");

            content.Add(new StringContent(additionalMetadata), "additionalMetadata");
            content.Add(new StreamContent(new MemoryStream(file)), "file", "test.avif");

            HttpResponseMessage response = await client.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CallPostAPI(string url, object content)
        {
            HttpClient client = CreateHttpClient();
            HttpResponseMessage response = await client.PostAsync(url, SerializeContent(content));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CallPutAPI(string url, object content)
        {
            HttpClient client = CreateHttpClient();
            HttpResponseMessage response = await client.PutAsync(url, SerializeContent(content));
            return await response.Content.ReadAsStringAsync();
        }

        public StringContent SerializeContent(object value)
        {
            string jsonObject = JsonConvert.SerializeObject(value);
            return new StringContent(jsonObject, Encoding.UTF8, "application/json");
        }
    }
}
