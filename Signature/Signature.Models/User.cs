using Common.Encrypt;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signature.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public bool? EmailVerify { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneVerify { get; set; }
        private string _password { get; set; }
        public string Password
        {
            get { return _password; }
            set { _password = EncryptHelper.EncryptMD5(_password); }
        }
        public decimal Amount { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
