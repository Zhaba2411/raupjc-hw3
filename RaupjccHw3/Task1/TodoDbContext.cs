﻿using System.Data.Entity;

namespace Task1
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(string cnnstr) : base(cnnstr)
        {
        }

        public IDbSet<TodoItem> TodoItems { get; set; }
        public IDbSet<TodoItemLabel> TodoItemLabels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItem>().Property(t => t.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.DateCompleted);
            modelBuilder.Entity<TodoItem>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.DateDue);
            modelBuilder.Entity<TodoItem>().HasMany(t => t.Labels).WithMany(m => m.LabelTodoItems);

            modelBuilder.Entity<TodoItemLabel>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItemLabel>().Property(t => t.Value).IsRequired();
        }


    }
}