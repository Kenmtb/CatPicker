using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using System.Data;

namespace DAL.Repositories
{
	public class StateRepository<T> : ODBCRep<State> where T : class
	{

		public StateRepository()
		{
			base.conStrName = "CatsContext";
			base.createSQLstrings("dbo.states");
		}

		public void Delete(int id)
		{
			DeleteRecord(id);
		}

		public IEnumerable<State> GetAll()
		{
			return GetRecords();
		}

		public State GetById(int id)
		{
			// id = -1 means a new record is requested for the editor, otherwise return a record
			if (id == -1)
				return new State();
			else
				return GetRecordByID(id);
		}

		public void Insert(State obj)
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

		public void Save(State obj)
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

		//*************************************** CRUD Helpers
		//Put datarow data into a record object
		public override State PopulateRecord(DataRow dr)
		{
			State Rec = new State();
			Rec.Id = Convert.ToInt32(dr["Id"]);
			Rec.name = dr["name"].ToString();			
			return Rec;
		}

		//Put data object data into a data row
		public override DataRow PopulateDataRow(State datarec, DataRow dr)
		{
			DataSet ds = new DataSet();
			dr["name"] = datarec.name;			
			return dr;
		}

		public int getLastRecordID()
		{
			return getLastRecordIDBase();
		}

	}
}
