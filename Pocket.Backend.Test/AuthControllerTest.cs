using Newtonsoft.Json.Linq;

namespace Pocket.Backend.Test
{
    public class Tests
    {
        private HttpClient _httpClient;
        private string baseUrl = string.Empty;
        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            baseUrl = "https://localhost:7064/api/";
        }

        [Test]
        public void ShouldAutheticateUser()
        {
            StringContent content = new StringContent("{\"email\":\"rohit.singh.test@gmail.com\", \"password\": \"123456\"}");
            HttpResponseMessage result = _httpClient.PostAsync($"{baseUrl}auth/login", content).Result;
            result.EnsureSuccessStatusCode();

            var authResult = result.Content.ReadAsStringAsync().Result;
            dynamic auth = JObject.Parse(authResult);

            Assert.IsTrue(auth);
        }
    }
}