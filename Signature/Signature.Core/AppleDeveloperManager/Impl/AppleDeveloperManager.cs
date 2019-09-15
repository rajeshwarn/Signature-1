using Jose;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Signature.Core.AppleDeveloperManager.Impl
{
	public class AppleDeveloperManager
	{
		public string ApiUrl { get; set; }
		public string KeyID{ get; set; }
		public string IssueID { get; set; }
		public string Audience { get; set; }
		public string KeyString { get; set; }
		public int ExpireMinutes { get; set; } = 20;

		#region 生成 Token
		private string GetUnixTimeString(DateTime dateTime)
		{
			var unixTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
			return unixTime.ToString();
		}

		private string GenerateToken()
		{
			var now = DateTime.Now;
			var exp = GetUnixTimeString(now.AddMinutes(ExpireMinutes));
			var payload = new Dictionary<string, object>()
			{
                { "exp", exp },
				{ "iss", IssueID },
				{ "aud", Audience }
			};
			var extraHeader = new Dictionary<string, object>()
			{
				{ "alg", "ES256" },
				{ "typ", "JWT" },
				{ "kid", KeyID }
			};
			CngKey privateKey = CngKey.Import(Convert.FromBase64String(KeyString), CngKeyBlobFormat.Pkcs8PrivateBlob);
			string token = JWT.Encode(payload, privateKey, JwsAlgorithm.ES256, extraHeader);
			return token;
		}

		private string HashHmac(List<KeyValuePair<string, string>> paras, string apiKey)
		{
			string plain = string.Empty;
			paras.Sort((x, y) => (x.Key.CompareTo(y.Key)));
			foreach (var item in paras)
			{
				plain += item.Value;
			}
			var key = Encoding.UTF8.GetBytes(apiKey);
			var payload = Encoding.UTF8.GetBytes(plain);
			using (var hmacSHA = new HMACSHA256(key))
			{
				var hash = hmacSHA.ComputeHash(payload, 0, payload.Length);
				return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
			}
		}
		#endregion


	}
}
