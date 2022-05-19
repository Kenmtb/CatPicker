using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EFRep
{

	public class CatDetailsRepository<T> : ICatRepository<T> where T : class
	{

		public Contexts.CatDetailContext _context = null;
		public DbSet<T> table = null;

		public CatDetailsRepository()
		{
			_context = new Contexts.CatDetailContext();
			table = _context.Set<T>();
			var catDetails = _context.CatDetails.ToList();
		}

		public CatDetailsRepository( Contexts.CatDetailContext _context)
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
			try
			{
				table.Add(obj);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Globals.CustomException("Save changes failed, check editor for invalid or missing entries.");
			}
		}	

		public void Save(T obj) 
		{			
				using (var context = new DAL.Contexts.CatDetailContext())
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
	}

}
