using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class MySqlHelper : Database
	{

		public MySqlHelper() { }

		public MySqlHelper(string connectionString)
			: base(connectionString)
		{
		}

		public override IDbConnection GetConnection()
		{
			return new MySqlConnection();
		}

		//public override IDbConnection CreateConnection()
		//{
		//	return new MySqlConnection();
		//}

		//public override IDbCommand CreateCommand()
		//{
		//	return new MySqlCommand();
		//}

		//public override IDataAdapter CreateDataAdapter(IDbCommand command)
		//{
		//	MySqlCommand _command = (MySqlCommand)command;
		//	return new MySqlDataAdapter(_command);
		//}
	}
}
