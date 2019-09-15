using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Models.AppleDeveloper
{
	public class Profile
	{
		public string id { get; set; }
		public string type { get; set; }
		public ResourceLinks links { get; set; }
		public ProfileAttributes attributes { get; set; }
	}

	public class ProfileAttributes
	{
		public string name { get; set; }
		public string platform { get; set; }
		public string profileContent { get; set; }
		public string uuid { get; set; }
		public DateTime createdDate { get; set; }
		public string profileState { get; set; }
		public string profileType { get; set; }
		public DateTime expirationDate { get; set; }
	}
}
