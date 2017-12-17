using System;

namespace Todo.Models
{
    public class TodoViewModel
    {
        public string Title { get; set; }
        public DateTime? DateDue { get; set; }
        public Guid Id { get; set; }
        public string LinkText { get; set; }
        public bool ShowOffset { get; set; }
    }
}