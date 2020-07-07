using System;
using System.Collections;
using System.Collections.Generic;

namespace ToDoList.API.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
        public DateTime DataAdded { get; set; } = DateTime.Now;
        public int DairyId { get; set; }
        public Dairy Dairy { get; set; }
    }
}