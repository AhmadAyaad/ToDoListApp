using System;
using System.Collections;
using System.Collections.Generic;

namespace ToDoList.API.Models
{
    public class Dairy
    {
        public int DairyId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

    }
}