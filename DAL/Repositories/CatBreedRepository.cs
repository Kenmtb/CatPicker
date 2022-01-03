using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	class CatBreedRepository<T> : ICatRepository<T> where T : class
	{

		public Contexts.CatBreedContext _context = null;
		public DbSet<T> table = null;

		public CatBreedRepository()
		{
			_context = new Contexts.CatBreedContext();
			table = _context.Set<T>();
			var catBreeds = _context.catBreeds.ToList();
		}

		public CatBreedRepository(Contexts.CatBreedContext _context)
		{
			this._context = _context;
			table = _context.Set<T>();
		}

		public void Delete(object id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> GetAll()
		{
			return table.ToList();
		}

		public T GetById(object id)
		{
			return table.Find(id);
		}

		public void Insert(T obj)
		{
			throw new NotImplementedException();
		}

		public void Save(T obj)
		{
			using (var context = new DAL.Contexts.CatBreedContext())
			{
				//Attach the new record to the context and mark it dirty (changed)
				context.Entry(obj).State = EntityState.Modified;
				//Save the changes
				context.SaveChanges();
			}
		}
	}
}
