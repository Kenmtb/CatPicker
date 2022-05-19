using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using System.Data;
using System.Data.SqlClient;


namespace DAL.Repositories
{
	class CatBreedRepository<T> : ODBCRep<CatBreed> where T : class
	{

		public CatBreedRepository()
		{
			base.conStrName = "CatsContext";
			base.createSQLstrings("dbo.breeds");
		}

		//********************************** CRUD Interface methods

		public IEnumerable<CatBreed> GetAll(string sqlStr = null, List<SqlParameter> paramList = null)
		{
			return GetRecords(sqlStr, paramList);
		}

		public CatBreed GetById(int id)
		{
			// id = -1 means a new record is requested for the editor, otherwise return a record
			if (id == -1)
				return new CatBreed();
			else
				return GetRecordByID(id);
		}

		public void Insert(CatBreed obj)
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

		public void Save(CatBreed obj)
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

		//*************************************** CRUD Helpers
		//Put datarow data into a record object
		public override CatBreed PopulateRecord(DataRow dr)
		{
			CatBreed Rec = new CatBreed();
			Rec.Id = Convert.ToInt32(dr["Id"]);
			Rec.breedName = dr["breedName"].ToString();
			Rec.breedDescription = dr["breedDescription"].ToString();
			Rec.breedPic = dr["breedPic"].ToString();
			return Rec;
		}

		//Put data object data into a data row
		public override DataRow PopulateDataRow(CatBreed datarec, DataRow dr)
		{
			DataSet ds = new DataSet();
			dr["Id"] = datarec.Id;
			dr["breedName"] = datarec.breedName;			
			dr["breedDescription"] = datarec.breedDescription;
			dr["breedPic"] = datarec.breedPic;
			return dr;
		}

		public int getLastRecordID()
		{
			return getLastRecordIDBase();
		}
	}
}
