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
	//********************* This is a base class repository for ADO Connection classes and CRUD methods helper classes
	//These methods are the abstract class for CRUD's worker methods that perform the data operations

	public abstract class ODBCRep<T> where T : class
	{
		private string baseSQLString;
		private string baseSQLDeleteString;
		private string baseSQLIdString;
		private string baseSQLGetLastRecordString;
		private string baseSQLGetLastRecordIDString;

		public string conStrName { get; set; }
		
		public ODBCRep()
		{
			SqlConnection con = new SqlConnection();
			SqlConnection.ClearAllPools();
			con.Close();
		}


		//********************************* ADO connection abstract methods

		public void createSQLstrings(string tableName)
		{
			baseSQLString = "SELECT * FROM " + tableName;
			baseSQLDeleteString = "DELETE FROM " + tableName;
			baseSQLIdString = "SELECT * FROM " + tableName + " where Id = ";
			baseSQLGetLastRecordIDString = " SELECT max(id) from " + tableName;

			//SQL for child tables
			baseSQLGetLastRecordString = " SELECT max(id) from " + tableName; //adding child table data
			
	}


		public string getConnectionString ()
		{
			return GetConnectionStringByName(conStrName);
		}

		public SqlConnection getConnection()
		{
			SqlConnection conn = new SqlConnection(getConnectionString());
			if (conn.State == System.Data.ConnectionState.Closed)
			{
				conn.Open();
			}
			return conn;
		}

		public SqlCommand getCommand (string sqlStr = null, List<SqlParameter> paramList = null)		
		{
			////test setup of filter
			//sqlStr = "Select * from dbo.cats where gender = @gender";
			//paramList = new List<SqlParameter>();
			//SqlParameter sp = new SqlParameter();
			//sp.ParameterName = "@gender";
			//sp.Value = "Male";
			//paramList.Add(sp);

			SqlCommand com = new SqlCommand();
			com.Connection = getConnection();
			if(sqlStr == null)
			{
				com.CommandText = baseSQLString;
			}
			else
			{
				com.CommandText = sqlStr;
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

		public SqlCommand getCommandstring(string sqlStr = null, List<SqlParameter> paramList = null)
		{
			
			SqlConnection con = getConnection();
			SqlCommand cmd = getCommand(sqlStr, paramList);
			return cmd;
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

		//*************************** Crud Helper methods *****************************************************

		public DataTable getDataObject(string sqlStr = null, List<SqlParameter> paramList = null)
		{
			DataTable dt = new DataTable();
			SqlCommand cmd;
			SqlDataReader sdr;

			using (cmd = getCommand(sqlStr, paramList))
			{
				sdr = cmd.ExecuteReader();
				dt.Load(sdr);
				sdr.Close();
			}
			//cmd.Dispose();

			return dt;
		}

		public virtual T PopulateRecord(DataRow reader)
		{

			return null;
		}

		//protected virtual T populateDropDownRecords(T datarec)
		//{
		//	return null;
		//}

		protected T GetRecordByID(int id)
		{
			return (T)GetRecords(baseSQLIdString + id).FirstOrDefault();			
		}

		//Can return all records or records based on a query string plus optional where clause with paramerter list
		protected IEnumerable<T> GetRecords(string sqlStr=null, List<SqlParameter> paramList = null)
		{
			var list = new List<T>();
			DataTable reader = null;

			//try
			//{
				reader = getDataObject(sqlStr,paramList);
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
				catch (Exception )
				{
					throw new Globals.CustomException("Record fetch failed. (check repository model mapping for missing or misspelled fields or null data)");
				}
			//}
			//catch (Exception ) 
			//{
			//	throw new Globals.CustomException("Failed to build the required data object.");
			//}
			
			return list;
		}

		public virtual DataRow PopulateDataRow(T datarec, DataRow dr)
		{
			return null;
		}

		protected void SaveRecord (T rec, int id)
		{

			//Venkat - Part 13 What is SqlCommandBuilder

			try
			{
			SqlCommand cmd;

				cmd = getCommand(baseSQLIdString + id);
				
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
			
				da.Fill(dt);
			//DataRow dr = dt.NewRow();

			//dt.Rows.Add(PopulateDataRow(rec,dr));
			DataRow dr = dt.Rows[0];
			dr = PopulateDataRow(rec, dr);

			SqlCommandBuilder cb = new SqlCommandBuilder(da);

			da.Update(dt);
			}
			catch (Exception)
			{
				throw new Globals.CustomException("Save changes failed, check editor for invalid or missing entries.");
			}
		}

		protected int getLastRecordIDBase()
		{
			//Get the last record ID. Not the best way but needed because we do not use a SQL insert statement.
			SqlCommand cmd = getCommand(baseSQLGetLastRecordIDString);
			int id = Convert.ToInt32(cmd.ExecuteScalar());
			return id;
		}

		protected void InsertRecord(T rec)
		{
			//Venkat - Part 13 What is SqlCommandBuilder
			try
			{
				SqlCommand cmd;
				cmd = getCommand(baseSQLString);				
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();

				da.Fill(dt);
				DataRow dr = dt.Rows[0];
				dr = PopulateDataRow(rec, dr);
				SqlCommandBuilder cb = new SqlCommandBuilder(da);

				//Add new record
				da.UpdateCommand = cb.GetInsertCommand();
				da.Update(dt);
			}
			catch (Exception ex)
			{
				throw new Globals.CustomException("Save changes failed, check editor for invalid or missing entries.");
			}
		}


		protected void DeleteRecord(int id)
		{
			try
			{
				SqlCommand cmd;
				cmd = getCommand(baseSQLString);
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);

				//GetDeleteCommand must come before delete process!
				SqlCommandBuilder cb = new SqlCommandBuilder(da);
				da.DeleteCommand = cb.GetDeleteCommand();

				DataRow dr = dt.Rows[0];
				dr = dt.AsEnumerable().FirstOrDefault(x => x.Field<int>("Id") == id);
				dr.Delete();
				da.Update(dt);				
			}
			catch (Exception ex)
			{
				//throw new Globals.CustomException("Save changes failed, check editor for invalid or missing entries.");
			}
		}


		//************************ Entity framework attempt - could not get filting to work with a text based where clause
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
