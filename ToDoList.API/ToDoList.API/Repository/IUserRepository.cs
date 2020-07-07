using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Models;

namespace ToDoList.API.Repository
{
    public interface IUserRepository
    {

        Task<User> GetUser(int id);
    }
}
