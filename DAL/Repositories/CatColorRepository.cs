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
	public class CatColorRepository<T> : ODBCRep<CatColor>  where T : class
	{
		public CatColorRepository()
		{
			base.conStrName = "CatColorContext";
			base.createSQLstrings("dbo.catColors");
		}

		//********************************** CRUD Interface methods

		public IEnumerable<CatColor> GetAll()
		{
			return GetRecords();
		}

		//*************************************** CRUD Helpers
		//Put datarow data into a record object
		public override CatColor PopulateRecord(DataRow dr)
		{
			CatColor Rec = new CatColor();
			Rec.catId = Convert.ToInt32(dr["Id"]);
			Rec.catColor = dr["catColor"].ToString();
			Rec.listOrder = Convert.ToInt32(dr["listOrder"]);
			return Rec;
		}
	}
}
