using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using System.Data;

namespace DAL.Repositories
{
	public class CityRepository<T> : ODBCRep<City> where T : class
	{
		public CityRepository()
		{
			base.conStrName = "CatsContext";
			base.createSQLstrings("dbo.cities");
		}

		//********************************** CRUD Interface methods

		public IEnumerable<City> GetAll()
		{
			return GetRecords();
		}

		public City GetById(int id)
		{
			// id = -1 means a new record is requested for the editor, otherwise return a record
			if (id == -1)
				return new City();
			else
				return GetRecordByID(id);
		}

		public void Insert(City obj)
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

		public void Save(City obj)
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
		public override City PopulateRecord(DataRow dr)
		{
			City Rec = new City();
			Rec.Id = Convert.ToInt32(dr["Id"]);
			Rec.cityName = dr["cityName"].ToString();
			Rec.stateID = Convert.ToInt32(dr["stateID"]);

			return Rec;
		}

		//Put data object data into a data row
		public override DataRow PopulateDataRow(City datarec, DataRow dr)
		{
			DataSet ds = new DataSet();
			dr["cityName"] = datarec.cityName;
			dr["Id"] = datarec.Id;
			dr["stateID"] = datarec.stateID;
			return dr;
		}

		public int getLastRecordID()
		{
			return getLastRecordIDBase();
		}
	}
}
