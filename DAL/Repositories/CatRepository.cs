using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using Models.Models;

namespace DAL.Repositories
{

	public class CatRepository<T> : ODBCRep<Cat> where T : class
	{

		public Contexts.CatsContext _context = null;
		//public DbSet<T> table = null;
		SqlDataReader dr;
		List<T> table;
		T catRec;
		 

		public CatRepository()
		{

			base.conStrName = "CatsContext";

		}

		public CatRepository( Contexts.CatsContext _context)
		{
			this._context = _context;
			//table = _context.Set<T>();			
		}

		void Delete(object id)
		{
			throw new NotImplementedException();
		}


		public override Cat PopulateRecord(SqlDataReader reader)
		{
			Cat catRec = new Cat();
			
			catRec.Id = Convert.ToInt32(dr["Id"]);
			catRec.breedId = Convert.ToInt32(dr["breedId"]);
			catRec.catDetailsId = Convert.ToInt32(dr["catDetailsId"]);
			catRec.locationId = Convert.ToInt32(dr["locationId"]);
			catRec.name = dr["name"].ToString();
			catRec.age = Convert.ToInt32(dr["age"]);
			catRec.gender = dr["gender"].ToString();
			catRec.age = Convert.ToInt32(dr["age"]);
			catRec.pic = dr["pic"].ToString();
			return catRec;
		}


		public IEnumerable<T> GetAll()
		{
	
			return table.ToList();
		}

		public T GetById(int id)
		{
			return null;// table.Find(id);
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

		//public void Delete(object id)
		//{
		//	T existing = table.Find(id);
		//	table.Remove(existing);
		//}

		//public void Save()
		//{
		//	_context.SaveChanges();
		//}
	}

}
