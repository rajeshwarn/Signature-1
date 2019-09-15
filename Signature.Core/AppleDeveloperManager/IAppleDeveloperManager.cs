using System;
using System.Collections.Generic;
using System.Text;

namespace Signature.Core.AppleDeveloperManager
{
	public interface IAppleDeveloperManager
	{
		string ApiUrl { get; set; }
		string Key { get; set; }
		string IssueID { get; set; }
		int ExpirationTime { get; set; }


	}
}
