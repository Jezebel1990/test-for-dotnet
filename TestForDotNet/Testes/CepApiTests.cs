using NUnit.Framework;
using System.Net;


namespace TestForDotNet.Testes
{
    [TestFixture]
    public class CepApiTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }
        [Test]
        public async Task ConsultaCep_DeveRetornarCepValido()
        {
            string cep = "12345678";

            HttpResponseMessage response = await _client.GetAsync($"api/cep/{cep}");
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(responseBody);
        }
        [Test]
        public async Task ConsultaCep_DeveRetornarErroParaCepInvalido()
        {
            string cep = "111111111";

            HttpResponseMessage response = await _client.GetAsync($"api/cep/{cep}");
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsNotNull(responseBody);
        }
    }
}
