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

        public TodoItem(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            Labels = new List<TodoItemLabel>();
        }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                DateCompleted = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        public static bool operator ==(TodoItem i1, TodoItem i2)
        {
            if (ReferenceEquals(i1, i2))
            {
                return true;
            }
            if (ReferenceEquals(i1, null) || ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.Equals(i2);
        }

        public static bool operator !=(TodoItem i1, TodoItem i2)
        {
            return !(i1 == i2);
        }
        public override bool Equals(object obj)
        {
            return obj is TodoItem && ((TodoItem)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
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

            public List<TodoItem> LabelTodoItems { get; set; }

            public TodoItemLabel(string value) : this()
            {
                Id = Guid.NewGuid();
                Value = value;
            }

            public TodoItemLabel()
            {
                LabelTodoItems = new List<TodoItem>();
            }

            public override bool Equals(object obj)
            {
                return obj is TodoItemLabel && ((TodoItemLabel)obj).Id == Id;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }
    }
}
