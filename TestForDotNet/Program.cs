using TestForDotNet.Models;

namespace TestForDotNet;


public class Program
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddSingleton<ITarefaRepositorio, TarefaRepositorio>();
    }



    static void Main(string[] args)

    {
        IWebHost host = new WebHostBuilder()
                       .UseKestrel()
                       .ConfigureServices(services =>
                       {
                           services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "The API", Version = "v1" });
});
                       })
 .Configure(app =>
 {
     app.UseSwagger();
     app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
     });
 })
                .Build();

        host.Run();
    }
}

