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
	public class CatDetailRepository<T> : ODBCRep<CatDetail> where T : class
	{
		public CatDetailRepository()
		{
			base.conStrName = "CatsContext";
			base.createSQLstrings("dbo.catDetails");
		}

		//********************************** CRUD Interface methods

		public IEnumerable<CatDetail> GetAll(string sqlStr = null, List<SqlParameter> paramList = null)
		{
			return GetRecords(sqlStr, paramList);
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
			//Rec.catId = Convert.ToInt32(dr["CatId"]);

			Rec.personalityId = (object)dr["personalityId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["personalityId"]);
			Rec.locationId = (object)dr["locationId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["locationId"]);
			Rec.stateId = (object)dr["stateId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["stateId"]);
			Rec.cityId = (object)dr["cityId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["cityId"]);
			Rec.detailPicsId = (object)dr["detailPicsId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["detailPicsId"]);

			Rec.weight = (object)dr["weight"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["weight"]);
			Rec.description = (object)dr["description"] == DBNull.Value ? null : dr["description"].ToString();

			//Rec.personalityId = Convert.ToInt32(dr["personalityId"]);
			//Rec.locationId = Convert.ToInt32(dr["locationId"]);
			//Rec.detailPicsId = Convert.ToInt32(dr["detailPicsId"]);

			//Rec.weight = Convert.ToDecimal( dr["weight"]);
			//Rec.description = dr["description"].ToString();

			return Rec;
		}

		//Put data object data into a data row
		public override DataRow PopulateDataRow(CatDetail datarec, DataRow dr)
		{
			DataSet ds = new DataSet();
			dr["Id"] = datarec.Id;			
			//dr["CatId"] = datarec.catId;
			dr["personalityId"] = datarec.personalityId;
			dr["locationId"] = datarec.locationId;
			dr["stateId"] = datarec.stateId;
			dr["cityId"] = datarec.cityId;
			dr["catDetailPicsId"] = datarec.detailPicsId;
			dr["weight"] = datarec.weight;
			dr["description"] = datarec.description;
			return dr;
		}

		public int getLastRecordID()
		{
			return getLastRecordIDBase();
		}
	}
}
