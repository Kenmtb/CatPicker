using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using System.Data;

namespace DAL.Repositories
{
	public class StateRepository<T> : ODBCRep<State> where T : class, IRepository<T>
	{

		public StateRepository()
		{
			base.conStrName = "StatesContext";
		}

		public void Delete(int id)
		{
			DeleteRecord(id);
		}

		public IEnumerable<State> GetAll()
		{
			return GetRecords();
		}

		public State GetById(object id)
		{
			return (GetRecords("SELECT * FROM dbo.States WHERE Id = " + id)).FirstOrDefault();
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
	}
}
