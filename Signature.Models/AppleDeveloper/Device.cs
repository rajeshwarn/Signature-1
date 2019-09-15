using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Models.AppleDeveloper
{
	public class Device
	{
		public string id { get; set; }
		public string type { get; set; }
		public ResourceLinks links { get; set; }
		public DeviceAttributes attributes { get; set; }
	}

	public class DeviceAttributes
	{
		public string deviceClass { get; set; }
		public string model { get; set; }
		public string name { get; set; }
		public string platform { get; set; }
		public string status { get; set; }
		public string udid { get; set; }
		public DateTime addedDate { get; set; }
	}

	public class DeviceCreate
	{
		public string type { get; set; } = "devices";
		public DeviceCreateAttributes attributes { get; set; }

	}

	public class DeviceCreateAttributes
	{
		public string name { get; set; }
		public string platform { get; set; }
		public string udid { get; set; }
	}
}
