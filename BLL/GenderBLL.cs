using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace BLL
{
	public class GenderBLL
	{

		DAL.Repositories.GenderRepository rep;

		public GenderBLL()
		{
			rep = new DAL.Repositories.GenderRepository();
		}

		public List<Gender> getAllCities()
		{
			return rep.GetAll().ToList();
		}

	}
}
