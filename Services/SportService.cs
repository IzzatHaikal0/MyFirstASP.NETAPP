using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
namespace MyMvcApp.Services
{
    public class SportService : ISportService
    {
        private readonly AppDbContext _context;

        public SportService(AppDbContext context)
        {
            _context = context;
        }

        public List<Sport> GetAllSports()
        {
            return _context.Sports.Include(sport => sport.Category).ToList();
        }

        public Sport GetSportById(int id)
        {
           return _context.Sports.Find(id);
        }

        public void CreateSport(Sport sport)
        {
            _context.Sports.Add(sport);
            _context.SaveChanges();
        }

        public void DeleteSport(Sport sport)
        {
            _context.Sports.Remove(sport);
            _context.SaveChanges();
        }

        public void UpdateSport(Sport sport)
        {
            _context.Sports.Update(sport);
            _context.SaveChanges();
        }

    }

}