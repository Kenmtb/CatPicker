using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;




namespace DAL.Repositories.EFRep
{

	public class CatRepository<T> : ICatRepository<T> where T : class
	{

		public Contexts.CatsContext _context = null;
		public DbSet<T> table = null;
		
		public CatRepository()
		{
			_context = new Contexts.CatsContext();

			table = _context.Set<T>();
			//DbSet<T> tab = (DbSet<T>)_context.Set<T>().Where("Age = 3");
			
			//var cats = _context.cats.ToList();
			//var tab = _context.cats.Where("Age = 3");



		}

		public CatRepository( Contexts.CatsContext _context)
		{
			this._context = _context;
			table = _context.Set<T>();			
		}

		void ICatRepository<T>.Delete(object id)
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

		//public void Update(T obj)
		//{
		//	table.Attach(obj);
		//	_context.Entry(obj).State = EntityState.Modified;
		//}


		public void Save(T obj)
		{
			try
			{
				using (var context = new DAL.Contexts.CatsContext())
				{
					//Attach the new record to the context and mark it dirty (changed)
					context.Entry(obj).State = EntityState.Modified;
					//Save the changes
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Save failed. Changes are not saved ex:");
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
