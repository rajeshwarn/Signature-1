using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Url
{
	public static class UrlHelper
	{
		/*
		 *	Url:			https://www.books.com.tw/activity/fashion/2019/01/b_cleanvip/?loc=member_003&utm_source=boxmagic&utm_medium=ap-books&utm_content=recommend&utm_campaign=ap-201901
		 *  Segments:		System.String[]
		 *  Query:			?loc=member_003&utm_source=boxmagic&utm_medium=ap-books&utm_content=recommend&utm_campaign=ap-201901
		 *  Port:			443
		 *  Scheme:			https
		 *  PathAndQuery:	/activity/fashion/2019/01/b_cleanvip/?loc=member_003&utm_source=boxmagic&utm_medium=ap-books&utm_content=recommend&utm_campaign=ap-201901
		 *  OriginalString: https://www.books.com.tw/activity/fashion/2019/01/b_cleanvip/?loc=member_003&utm_source=boxmagic&utm_medium=ap-books&utm_content=recommend&utm_campaign=ap-201901
		 *  Fragment:
		 *  Host:			www.books.com.tw
		 *  AbsoluteUri:	https://www.books.com.tw/activity/fashion/2019/01/b_cleanvip/?loc=member_003&utm_source=boxmagic&utm_medium=ap-books&utm_content=recommend&utm_campaign=ap-201901
		 *  AbsolutePath:	/activity/fashion/2019/01/b_cleanvip/
		 *  Authority:		www.books.com.tw
		 *  LocalPath:		/activity/fashion/2019/01/b_cleanvip/
			 */

		public static IDictionary<string, string> GetQueryDictionary(string url)
		{
			return GetQueryDictionary(new Uri(url));
		}

		public static IDictionary<string, string> GetQueryDictionary(Uri uri)
		{
			var nvc = HttpUtility.ParseQueryString(uri.Query);
			var queryDictionary = nvc.AllKeys.Where(x => x != null).ToDictionary(x => x, x => nvc[x]);
			return queryDictionary;
		}

		public static string GetNewUrl(string url, IDictionary<string, string> querys)
		{
			url = HttpUtility.HtmlDecode(url);
			url = HttpUtility.UrlDecode(url);
			return GetNewUrl(new Uri(url), querys);
		}

		public static string GetNewUrl(Uri uri, IDictionary<string, string> querys)
		{
			var _querys = GetQueryDictionary(uri);
			foreach (var q in querys)
				_querys[q.Key] = querys[q.Key];
			string queryString = string.Join("&", _querys.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => $"{x.Key}={x.Value}").ToArray());
			return $"{uri.Scheme}://{uri.Host}{uri.AbsolutePath}?{queryString}";
		}

	}
}
