using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{

	public class CatPersonalityRepository<T> : ICatRepository<T> where T : class
	{

		public Contexts.CatPersonalityContext _context = null;
		public DbSet<T> table = null;

		public CatPersonalityRepository()
		{
			_context = new Contexts.CatPersonalityContext();
			table = _context.Set<T>();
			//var cats = _context.catPersonalities.ToList();
		}

		public CatPersonalityRepository( Contexts.CatPersonalityContext _context)
		{
			this._context = _context;
			table = _context.Set<T>();			
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
			table.Add(obj);
		}	

		public void Save(T obj) 
		{			
				using (var context = new DAL.Contexts.CatPersonalityContext())
				{
					//Attach the new record to the context and mark it dirty (changed)
					context.Entry(obj).State = EntityState.Modified;
					//Save the changes
					context.SaveChanges();
				}
		}

		public void Delete(object id)
		{
			T existing = table.Find(id);
			table.Remove(existing);
		}
		//public void Save()
		//{
		//	_context.SaveChanges();
		//}
	}

}
