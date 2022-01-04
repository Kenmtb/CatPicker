using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using Models.Models;
using System.Data;

namespace DAL.Repositories
{

	public class CatRepository<T> : ODBCRep<Cat> where T : class
	{

		public Contexts.CatsContext _context = null;
		//public DbSet<T> table = null;
		//SqlDataReader dr;
		List<T> table;
		T catRec;
		 

		public CatRepository()
		{

			base.conStrName = "CatsContext";

		}

		public CatRepository( string sqlStr)
		{
			
			//this._context = _context;
			//table = _context.Set<T>();			
		}

		void Delete(object id)
		{
			throw new NotImplementedException();
		}


		public override Cat PopulateRecord(DataRow dr)
		{
			Cat catRec = new Cat();
			
			catRec.Id = Convert.ToInt32(dr["Id"]);
			catRec.name = dr["name"].ToString();
			catRec.breedId = (object)dr["breedId"] == DBNull.Value ? null : (int?)dr["breedId"];
			catRec.locationId = (object)dr["locationId"] == DBNull.Value ? null : (int?)dr["locationId"];
			catRec.catDetailsId = (object)dr["catDetailsId"] == DBNull.Value ? null : (int?)dr["catDetailsId"];   //!dr.IsDBNull(3) ? (Int32?)dr.GetInt32(3) : null;
			catRec.catPersonalityId = (object)dr["catPersonalityId"] == DBNull.Value ? null : (int?)dr["catPersonalityId"];
			catRec.age = (object)dr["age"] == DBNull.Value ? null : (int?)dr["age"];
			catRec.pic = dr["pic"].ToString();
			catRec.gender = dr["gender"].ToString();
			catRec.mainColor = dr["mainColor"].ToString();
			catRec.secondColor = dr["secondColor"].ToString();
			catRec.thirdColor = dr["thirdColor"].ToString();
			catRec.weight = (object)dr["weight"] == DBNull.Value ? null : (double?)dr["weight"];
			catRec.arrivalDate = (object)dr["arrivalDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["arrivalDate"].ToString());
			return catRec;
		}


		public IEnumerable<Cat> GetAll()
		{
			using (var command = new SqlCommand("SELECT * FROM dbo.cats"))
			{

				return GetRecords(command);
			}
			//return table.ToList();
		}

		public Cat GetById(int id)
		{
			using (var command = new SqlCommand("SELECT * FROM dbo.cats WHERE Id = " + id))
			{
				return (GetRecords(command)).FirstOrDefault();
			}
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
