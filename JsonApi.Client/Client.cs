using JsonApi.Client.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JsonApi.Client
{
    public class Client
    {
        private readonly HttpClient _client;
        private readonly string _apiEndpoint = "https://jsonapi.gunterweb.ca/api";
        private readonly string _apiKey;

        public Client(string apiKey, string apiEndpoint = "") {
            this._apiKey = apiKey;

            this._client = new HttpClient();
            this._client.Timeout = new System.TimeSpan(0, 5, 0);
            this._client.DefaultRequestHeaders.Clear();
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this._client.DefaultRequestHeaders.Add("Authorization", _apiKey);

            if (!string.IsNullOrEmpty(apiEndpoint)) {
                this._apiEndpoint = apiEndpoint;
            }
        }

        public Client(HttpClient client, string apiKey, string apiEndpoint = "https://jsonapi.gunterweb.ca/api") {
            this._apiKey = apiKey;
            this._client = client;
            this._client.DefaultRequestHeaders.Add("Authorization", _apiKey);

            if (!string.IsNullOrEmpty(apiEndpoint)) {
                this._apiEndpoint = apiEndpoint;
            }
        }

        public async Task<List<JsonObject>> GetObjects(string bucketName, int page = 0) {
            var resp = await _client.GetAsync(this.GetUrl(bucketName, null, page));
            return await this.GetResult<List<JsonObject>>(resp);
        }

        public async Task<JsonObject> GetObject(string bucketName, string dataId) {
            var resp = await _client.GetAsync(this.GetUrl(bucketName, dataId));
            return await this.GetResult<JsonObject>(resp);
        }

        public async Task<string> CreateObject(string bucketName, string jsonData) {
            var resp = await _client.PostAsync(this.GetUrl(bucketName), new StringContent(jsonData, Encoding.UTF8));
            return await this.GetResult<string>(resp);
        }

        public async Task<bool> DeleteObject(string bucketName, string dataId) {
            var resp = await _client.DeleteAsync(this.GetUrl(bucketName, dataId));
            return await this.GetResult<bool>(resp);
        }

        public async Task<bool> UpdateObject(string bucketName, string dataId, string newContent) {
            var resp = await _client.PutAsync(this.GetUrl(bucketName, dataId), new StringContent(newContent, Encoding.UTF8));
            return await this.GetResult<bool>(resp);
        }

        private string GetUrl(string bucketName, string dataId = "", int page = 0) {
            string url = $"{_apiEndpoint}/{bucketName}";

            if (!string.IsNullOrEmpty(dataId)) {
                url += $"/{dataId}";   
            }

            if (page >= 1) {
                url += $"?page={page}";
            }

            return url;
        }

        private async Task<T> GetResult<T>(HttpResponseMessage resp) {
            if (resp.StatusCode == System.Net.HttpStatusCode.OK || resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                string jsonData = await resp.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
            } else {
                return default(T);
            }
        }
    }
}
