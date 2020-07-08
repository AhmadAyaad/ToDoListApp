using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Models;

namespace ToDoList.API.Dtos
{
    public class DairyForCreateDto
    {
        public int DairyId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int UserId { get; set; }
        public IFormFile File { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public DairyForCreateDto()
        {
            DateAdded = DateTime.Now;
        }
        //public string Url { get; set; }
        //public IFormFile File { get; set; }

        //public string PublicId { get; set; }
    }
}
