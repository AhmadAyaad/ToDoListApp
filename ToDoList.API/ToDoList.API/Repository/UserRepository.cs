using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Data;
using ToDoList.API.Dtos;
using ToDoList.API.Models;

namespace ToDoList.API.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public UserDairiesDto GetUser(int id)
        {
            var user = _context.Users.Include("Dairies.Photos").FirstOrDefault(u => u.UserId == id);
            user.Dairies = user.Dairies.OrderByDescending(d =>d.Date).ThenByDescending(d=>d.Time).ToList();
            
            UserDairiesDto userDairiesDto = new UserDairiesDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Dairies = user.Dairies
            };
            return userDairiesDto;
        }
    }
}
