using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Contexts
{
	public class CatBreedContext : DbContext
	{
		public CatBreedContext()
		{
			Database.SetInitializer<CatBreedContext>(null);
		}

		public DbSet<CatBreed> catBreeds { get; set; }
	}
}
