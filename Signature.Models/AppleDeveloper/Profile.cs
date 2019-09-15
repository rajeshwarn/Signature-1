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

	public class ProfileCreate
	{
		public string type { get; set; } = "profiles";
		public ProfileAttributes attributes { get; set; }
	}

	public class ProfileCreateAttributes
	{
		public string name { get; set; }
		public string profileType { get; set; }
	}

	public class ProfileCreateRelationships
	{
		public BundleId bundleId { get; set; }
		public Certificates certificates { get; set; }
		public Devices devices { get; set; }

		public class BundleId
		{
			public BundleIdAttributes data { get; set; }
		}

		public class BundleIdAttributes
		{
			public string id { get; set; }
			public string type { get; set; } = "bundleIds";
		}

		public class Certificates
		{
			public List<CertificateAttributes> data { get; set; }
		}

		public class CertificateAttributes
		{
			public string id { get; set; }
			public string type { get; set; } = "certificates";
		}

		public class Devices
		{
			public List<DeviceAttributes> data { get; set; }
		}

		public class DeviceAttributes
		{
			public string id { get; set; }
			public string type { get; set; } = "devices";
		}
	}

}
