using Common.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signature.Core.SignatureManager
{
    public interface ISignatureManager
    {
        Database Db { get; set; }

        
    }
}
