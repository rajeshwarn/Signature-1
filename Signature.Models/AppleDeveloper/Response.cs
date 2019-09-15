using System;
using System.Collections.Generic;
using System.Text;

namespace Signature.Models.AppleDeveloper
{
	public class Response<T>
	{
		public T data { get; set; }
		public PagedDocumentLinks links { get; set; }
		public PagingInformation meta { get; set; }
	}

	public class PagedDocumentLinks
	{
		public string first { get; set; }
		public string next { get; set; }
		public string self { get; set; }
	}

	public class PagingInformation
	{
		public Paging paging { get; set; }
	}

	public class Paging
	{
		public int total { get; set; }
		public int limit { get; set; }
	}

	public class ResourceLinks
	{
		public string self { get; set; }
	}
}
