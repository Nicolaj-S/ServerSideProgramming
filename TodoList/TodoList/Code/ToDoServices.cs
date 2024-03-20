using Microsoft.EntityFrameworkCore;
using TodoList.Model;

namespace TodoList.Code
{
    public class ToDoServices(TodoContext context)
    {
        public async Task<bool> CreateToDoItem(ToDo item)
        {
            if (item != null)
            {
                context.ToDos.Add(item);
                return await Save();
            }
            return false;
        }

        public async Task<List<ToDo>> GetItems()
        {
            return await context.ToDos.ToListAsync();
        }

        public async Task<bool> DeleteItem(int itemId)
        {
            var item = await context.ToDos.FindAsync(itemId);
            if (item != null)
            {
                context.ToDos.Remove(item);
                return await Save();
            }
            return false;
        }

        public async Task<bool> DeleteAllItems(int userId)
        {
            var userItems = await context.ToDos.Where(item => item.UserId == userId).ToListAsync();
            foreach (var item in userItems)
            {
                context.ToDos.Remove(item);
            }
            return await Save();
        }


        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
