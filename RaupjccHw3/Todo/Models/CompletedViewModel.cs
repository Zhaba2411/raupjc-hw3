using System.Collections.Generic;

namespace Todo.Models
{
    public class CompletedViewModel
    {
        public IList<TodoViewModel> TodoList { get; }

        public CompletedViewModel(IList<TodoViewModel> models)
        {
            TodoList = models;
        }
    }
}