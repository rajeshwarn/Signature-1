using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signature.Models.Database
{
    [Table("AmountRecord")]
    public class AmountRecord
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 金额类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        [Computed]
        public string Description { get; set; }


        public DateTime CreateTime { get; set; }
    }
}
