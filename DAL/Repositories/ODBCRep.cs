using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL.Repositories
{
	//https://www.danylkoweb.com/Blog/creating-a-repository-pattern-without-an-orm-A9
	public abstract class ODBCRep<T> where T : class
	{
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

		public SqlCommand getCommand (SqlConnection conn, string sqlStr, List<SqlParameter> paramList = null)		
		{
			SqlCommand com = new SqlCommand();
			com.Connection = conn;
			com.CommandText = sqlStr;
			if(paramList != null)
			{
				foreach (var param in paramList)
				{
					com.Parameters.Add(param);
				}				
			}
			return com;
		}

		public SqlDataReader getDataReader (string connStrName, string sqlStr, List<SqlParameter> paramList=null)
		{			
			SqlDataReader dataReader;
			string connStr = GetConnectionStringByName(connStrName);
			SqlConnection con = getConnection(connStr);
			SqlCommand cmd = getCommand(con, sqlStr, paramList);
			dataReader = cmd.ExecuteReader();
			return dataReader;
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


		public virtual T PopulateRecord(SqlDataReader reader)
		{
			return null;
		}

		protected IEnumerable<T> GetRecords(SqlCommand command)
		{
			var list = new List<T>();
			SqlDataReader reader=null;

			try
			{
				reader = getDataReader(conStrName, "");
				try
				{

						list.Add(PopulateRecord(reader));
				}
				finally
				{
					// Always call Close when done reading.
					reader.Close();
				}
			}
			finally
			{
				reader.Close();				
			}
			return list;
		}
	}
}
