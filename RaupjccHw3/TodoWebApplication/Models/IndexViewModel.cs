using System.Collections;
using System.Collections.Generic;
using Todo.Models;

namespace TodoWebApplication.Models
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