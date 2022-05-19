using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Repositories;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
	public class CityBLL
	{

		DAL.Repositories.CityRepository<City> rep;


		public CityBLL()
		{
			rep = new DAL.Repositories.CityRepository<City>();
		}

		public List<City> test(string sqlStr = null, List<SqlParameter> paramList = null)
		{
			sqlStr = "Select * from cities Where stateID = " + sqlStr;
			return (rep.GetAll(sqlStr).ToList());
		}

		public List<City> getAllCities()
		{
			return rep.GetAll().ToList();
		}

		public City getById(int id)
		{
			return rep.GetById(id);
		}

		//Custom utilities
		public List<City> getCitiesByStateId(int id)
		{
			return rep.GetAll().Where(x => x.stateID == id).OrderBy(x=>x.cityName).ToList();
		}

	}
}
