using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class SqlServerHelper : Database
	{
		public SqlServerHelper() { }

		public SqlServerHelper(string connectionString)
			: base(connectionString)
		{
		}

		public override IDbConnection GetConnection()
		{
			return new SqlConnection();
		}

		//public override IDbConnection CreateConnection()
  //      {
  //          return new SqlConnection();
  //      }

  //      public override IDbCommand CreateCommand()
  //      {
  //          return new SqlCommand();
  //      }

  //      public override IDataAdapter CreateDataAdapter(IDbCommand command)
  //      {
  //          SqlCommand _command = (SqlCommand)command;
  //          return new SqlDataAdapter(_command);
  //      }

        //public void WriteToDatebae(string tableName, DataTable dataTable)
        //{
        //    var connectionString = _connectionString;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlBulkCopy bulkCopy =
        //            new SqlBulkCopy(
        //                connection,
        //                SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction,
        //                null
        //            );
        //        bulkCopy.DestinationTableName = tableName;
        //        try
        //        {
        //            if (connection.State != ConnectionState.Open)
        //                connection.Open();
        //            if (dataTable != null && dataTable.Rows.Count != 0)
        //                bulkCopy.WriteToServer(dataTable);
        //        }
        //        catch { }
        //        finally
        //        {
        //            connection.Close();
        //            if (bulkCopy != null)
        //                bulkCopy.Close();
        //        }
        //    }
        //}

    }
}
