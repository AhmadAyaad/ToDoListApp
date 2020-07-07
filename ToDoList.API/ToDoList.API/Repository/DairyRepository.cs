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
    public class DairyRepository : IDairyRepository
    {
        readonly DataContext _context;
        public DairyRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateDairy(Dairy dairyForCreateDto, Photo photo, User user)
        {
            Dairy dairy = new Dairy
            {
                DairyId = dairyForCreateDto.DairyId,
                Date = dairyForCreateDto.Date,
                Text = dairyForCreateDto.Text,
                Time = dairyForCreateDto.Time,
                UserId = dairyForCreateDto.UserId

            };
            User newUser = new User
            {
                UserId = user.UserId
            };
            dairy.Photos.Add(photo);
            _context.Dairies.Add(dairy);
            newUser.Dairies.Add(dairy);
            _context.SaveChanges();
        }

        public async Task<List<Dairy>> GetAll(int userId)
        {
            var dairies = await _context.Dairies.Include(p=>p.Photos).Where(d => d.UserId == userId).ToListAsync();
            if (dairies != null)
                return dairies;
            return new List<Dairy>();
        }

        public async Task<Dairy> GetDairy(int dairyId, int userId)
        {
            var dairy = await _context.Dairies.Include(p=>p.Photos).Where(d => d.UserId == userId)
                              .FirstOrDefaultAsync(d => d.DairyId == dairyId);
            if (dairy != null)
                return dairy;
            return new Dairy();
        }
    }
}
