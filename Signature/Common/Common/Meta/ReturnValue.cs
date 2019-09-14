#region using
using Common.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#endregion

namespace Common.Meta
{
    [Serializable]
    public class ReturnValue<T>
    {
        public string FailedMessage { get; set; }
        public bool Passed { get; set; }
        public T Data { get; set; }

        public ReturnValue(bool passed, T data, string failedMessage)
        {
            this.Passed = passed;
            if (passed) this.Data = data;
            this.FailedMessage = passed ? string.Empty : failedMessage;
        }
        public ReturnValue(T data)
            : this(true, data, string.Empty)
        {

        }
        public ReturnValue()
        {
            this.Passed = true;
            this.FailedMessage = string.Empty;
        }
        public ReturnValue ToReturnValue()
        {
            return new ReturnValue(this.Passed, this.Data, this.FailedMessage);
        }
        public override string ToString()
        {
            string q = string.Empty;
            if (Data is IDictionary) q = string.Format("[{0}]", string.Join(",", (Data as IDictionary).ToKeyValueArray().Select(v => string.Format("{0}:{1}", v.Key, v.Value))));
            else if (Data != null && Data.GetType() != typeof(string) && (Data.GetType().IsArray || Data is IEnumerable)) q = string.Format("{{{0}}}", string.Join(",", ((IEnumerable<object>)Data).Select(v => (v == null) ? "null" : v.ToString())));
            else if (Data is DataTable)
            {
                string cols = string.Empty;
                foreach (DataColumn c in (Data as DataTable).Columns) cols += "," + c.ColumnName;
                q = string.Format("dataTable({0}) [{1}]", (Data as DataTable).Rows.Count, cols.Equals(string.Empty) ? "" : cols.Substring(1));
            }
            else q = (Data == null) ? "null" : Data.ToString();
            return string.Format("{0}:{1}", this.Passed ? "true" : "false", this.Passed ? q : this.FailedMessage);
        }
        public ReturnValue<T> FailThrowException()
        {
            if (!this.Passed) throw new System.Exception(this.FailedMessage);
            return this;
        }
    }
    [Serializable]
    public class ReturnValue
    {
        public bool Passed { get; set; }
        public string FailedMessage { get; set; }
        public object Data { get; set; }

        public ReturnValue(bool passed, object data, string failedMessage)
        {
            this.Passed = passed;
            this.Data = passed ? data : null;
            this.FailedMessage = passed ? string.Empty : failedMessage;
        }
        public ReturnValue(object data)
            : this(true, data, string.Empty)
        {
        }
        public ReturnValue()
            : this(true, string.Empty, string.Empty)
        {

        }

        public override string ToString()
        {
            string q = string.Empty;
            if (Data is IDictionary) q = string.Format("[{0}]", string.Join(",", (Data as IDictionary).ToKeyValueArray().Select(v => string.Format("{0}:{1}", v.Key, v.Value))));
            else if (Data != null && Data.GetType() != typeof(string) && (Data.GetType().IsArray || Data is IEnumerable)) q = string.Format("{{{0}}}", string.Join(",", ((IEnumerable<object>)Data).Select(v => (v == null) ? "null" : v.ToString())));
            else if (Data is DataTable)
            {
                string cols = string.Empty;
                foreach (DataColumn c in (Data as DataTable).Columns) cols += "," + c.ColumnName;
                q = string.Format("dataTable({0}) [{1}]", (Data as DataTable).Rows.Count, cols.Equals(string.Empty) ? "" : cols.Substring(1));
            }
            else q = (Data == null) ? "null" : Data.ToString();
            return string.Format("{0}:{1}", this.Passed ? "true" : "false", this.Passed ? q : this.FailedMessage);
        }

        public ReturnValue FailThrowException()
        {
            if (!this.Passed) throw new System.Exception(this.FailedMessage);
            return this;
        }
    }

}
