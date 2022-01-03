using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Contexts
{
	public class CatLocationContext:DbContext
	{
		public DbSet<CatLocation> CatLocations { get; set; }
	}
}
