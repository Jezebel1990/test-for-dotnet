using NUnit.Framework;
using System.Net;

namespace TestForDotNet.Testes
{
    public class ApiTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
           
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000/"); 
        }

        [Test]
        public async Task GetResource_DeveRetornarNomeCorreto()
        {
       
            string expectedName = "Maria Silva"; 

          
            HttpResponseMessage response = await _client.GetAsync("api/resource");
            string responseBody = await response.Content.ReadAsStringAsync();

       
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(responseBody);

            // Verificar o nome retornado no corpo da resposta
            Assert.IsTrue(responseBody.Contains(expectedName), "O nome retornado não corresponde ao nome esperado.");
        }
    }
}

