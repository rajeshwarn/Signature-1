using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class OleDbHelper : Database
    {
		public OleDbHelper() { }

		public OleDbHelper(string connectionString)
			: base(connectionString)
		{
		}

		public override IDbConnection GetConnection()
		{
			return new OleDbConnection();
		}

		//public override IDbConnection CreateConnection()
  //      {
  //          return new OleDbConnection();
  //      }


  //      public override IDbCommand CreateCommand()
  //      {
  //          return new OleDbCommand();
  //      }

  //      public override IDataAdapter CreateDataAdapter(IDbCommand command)
  //      {
  //          OleDbCommand _command = (OleDbCommand)command;
  //          return new OleDbDataAdapter(_command);
  //      }

    }
}
