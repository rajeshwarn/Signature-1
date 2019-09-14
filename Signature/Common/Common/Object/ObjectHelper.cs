using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Caching;
using Common.Logging;
using Common.Meta;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Collections.Specialized;
using System.Configuration;
using Common.Configuration.Element;
using Common.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Web.Script.Serialization;

namespace Common.Object
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnorePropertyAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
        public string DictioanryKeys { get; set; }
        public RequiredAttribute(string DictionaryKeys)
        {
            this.DictioanryKeys = DictioanryKeys;
        }
        public RequiredAttribute() : this("")
        {

        }
    }

    public static class ObjectHelper
    {
        public static string ToString(this ParameterInfo[] parameters, IMethodInvocation method, bool forLogging = false)
        {
            string str = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                string val = string.Empty;
                object data = method.Arguments[i] ?? null;

                IgnoreLoggerAttribute attr = parameters[i].GetCustomAttribute<IgnoreLoggerAttribute>();
                if (attr != null && forLogging) val = "***";
                else
                {
                    if (parameters[i] is ICloneable) val = (data as ICacheable).CacheKey;
                    else if (data != null && (parameters[i].ParameterType.IsArray || typeof(IEnumerable<object>).IsAssignableFrom(parameters[i].ParameterType)))
                        val = string.Format("{{{0}}}", string.Join(",", ((IEnumerable<object>)data).Select(v => (v == null) ? "null" : v.ToString())));
                    else val = (data == null) ? "null" : data.ToString();
                }
                str += string.Format(",{0}:{1}", parameters[i].Name, val);
            }
            return str.Equals(string.Empty) ? string.Empty : str.Substring(1);
        }

        public static string ToString(DataTable dataTable, string columnSplitChar, string rowSplitChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow r in dataTable.Rows)
            {
                string str = string.Empty;
                for (int i = 0; i < dataTable.Columns.Count; i++) str += string.Format("{0}{1}", columnSplitChar, r[i] ?? "");
                sb.Append(string.Format("{0}{1}", rowSplitChar, str.Equals(string.Empty) ? "" : str.Substring(columnSplitChar.Length)));
            }
            return (sb.ToString().Length == 0) ? string.Empty : sb.ToString().Substring(rowSplitChar.Length);
        }

        public static string ToJson(this object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (obj == null) return "null";
            return jss.Serialize(obj);
        }
        public static T JsonToObject<T>(this string jsonString) where T : class
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(jsonString);
        }

        public static KeyValue[] ToKeyValueArray(this IDictionary dict)
        {
            List<KeyValue> kvs = new List<KeyValue>();
            foreach (string key in dict.Keys) kvs.Add(new KeyValue(key.ToUpper(), ((dict[key] == null) ? string.Empty : dict[key].ToString())));
            return kvs.ToArray<KeyValue>();
        }

        public static NameValueCollection ToNameValueCollection(this object obj, object obj2 = null, bool overwrite = true)
        {
            var nv = new System.Collections.Specialized.NameValueCollection();
            if (obj == null) return nv;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo p in properties) nv.Add(p.Name, string.Format("{0}", p.GetValue(obj)));
            if (obj2 != null)
            {
                PropertyInfo[] properties2 = obj2.GetType().GetProperties();
                foreach (PropertyInfo p in properties2)
                {
                    if (nv[p.Name] == null) nv.Add(p.Name, string.Format("{0}", p.GetValue(obj2)));
                    else if (overwrite) nv[p.Name] = string.Format("{0}", p.GetValue(obj2));
                }
            }
            return nv;
        }

        public static T CreateObject<T>(this ComponentElement com) where T : class
        {
            Type type = Type.GetType(com.Type);
            object obj = type.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes);
            if (com.ComponentProperties != null)
            {
                if (obj is IConfigable) foreach (NameValueConfigurationElement p in com.ComponentProperties) (obj as IConfigable).SetProperty(p.Name, p.Value);
            }
            return obj as T;
        }

        public static T CreateObject<T>(string type, NameValueConfigurationCollection properties) where T : class
        {
            Type t = Type.GetType(type);
            object obj = t.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes);
            if (properties != null)
            {
                if (obj is IConfigable) foreach (NameValueConfigurationElement p in properties) (obj as IConfigable).SetProperty(p.Name, p.Value);
            }
            return obj as T;
        }

        public static object InitFields(this object obj, IDictionary<string, object> values)
        {
            if (values == null) return obj;
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (FieldInfo f in fields)
            {
                try
                {
                    if (values.ContainsKey(f.Name) && values[f.Name] != DBNull.Value) f.SetValue(obj, values[f.Name]);
                }
                catch
                {
                    continue;
                }
            }
            return obj;
        }

        public static object InitProperties(this object obj, IDictionary<string, object> values)
        {
            if (values == null) return obj;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo p in properties) if (values.ContainsKey(p.Name) && values[p.Name] != DBNull.Value) p.SetValue(obj, values[p.Name], null);
            return obj;
        }

        public static object InitProperties(this object obj, IDictionary<string, string> values)
        {
            if (values == null) return obj;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo p in properties) if (values.ContainsKey(p.Name) && values[p.Name] != null && !values[p.Name].Equals(string.Empty)) p.SetValue(obj, System.Convert.ChangeType(values[p.Name], p.PropertyType), null);
            return obj;
        }

        public static object InitProperties(this object obj, DataRow row)
        {
            if (row == null) return obj;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                try
                {
                    var v = row[p.Name];
                    if (v != DBNull.Value) p.SetValue(obj, v, null);
                }
                catch
                {
                    continue;
                }
            }
            return obj;
        }

        public static IDictionary<string, object> GetProperties(this object obj)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            PropertyInfo[] ps = obj.GetType().GetProperties();
            foreach (PropertyInfo p in ps)
            {
                if (p.GetCustomAttribute(typeof(IgnorePropertyAttribute)) != null) continue;
                Type t = p.PropertyType;
                if (!t.IsArray && !t.IsAbstract && !t.IsByRef && !(t is IList) && !(t is IDictionary)) properties.Add(p.Name, p.GetValue(obj, null));
            }
            return properties;
        }

        public static IDictionary<string, object> GetFields(this object obj)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();
            FieldInfo[] fs = obj.GetType().GetFields();
            foreach (FieldInfo f in fs) fields.Add(f.Name, f.GetValue(obj));
            return fields;
        }

        public static T XmlDeserialize<T>(this string xml)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xml))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static string ToString(this IDictionary dict, string splitChar = " ")
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in dict.Keys) sb.Append(string.Format("{0}={1}{2}", s, dict[s], splitChar));
            return sb.ToString().Trim();
        }

        public static string XmlSerialize<T>(this T obj)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            ser.Serialize(sw, obj);
            return sw.ToString();
        }

        public static void AddToNameValueCollection(this System.Web.HttpCookieCollection cookies, System.Collections.Specialized.NameValueCollection nv, bool overwrite = false)
        {
            for (var i = 0; i < cookies.Count; i++)
            {
                var name = cookies[i].Name;
                var value = cookies[name].Value;
                if (nv[name] == null) nv.Add(name, value);
                else if (overwrite) nv[name] = value;
            }
        }

    }
}
