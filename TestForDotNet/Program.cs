namespace TestForDotNet
{

    public class Program
    {
        static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                           .UseKestrel()
                           .ConfigureServices(services =>
                           {
                               services.AddMvc();
                               services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "The API", Version = "v1" });
    });
                           })
     .Configure(app =>
     {
         app.UseMvc();
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

    public class TodoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Completed { get; set; }
    }

    public static class TodoRepository
    {
        private static List<TodoItem> _todos = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Task 1", Completed = false },
            new TodoItem { Id = 2, Title = "Task 2", Completed = true },
            new TodoItem { Id = 3, Title = "Task 3", Completed = false }
        };

        public static List<TodoItem> GetAll()
        {
            return _todos;
        }

        public static TodoItem Get(int id)
        {
            return _todos.Find(todo => todo.Id == id);
        }

        public static void Add(TodoItem todo)
        {
            todo.Id = _todos.Count + 1;
            _todos.Add(todo);
        }

        public static void Update(int id, TodoItem todo)
        {
            int index = _todos.FindIndex(t => t.Id == id);
            if (index != -1)
            {
                _todos[index] = todo;
            }
        }

        public static void Delete(int id)
        {
            _todos.RemoveAll(todo => todo.Id == id);
        }
    }

    public class TodoController
    {
        private readonly HttpClient _httpClient;

        public TodoController()
        {
            _httpClient = new HttpClient();
        }

        public List<TodoItem> GetAll()
        {
            return TodoRepository.GetAll();
        }

        public TodoItem Get(int id)
        {
            return TodoRepository.Get(id);
        }

        public void Add(TodoItem todo)
        {
            NewMethod(todo);
        }

        private static void NewMethod(TodoItem todo)
        {
            TodoRepository.Add(todo);
        }
    }
}
