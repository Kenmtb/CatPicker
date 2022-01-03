using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Contexts
{
	             
	public class CatPersonalityContext : DbContext
	{
		public CatPersonalityContext()
		{
			Database.SetInitializer<CatPersonalityContext>(null);
		}

		public DbSet<CatPersonality> catPersonalities { get; set; }		
	}
}
