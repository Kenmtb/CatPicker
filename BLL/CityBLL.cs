using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Repositories;

namespace BLL
{
	public class CityBLL
	{

		DAL.Repositories.CityRepository<City> rep;


		public CityBLL()
		{
			rep = new DAL.Repositories.CityRepository<City>();
		}

		public List<City> getAllCities()
		{
			return rep.GetAll().ToList();
		}

	}
}
