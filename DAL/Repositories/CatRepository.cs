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

		//public Contexts.CatsContext _context = null;
		//public DbSet<T> table = null;
		//SqlDataReader dr;
		//List<T> table;
		//T catRec;
		 

		public CatRepository()
		{
			base.conStrName = "CatsContext";
			base.createSQLstrings("dbo.cats");				
		}

	

		//********************************** CRUD Interface methods

		//public IEnumerable<Cat> GetAll()
		//{
		//	using (var command = new SqlCommand("SELECT * FROM dbo.cats"))
		//	{
		//		return GetRecords(command);
		//	}			
		//}

		public IEnumerable<Cat> GetAll()
		{		
				return GetRecords();		
		}

		//public Cat GetById(int id)
		//{
		//	using (var command = new SqlCommand("SELECT * FROM dbo.cats WHERE Id = " + id))
		//	{
		//		return (GetRecords(command)).FirstOrDefault();
		//	}
		//}

		public Cat GetById(int id)
		{		
				return (GetRecords("SELECT * FROM dbo.cats WHERE Id = " + id)).FirstOrDefault();
		}

		public void Insert(Cat obj)
		{
			try
			{
				InsertRecord(obj);
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


		public void Save(Cat obj)
		{
			try
			{							
				SaveRecord(obj, obj.Id);
			}
			catch (Exception ex)
			{
				throw new Exception("Save failed. Changes are not saved ex:");
			}
		}


		public void Delete(int id)
		{			
			DeleteRecord(id);
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

		//*************************************** CRUD Helpers
		//Put datarow data into a record object
		public override Cat PopulateRecord(DataRow dr)
		{
			Cat catRec = new Cat();

			catRec.Id = Convert.ToInt32(dr["Id"]);
			catRec.name = dr["name"].ToString();
			catRec.breedId = (object)dr["breedId"] == DBNull.Value ? null : (int?)dr["breedId"];
			catRec.locationId =(object)dr["locationId"] == DBNull.Value ? 1 : Convert.ToInt32(dr["locationId"]);// Convert.ToInt32(dr["locationId"]);
			//catRec.catDetailsId = (object)dr["catDetailsId"] == DBNull.Value ? null : (int?)dr["catDetailsId"];   //!dr.IsDBNull(3) ? (Int32?)dr.GetInt32(3) : null;
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

		//Put data object data into a data row
		public override DataRow PopulateDataRow(Cat datarec, DataRow dr)
		{
			DataSet ds = new DataSet();

			//dr.BeginEdit();

			//dr = ds.Tables["cbo.cats"].NewRow();
			//dr["Id"] = datarec.Id;
			dr["name"] = datarec.name;
			dr["breedId"] = datarec.breedId;
			//dr["locationId"] = datarec.locationId;
			//if (datarec.catDetailsId != null) dr["catDetailsId"] = datarec.catDetailsId;  //Foriegn key dependancy, will be populated after foriegn table gets it's id. // == null ? 0 : datarec.catDetailsId; ;
			dr["catPersonalityId"] = datarec.catPersonalityId;
			dr["age"] = datarec.age;
			dr["pic"] = datarec.pic;
			dr["gender"] = datarec.gender;
			dr["mainColor"] = datarec.mainColor;
			dr["secondColor"] = datarec.secondColor;
			dr["thirdColor"] = datarec.thirdColor;
			dr["weight"] = datarec.weight == null ? 0 : datarec.weight;
			dr["arrivalDate"] = ((DateTime)datarec.arrivalDate).Date.ToString("yyyy-MM-dd");
			//dr.EndEdit();
			return dr;
		}

		public int getLastCatRecordID()
		{
			return getLastCatRecordIDBase();
		}

	}

}
