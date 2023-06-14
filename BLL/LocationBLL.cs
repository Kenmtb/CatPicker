using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Repositories;

namespace BLL
{
	public class LocationBLL
	{
		DAL.Repositories.LocationRepository<Location> rep;


		public LocationBLL()
		{
			rep = new DAL.Repositories.LocationRepository<Location>();
		}

		public List<Location> getAllLocations()
		{
			return rep.GetAll().ToList();
		}

		public List<Location> getLoctionById(int id)
		{
			return rep.GetAll().Where(x => x.Id == id).OrderBy(x => x.locationName).ToList();
		}
	}
}
