using Common.Database;
using Common.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signature.Core.SignatureManager.Impl
{
    public class SignatureManager
    {
        public Database Db { get; set; }

        public ReturnValue<bool> SetUDID(string udid)
        {
            Db.Connection(conn =>
            {

            });

            return new ReturnValue<bool>();
        }
    }
}
