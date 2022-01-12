using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Repositories;

namespace BLL
{
	public class StateBLL
	{
		DAL.Repositories.StateRepository<State> rep;
		

	public StateBLL()
		{
			rep = new DAL.Repositories.StateRepository<State>();
		}

		public List<State> getAllStates()
		{
			return rep.GetAll().ToList();
		}
	}
}
