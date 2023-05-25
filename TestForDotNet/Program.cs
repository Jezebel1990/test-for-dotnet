using Microsoft.AspNetCore;
using System.Text.Json;

namespace TestForDotNet;

public class Program
{

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseUrls("http://localhost:5000") 
        .UseStartup<Startup>();


    private static Dictionary<string, string> cepDatabase = new Dictionary<string, string>();
    private static HttpClient httpClient = new HttpClient();

    static async Task Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        string cep = "09390600"; // Cep a ser consultado

        // Verifica se o CEP está armazenado na base de dados em memória
        if (cepDatabase.ContainsKey(cep))
        {
            Console.Write("Dados do CEP (recuperados da base de dados em memória):");
            Console.WriteLine(cepDatabase[cep]);
        }
        else
        {
            // Primeira requisição e armazena os dados
            string responseData = await GetCepData(cep);
            cepDatabase[cep] = responseData;

            Console.WriteLine("Dados do CEP (consultados pela primeira vez):");
            Console.WriteLine(responseData);
        }
    }
    static async Task<string> GetCepData(string cep)
    {
        string apiUrl = $"https://viacep.com.br/ws/{cep}/json/";
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode(); //Verifica a resposta

        string jsonResponse = await response.Content.ReadAsStringAsync();
        //Extrai dados relevantes
        var cepData = JsonSerializer.Deserialize<CepResponse>(jsonResponse);

        // Armazena informações desejadas 
        string dataToStore = $"CEP: {cepData?.Cep}, Logradouro: {cepData?.Logradouro}, Cidade: {cepData?.Localidade}";
        return dataToStore;
    }
}
class CepResponse
{
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Localidade { get; set; }
}


