using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Data;

namespace ToDoList.API.Repository
{
    public class PhotoRepository
    {
        readonly DataContext _context;

        public PhotoRepository(DataContext context)
        {
            _context = context;
        }


    }
}
