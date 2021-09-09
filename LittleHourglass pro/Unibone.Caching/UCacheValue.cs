using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching
{
    internal class UCacheValue
    {
        public UCacheValue()
        { }
        public UCacheValue(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
        private string _key = null;
        public string Key
        {
            get
            {
                if (_key != null)
                    return _key.ToLower();
                return _key;
            }
            set
            {
                if (value != null)
                    _key = value.ToLower();
                else
                    _key = value;
            }
        }
        public object Value { get; set; }
    }
}
