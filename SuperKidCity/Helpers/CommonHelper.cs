using SKC.Lib.Data.Models.Entities;
using SuperKidCity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SuperKidCity.Helpers
{
    public static class CommonHelper
    {
        internal static string GetErrorMessage(Exception ex, ref string msg)
        {
            //string msg = string.Empty;
            if (ex == null)
                return msg;
            else
            {
                msg = ex.Message;
                GetErrorMessage(ex.InnerException, ref msg);
            }

            return msg;
        }

        internal static StateDTO GetStateById(int Id)
        {
            var stateDTO = new StateDTO();
            using (var context = new ApplicationDbContext())
            {
                stateDTO = context.States.Find(Id);
                context.Entry(stateDTO).State = EntityState.Detached;
            }
            return stateDTO;
        }

        internal static CityDTO GetCityById(int Id)
        {
            var cityDTO = new CityDTO();
            using (var context = new ApplicationDbContext())
            {
                cityDTO = context.Cities.Find(Id);
                context.Entry(cityDTO).State = EntityState.Detached;
            }
            return cityDTO;
        }

        internal static LocalityDTO GetLocalityById(int Id)
        {
            var localityDTO = new LocalityDTO();
            using (var context = new ApplicationDbContext())
            {
                localityDTO = context.Localities.Find(Id);
                context.Entry(localityDTO).State = EntityState.Detached;
            }
            return localityDTO;
        }

        internal static bool CityExist(string cityName)
        {
             using (var context = new ApplicationDbContext())
            {
               return context.Cities.Any(c=>c.Name == cityName);
            }
        }

        internal static bool StateExist(string stateName)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.States.Any(s => s.Name == stateName);
            }
        }
    }
}