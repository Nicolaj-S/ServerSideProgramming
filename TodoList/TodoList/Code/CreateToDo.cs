using TodoList.Model;

namespace TodoList.Code
{
    public class CreateToDo
    {
        private readonly TodoContext _context;

        public CreateToDo(TodoContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateToDoItem(ToDo item)
        {
            if (item != null)
            {
                _context.ToDos.Add(item);
                return await Save();
            }
            return false;
        }

        public async Task<bool> getItems()
        {
            return null;
        }
        public async Task<bool> deleteItems()
        {
            return null;
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
