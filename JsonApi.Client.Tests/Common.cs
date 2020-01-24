using System.Net.Http;

namespace JsonApi.Client.Tests
{
    public class Common
    {
        public static string ApiKey { get; } = "--yourkey--";

        private static HttpClient _httpClient = null;
        public static HttpClient HttpClient {
            get {
                if (_httpClient == null) {
                    _httpClient = new HttpClient();
                }
                return _httpClient;
            }
        }

        private static Client _client = null;
        public static Client Client {
            get {
                if (_client == null) {
                    _client = new Client(Common.ApiKey);
                }
                return _client;
            }
        }
    }
}
