using System;
using System.Collections.Generic;

namespace Task1
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        // Shorter syntax for single line getters in C#6
        // public bool IsCompleted => DateCompleted . HasValue ;
        public bool IsCompleted
        {
            get {
                return DateCompleted.HasValue;
            }
        }
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary >
        /// User id that owns this TodoItem
        /// </ summary >
        public Guid UserId { get; set; }

        /// <summary >
        /// /// List of labels associated with TodoItem
        /// </ summary >
        public List<TodoItemLabel> Labels { get; set; }

        /// <summary >
        /// Date due . If null , no date was set by the user
        /// </ summary >

        public DateTime? DateDue { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }
        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }

    }
        /// <summary >
        /// Label describing the TodoItem .
        /// e.g. Critical , Personal , Work ...
        /// </ summary >
        public class TodoItemLabel 
        {
            public Guid Id { get; set; }
            public string Value { get; set; }
           
            /// <summary >
            /// All TodoItems that are associated with this label
            /// </ summary >
            public List<TodoItem> LabelTodoItems { get; set; }

            public TodoItemLabel(string value)
            {
                Id = Guid.NewGuid();
                Value = value;
                LabelTodoItems = new List<TodoItem>();
            }
        }
}
