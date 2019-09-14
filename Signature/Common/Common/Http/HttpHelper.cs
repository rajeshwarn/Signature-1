using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Http
{
    public static class HttpHelper
    {
        public static Dictionary<string, string> TypeToDictionary(object source)
        {
            var type = source.GetType();

            return type.GetProperties().ToDictionary(
                  p => p.Name,
                  p =>
                  {
                      var v = p.GetValue(source, null);
                      if (v == null)
                          return string.Empty;
                      return v.ToString();
                  });
        }

        public static string CombinationDictionary(IDictionary<string, string> source, bool needOrder = false, bool filterEmpty = true, string symbol = @"=", string groupSymbol = @"&")
        {
            var list = source.Where(x =>
            {
                //如果需要筛选为空的，则判断该值是否为空
                if (filterEmpty)
                    return !string.IsNullOrWhiteSpace(x.Value);
                //否则全部返回
                return true;
            });
            if (needOrder)
                list = list.OrderBy(x => x.Key);

            return list
                .Select(x => x.Key + symbol + x.Value)
                .Aggregate((x, y) => x + groupSymbol + y);
        }

        public static string BasePost(string data, string url, Encoding encoding)
        {
            return BasePost(data, url, encoding, null, null);
        }
        public static string BasePost(string data, string url, Encoding encoding, Func<HttpWebRequest, object> handlerRequest)
        {
            return BasePost(data, url, encoding, handlerRequest, null);
        }

        public static string BasePost(string data, string url, Encoding encoding, Func<HttpWebResponse, HttpStatusCode, string, string> processResponse)
        {
            return BasePost(data, url, encoding, null, processResponse);
        }

        public static string BasePost(string data, string url, Encoding encoding, Func<HttpWebRequest, object> handlerRequest, Func<HttpWebResponse, HttpStatusCode, string, string> processResponse)
        {
            Func<HttpWebRequest, HttpWebRequest> setRequest = x =>
            {
                x.Method = "POST";

                if (handlerRequest != null)
                {
                    var result = handlerRequest(x);
                    //当返回结果是可执行函数时，说明使用者意图接管后续所有操作，包括数据写入输入流的过程
                    if ((result as Action) != null)
                    {
                        var action = result as Action;
                        action();
                    }
                    //当返回结果为其它的时候，则说明handler函数除了设置请求对象以外，并没有更多意图
                    //所以直接进行数据写入输入流的过程
                    else
                    {
                        using (var writer = x.GetRequestStream())
                        {
                            var byteData = encoding.GetBytes(data);
                            writer.Write(byteData, 0, byteData.Length);
                        }
                    }
                }
                else
                {
                    using (var writer = x.GetRequestStream())
                    {
                        var byteData = encoding.GetBytes(data);
                        writer.Write(byteData, 0, byteData.Length);
                    }
                }

                return x;
            };

            return BaseSend(url, encoding, setRequest, processResponse);
        }

        public static string BaseGet(string url, Encoding encoding)
        {
            return BaseGet(url, encoding, null, null);
        }

        public static string BaseGet(string url, Encoding encoding, Func<HttpWebRequest, HttpWebRequest> handlerRequest)
        {
            return BaseGet(url, encoding, handlerRequest, null);
        }

        public static string BaseGet(string url, Encoding encoding, Func<HttpWebResponse, HttpStatusCode, string, string> processResponse)
        {
            return BaseGet(url, encoding, null, processResponse);
        }

        public static string BaseGet(string url, Encoding encoding, Func<HttpWebRequest, HttpWebRequest> handlerRequest, Func<HttpWebResponse, HttpStatusCode, string, string> processResponse)
        {
            Func<HttpWebRequest, HttpWebRequest> setRequest = x =>
            {
                x.Method = "GET";

                if (handlerRequest != null)
                    x = handlerRequest(x);

                return x;
            };

            return BaseSend(url, encoding, setRequest, processResponse);
        }

        private static string BaseSend(string url, Encoding encoding, Func<HttpWebRequest, HttpWebRequest> handlerRequest, Func<HttpWebResponse, HttpStatusCode, string, string> processResponse = null)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = "";
            request.Accept = "*/*";
            request.KeepAlive = true;
            request.CookieContainer = new CookieContainer();
            request.Timeout = 30 * 1000;

            request = handlerRequest(request);

            return GetWebResult(request, encoding, processResponse);
        }

        private static string GetWebResult(HttpWebRequest request, Encoding encoding, Func<HttpWebResponse, HttpStatusCode, string, string> processResponse)
        {
            var receiveString = "";
            HttpWebResponse wr = null;

            try
            {
                wr = request.GetResponse() as HttpWebResponse;
                Stream ReceiveStream = wr.GetResponseStream();
                using (StreamReader reader = new StreamReader(ReceiveStream, encoding))
                {
                    receiveString = reader.ReadToEnd();
                }
                if (processResponse != null)
                {
                    return processResponse(wr, wr.StatusCode, receiveString);
                }
            }
            catch (WebException ex)
            {
                wr = (HttpWebResponse)ex.Response;
                if (wr != null)
                {
                    Stream ReceiveStream = wr.GetResponseStream();
                    using (StreamReader reader = new StreamReader(ReceiveStream, encoding))
                    {
                        receiveString = reader.ReadToEnd();
                    }
					if (processResponse == null) return receiveString;
					return processResponse(wr, wr.StatusCode, receiveString);
                }
            }
            finally
            {
                if (wr != null) wr.Dispose();
            }
            return receiveString;
        }
    }
}
