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
	public class CatDetailPicsRepository<T> : ODBCRep<CatDetailPic> where T : class
	{
		public CatDetailPicsRepository()
		{
			base.conStrName = "CatsContext";
			base.createSQLstrings("dbo.catDetailPics");
		}

		//********************************** CRUD Interface methods

		public IEnumerable<CatDetailPic> GetAll(string sqlStr = null, List<SqlParameter> paramList = null)
		{
			return GetRecords(sqlStr, paramList);
		}

		public CatDetailPic GetById(int id)
		{
			// id = -1 means a new record is requested for the editor, otherwise return a record
			if (id == -1)
				return new CatDetailPic();
			else
				return GetRecordByID(id);
		}

		public void Insert(CatDetailPic obj)
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

		public void Save(CatDetailPic obj)
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
		public override CatDetailPic PopulateRecord(DataRow dr)
		{
			CatDetailPic Rec = new CatDetailPic();
			Rec.Id = Convert.ToInt32(dr["Id"]);
			if (dr["catId"] != null) Rec.catId = Convert.ToInt32(dr["catId"]);
			if (dr["catPicDescription"] != null) Rec.catPicDescription = dr["catPicDescription"].ToString();
			if (dr["catPicUrl"] != null) Rec.catPicUrl = dr["catPicUrl"].ToString();
			return Rec;
		}

		//Put data object data into a data row
		public override DataRow PopulateDataRow(CatDetailPic datarec, DataRow dr)
		{
			DataSet ds = new DataSet();
			dr["Id"] = datarec.Id;			
			dr["catId"] = datarec.catId;
			dr["catPicDescription"] = datarec.catPicDescription;
			dr["catPicUrl"] = datarec.catPicUrl;			
			return dr;
		}

		public int getLastRecordID()
		{
			return getLastRecordIDBase();
		}
	}
}
