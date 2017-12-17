using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class AddTodoViewModel
    {
        [Required]
        public string TodoText { get; set; }

        public string Labels { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateDue { get; set; }
    }
}