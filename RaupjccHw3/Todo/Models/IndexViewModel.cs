using System.Collections.Generic;

namespace Todo.Models
{
    public class IndexViewModel
    {
        public IList<TodoViewModel> TodoList { get; }

        public IndexViewModel(IList<TodoViewModel> models)
        {
            TodoList = models;
        }
    }
}