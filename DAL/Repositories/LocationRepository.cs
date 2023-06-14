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
	public class LocationRepository<T> : ODBCRep<Location> where T : class
	{
		public LocationRepository()
		{
			base.conStrName = "CatDetailContext";
			base.createSQLstrings("dbo.location");
		}

		//********************************** CRUD Interface methods

		public IEnumerable<Location> GetAll()
		{
			return GetRecords();
		}

		public Location GetById(int id)
		{
			// id = -1 means a new record is requested for the editor, otherwise return a record
			if (id == -1)
				return new Location();
			else
				return GetRecordByID(id);
		}

		public IEnumerable<Location> GetAll(string sqlStr = null, List<SqlParameter> paramList = null)
		{
			return GetRecords(sqlStr, paramList);
		}

		public void Insert(Location obj)
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

		public void Save(Location obj)
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
		public override Location PopulateRecord(DataRow dr)
		{
			Location Rec = new Location();

			Rec.Id = Convert.ToInt32(dr["Id"]);
			Rec.locationName = dr["locationName"].ToString();
			Rec.cityId = (int)dr["cityId"];

			return Rec;
		}

		//Put data object data into a data row
		public override DataRow PopulateDataRow(Location datarec, DataRow dr)
		{
			DataSet ds = new DataSet();

			dr["locationName"] = datarec.locationName;
			dr["cityId"] = datarec.cityId;
			return dr;
		}

		public int getLastRecordID()
		{
			return getLastRecordIDBase();
		}

	}
}
