using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Meta
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public override bool Equals(object obj)
        {
            return obj.ToString().ToUpper().Equals(this.Key.ToUpper());
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public KeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public KeyValue()
            : this(string.Empty, string.Empty)
        {
        }
        public override string ToString()
        {
            return string.Format("{0}:{1}", this.Key, this.Value);
        }
    }

    public class KeyValueCollection : ICollection<KeyValue>
    {
        private List<KeyValue> fls = new List<KeyValue>();
        public void Add(KeyValue item)
        {
            this.fls.Add(item);
        }
        public KeyValueCollection Add(string key, string value)
        {
            this.Add(new KeyValue(key, value));
            return this;
        }
        public void Clear()
        {
            this.fls.Clear();
        }

        public bool Contains(KeyValue item)
        {
            return this.fls.Contains(item);
        }

        public void CopyTo(KeyValue[] array, int arrayIndex)
        {
            if (arrayIndex > (array.Length - 1)) return;
            IList<KeyValue> ncols = this.fls.ToList<KeyValue>();
            for (var i = 0; (i <= (array.Length - arrayIndex) && i < ncols.Count()); i++) array[i + arrayIndex] = ncols[i];
        }

        public int Count
        {
            get { return this.fls.Count(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValue item)
        {
            return this.fls.Remove(item);
        }

        public IEnumerator<KeyValue> GetEnumerator()
        {
            return this.fls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.fls.GetEnumerator();
        }
    }

    
}
