using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            TodoItem td = _context.TodoItems.First(t => t.Id.Equals(todoId));

            if (td == null)
            {
                return null;
            }

            if (!td.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("User" + userId + " is not the owner of the Todo item!");
            }

            return td;
        }

        public void Add(TodoItem todoItem)
        {
            if (_context.TodoItems.Contains(todoItem))
            {
                throw new DuplicateTodoItemException("duplicate id: " + todoItem.Id);
            }

            _context.TodoItems.Add(todoItem);
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem td = _context.TodoItems.First(t => t.Id.Equals(todoId));

            if (td == null)
            {
                return false;
            }

            if (!td.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("User" + userId + " is not the owner of the Todo item!");
            }

            _context.TodoItems.Remove(td);
            return true;
            
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (!todoItem.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("User" + userId + " is not the owner of the Todo item!");
            }
            var rm = Remove(todoItem.Id, userId);
            Add(todoItem);
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {

            TodoItem td = _context.TodoItems.First(t => t.Id.Equals(todoId));

            if (td == null)
            {
                return false;
            }

            if (!td.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("User" + userId + " is not the owner of the Todo item!");
            }

            td.DateCompleted = DateTime.Now;
            Update(td, userId);
            return true;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId.Equals(userId)).OrderByDescending(o => o.DateCreated).ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId.Equals(userId)).Where(t => t.IsCompleted == false).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId.Equals(userId)).Where(t => t.IsCompleted == true).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItems.AsEnumerable().Where(t => t.UserId.Equals(userId)).Where(filterFunction).ToList();
        }
    }

}
