namespace TestForDotNet.Models
{
    public interface ITarefaRepositorio
    {
        void Add(TarefaItem item);
        IEnumerable<TarefaItem> GetAll();
        TarefaItem Find(int key);
        void Remove(int key);
        void Update(TarefaItem item);

    }
}
