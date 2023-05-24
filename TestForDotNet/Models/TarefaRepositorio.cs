namespace TestForDotNet.Models
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly TarefaContext _context;
        public TarefaRepositorio(TarefaContext context)
        {
            _context = context;
            Add(new TarefaItem { Name = "Item1" });
        }
        public IEnumerable<TarefaItem> GetAll() 
        { 
            return _context.TarefaItems.ToList();
        }
        public void Add(TarefaItem item)
        {
            _context.TarefaItems.Add(item);
            _context.SaveChanges();
        }
        public TarefaItem Find(int key)
        {
            return _context.TarefaItems.FirstOrDefault(t => t.Id == key);
        }
        public void Remove(int key) 
        { 
            var entity = _context.TarefaItems.First(t => t.Id == key);
            _context.TarefaItems.Remove(entity);
            _context.SaveChanges();
        }
        public void Update(TarefaItem item)
        {
            _context.TarefaItems.Update(item);
            _context.SaveChanges();
        }
    }
}
