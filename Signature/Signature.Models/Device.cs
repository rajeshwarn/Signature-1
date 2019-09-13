using Dapper.Contrib.Extensions;
using System;

namespace Signature.Model
{
    [Table("Device")]
    public class Device
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        /// <summary>
        /// 设备 ID
        /// </summary>
        public string UDID { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
