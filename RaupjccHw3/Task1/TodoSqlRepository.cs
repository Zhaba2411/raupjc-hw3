using System;
using System.Collections.Generic;
using System.Linq;

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
            TodoItem td = _context.TodoItems.First(t => t.Id == todoId);

            if (td == null)
            {
                return null;
            }

            if (!(td.UserId == userId))
            {
                throw new TodoAccessDeniedException("User" + userId + " is not the owner of the Todo item!");
            }

            return td;
        }

        public void Add(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem td = _context.TodoItems.First(t => t.Id == todoId);

            if (td == null)
            {
                return false;
            }

            if (!(td.UserId == userId))
            {
                throw new TodoAccessDeniedException("User" + userId + " is not the owner of the Todo item!");
            }

            _context.TodoItems.Remove(td);
            _context.SaveChanges();
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
            _context.SaveChanges();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {

            TodoItem td = _context.TodoItems.First(t => t.Id == todoId);

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
            _context.SaveChanges();
            return true;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId == userId).OrderByDescending(o => o.DateCreated).ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId == userId).Where(t => t.DateCompleted == null).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId == userId).Where(t => t.DateCompleted != null).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItems.AsEnumerable().Where(t => t.UserId == userId).Where(filterFunction).ToList();
        }

        public void AddLabel(string labelText, Guid itemId)
        {
            TodoItem item = _context.TodoItems.FirstOrDefault(i => i.Id == itemId);
            TodoItemLabel label = _context.TodoItemLabels.FirstOrDefault(l => l.Value == labelText);
            if (label == null)
            {
                label = new TodoItemLabel(labelText);
                _context.TodoItemLabels.Add(label);
                label.LabelTodoItems.Add(item);
                item.Labels.Add(label);
            }
            else
            {
                item.Labels.Add(label);
                label.LabelTodoItems.Add(item);
            }
            _context.SaveChanges();
        }
    }
}


