using Common.Http;
using Common.Meta;
using Jose;
using Newtonsoft.Json;
using Signature.Models.AppleDeveloper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Signature.Core.AppleDeveloperManager.Impl
{
	public class AppleDeveloperManager
	{
		public string ApiUrl { get; set; } = "https://api.appstoreconnect.apple.com";
		public string KeyID { get; set; } = "9JLXB85HU7";
		public string IssueID { get; set; } = "32edb26b-647f-4365-b23f-a2a797515a42";
		public string Audience { get; set; } = "appstoreconnect-v1";
		public string KeyString { get; set; } = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQgF43e2V0H2XckSXAAldtGiK0cNBrJs9EHtmxgvu415MygCgYIKoZIzj0DAQehRANCAAQhUiAb6vs1NruZ+WoP/KGEAlXPTFlDbF7jztiQYlVEVz7jWaVl+2k75EqQCyVV5zA0yhcHVUuYKHRozYsxATwu";
		public int ExpireMinutes { get; set; } = 20;

		#region 生成 Token
		//private string GetUnixTimeString(DateTime dateTime)
		//{
		//	var unixTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
		//	return unixTime.ToString();
		//}

		private string GenerateToken()
		{
			var now = DateTime.Now;
			var exp = Math.Round((DateTime.UtcNow.AddMinutes(ExpireMinutes) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds, 0);
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
		#endregion

		#region Users
		/// <summary>
		/// 取得所有用户信息
		/// </summary>
		/// <returns></returns>
		public ReturnValue<Response<List<User>>> GetUsers()
		{
			var token = GenerateToken();
			var url = $@"{ApiUrl}/v1/users";
			var json = HttpHelper.BaseGet(url, Encoding.UTF8,
				(req) => {
					req.Headers.Add("Authorization", $"Bearer {token}");
					return req;
				});
			var users = JsonConvert.DeserializeObject<Response<List<User>>>(json);
			return new ReturnValue<Response<List<User>>>(users);
		}

		#endregion

		#region Devices 
		/// <summary>
		/// 取得所有装置信息
		/// </summary>
		/// <returns></returns>
		public ReturnValue<Response<List<Device>>> GetDevices()
		{
			var token = GenerateToken();
			var url = $@"{ApiUrl}/v1/devices";
			var json = HttpHelper.BaseGet(url, Encoding.UTF8,
				(req) => {
					req.Headers.Add("Authorization", $"Bearer {token}");
					return req;
				});
			var devices = JsonConvert.DeserializeObject<Response<List<Device>>>(json);
			return new ReturnValue<Response<List<Device>>>(devices);
		}

		/// <summary>
		/// 新增装置
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public ReturnValue<bool> CreateDevice(Request<DeviceCreate> device)
		{
			var httpstatus = HttpStatusCode.OK;
			var token = GenerateToken();
			var url = $@"{ApiUrl}/v1/devices";
			var json = HttpHelper.BasePost(JsonConvert.SerializeObject(device), $"{ApiUrl}{url}", Encoding.UTF8,
				(req) => {
					req.Headers.Add("Authorization", token);
					return req;
				}, 
				(resp, status, data) =>
				{
					httpstatus = status;
					return data;
				});
			if (httpstatus == HttpStatusCode.Created) return new ReturnValue<bool>();
			return new ReturnValue<bool>(false, false, string.Empty);
		}

		#endregion

	}
}
