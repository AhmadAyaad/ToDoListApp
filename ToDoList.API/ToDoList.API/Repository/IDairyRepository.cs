using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Dtos;
using ToDoList.API.Models;

namespace ToDoList.API.Repository
{
    public interface IDairyRepository
    {
        Task<List<Dairy>> GetAll(int userId);
        void CreateDairy(Dairy dairy,Photo photo , User user);
        Task<Dairy> GetDairy(int dairyId, int userId);

    }
}
