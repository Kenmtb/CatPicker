using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ComponentModel;

namespace DAL.Repositories
{
	//https://www.danylkoweb.com/Blog/creating-a-repository-pattern-without-an-orm-A9
	public abstract class ODBCRep<T> where T : class
	{
		public const string baseSQL = "SELECT * FROM dbo.cats";
		public string conStrName;
		SqlConnection conn;

		//public ODBCRep(string conStrName)
		//{
		//	this.conStrName = conStrName;
		//}

		public SqlConnection getConnection(string connStr)
		{
			conn = new SqlConnection(connStr);
			if(conn.State == System.Data.ConnectionState.Closed)
			{
				conn.Open();
			}
			return conn;
		}

		public SqlCommand getCommand (SqlConnection conn, string sqlStr = null, List<SqlParameter> paramList = null)		
		{
			SqlCommand com = new SqlCommand();
			com.Connection = conn;
			if(sqlStr == null)
			{
				com.CommandText = baseSQL;
			}
			else
			{
				com.CommandText = null;
			}
				
			if(paramList != null)
			{
				foreach (var param in paramList)
				{
					com.Parameters.Add(param);
				}				
			}
			return com;
		}

		//public SqlDataReader getDataObject (string connStrName, string sqlStr=null, List<SqlParameter> paramList=null)
		//{			
		//	SqlDataReader dataReader;
		//	string connStr = GetConnectionStringByName(connStrName);
		//	SqlConnection con = getConnection(connStr);
		//	SqlCommand cmd = getCommand(con,sqlStr);
		//	dataReader = cmd.ExecuteReader();
		//	return dataReader;
		//}

		public  DataTable getDataObject(string connStrName, string sqlStr = null, List<SqlParameter> paramList = null)
		{
			DataTable dt=new DataTable();
			SqlDataReader sdr;

			string connStr = GetConnectionStringByName(connStrName);
			SqlConnection con = getConnection(connStr);
			SqlCommand cmd = getCommand(con, sqlStr);
			sdr = cmd.ExecuteReader();

			//using ( con = getConnection(connStr))			
			//	using ( cmd = getCommand(con, sqlStr))
			//	{					
			//		sdr = cmd.ExecuteReader();
			//	}
			
			dt.Load(sdr);
			return dt;
		}

		public string GetConnectionStringByName(string name)
		{
			// Assume failure.
			string returnValue = null;

			// Look for the name in the connectionStrings section.
			ConnectionStringSettings settings =
				ConfigurationManager.ConnectionStrings[name];

			// If found, return the connection string.
			if (settings != null)
				returnValue = settings.ConnectionString;

			return returnValue;
		}


		public virtual T PopulateRecord(DataRow reader)
		{
			return null;
		}



		protected IEnumerable<T> GetRecords(SqlCommand command, string sqlStr = null)
		{
			var list = new List<T>();
			DataTable reader = null;

			try
			{
				reader = getDataObject(conStrName, sqlStr);
				try
				{
					if (reader.Rows.Count > 0)
					{
						foreach (DataRow rec in reader.Rows)
						{

							list.Add(PopulateRecord(rec));
						}
					}

				}
				finally { }//catch (Exception ex) { }
			}
			finally { }//catch (Exception ex) { }
			
			return list;
		}




		//protected IEnumerable<T> GetRecords(SqlCommand command, string sqlStr = null)
		//{
		//	var list = new List<T>();
		//	SqlDataReader reader=null;

		//	try
		//	{
		//		reader = getDataObject(conStrName, sqlStr);
		//		try
		//		{
		//			if (reader.HasRows) {
		//				while(reader.Read())
		//				{

		//					list.Add(PopulateRecord(reader));
		//				}
		//			}

		//		}
		//		finally
		//		{
		//			// Always call Close when done reading.
		//			reader.Close();
		//		}
		//	}
		//	finally
		//	{
		//		reader.Close();				
		//	}
		//	return list;
		//}
	}
}
