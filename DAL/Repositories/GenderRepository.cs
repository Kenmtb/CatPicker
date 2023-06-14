using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using System.Data;

namespace DAL.Repositories
{
	public class GenderRepository
	{

		public IEnumerable<Gender> GetAll()
		{
			return GetRecords();
		}


		public IEnumerable<Gender> GetRecords()
		{
			return  new List<Gender>() { new Gender() {genderName="Male", genderID=0}, new Gender() { genderName = "Female", genderID = 1 }};						
		}

	}
}
