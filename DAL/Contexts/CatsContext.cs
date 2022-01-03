using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Contexts
{
	public class CatsContext:DbContext
	{
		public CatsContext()
		{
			Database.SetInitializer<CatPersonalityContext>(null);
		}

		public DbSet<Cat> cats { get; set; }
	}
}
