using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using System.Data;


namespace DAL.Repositories
{
	public class CatDetailsRepository<T> : ODBCRep<CatDetail> where T : class
	{

		public CatDetailsRepository()
		{
			base.conStrName = "CatDetailContext";
			base.createSQLstrings("dbo.catDetails");
		}

		//********************************** CRUD Interface methods

		public IEnumerable<CatDetail> GetAll()
		{
			return GetRecords();
		}

		public CatDetail GetById(int id)
		{
			// id = -1 means a new record is requested for the editor, otherwise return a record
			if (id == -1)
				return new CatDetail();
			else
				return GetRecordByID(id);
		}

		public void Insert(CatDetail obj)
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

		public void Save(CatDetail obj)
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
		public override CatDetail PopulateRecord(DataRow dr)
		{
			CatDetail Rec = new CatDetail();

			Rec.Id = Convert.ToInt32(dr["Id"]);
			Rec.description = dr["description"].ToString();
			Rec.catId = (int)dr["catId"];
			
			return Rec;
		}

		//Put data object data into a data row
		public override DataRow PopulateDataRow(CatDetail datarec, DataRow dr)
		{
			DataSet ds = new DataSet();

			dr["description"] = datarec.description;
			dr["catId"] = datarec.catId;
			return dr;
		}

		public int getLastRecordID()
		{
			return getLastRecordIDBase();
		}
	}
}
