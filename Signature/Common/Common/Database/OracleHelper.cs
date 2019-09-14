using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class OracleHelper : Database
    {
        public OracleHelper(string connectionString)
            : base(connectionString)
        {
        }

        public override IDbConnection CreateConnection()
        {
            return new OracleConnection();
        }

        public override IDbCommand CreateCommand()
        {
            return new OracleCommand();

        }

        public override IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            OracleCommand _command = (OracleCommand)command;
            return new OracleDataAdapter(_command);
        }
    }
}
