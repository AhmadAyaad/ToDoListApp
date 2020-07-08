using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Models;

namespace ToDoList.API.Dtos
{
    public class UserDairiesDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<Dairy> Dairies { get; set; } = new HashSet<Dairy>();
    }
}
