using MyMvcApp.Models;
using System.Collections.Generic;

namespace MyMvcApp.Services
{
    public interface ISportService
    {
        List<Sport> GetAllSports();
        Sport GetSportById(int id);
        void CreateSport(Sport sport);
        void UpdateSport(Sport sport);
        void DeleteSport(Sport sport);
    }
}