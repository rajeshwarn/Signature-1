using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signature.Models.Database
{
    [Table("Application")]
    public class Application
    {
        [Key]
        public string Id { get; set; }

        public int UserId { get; set; }
        public string SignatureType { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string IconUrl { get; set; }
        public List<string> ImageUrl { get; set; }
        public string IPAPath { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
