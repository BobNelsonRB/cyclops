using System;
using System.Collections;
using System.Collections.Generic;

namespace Cyclops.Data.Common
{
    public class Parameters : IEnumerable<Parameter>
    {
        private const string StrategyKey = "24F8BF83";

        private ParameterCollection _Parameters = new ParameterCollection();


        public Parameters(string key, object paramValue)
        {
            AddParam(key, paramValue);
        }

        public Parameters(string key, string paramValue)
        {
            AddParam(key, paramValue);
        }
        public Parameters(string key, int paramValue)
        {
            AddParam(key, paramValue);
        }
        public Parameters(string key, decimal paramValue)
        {
            AddParam(key, paramValue);
        }
        public Parameters(string key, DateTime paramValue)
        {
            AddParam(key, paramValue);
        }
        public Parameters()
        {

        }

        public void Add(string key, object value)
        {
            _Parameters.Add(new Parameter() { Key = key, Value = value });
        }

        public void Add(string key, string value)
        {
            _Parameters.Add(new Parameter() { Key = key, Value = value });
        }
        public void Add(string key, int value)
        {
            _Parameters.Add(new Parameter() { Key = key, Value = value });
        }
        public void Add(string key, decimal value)
        {
            _Parameters.Add(new Parameter() { Key = key, Value = value });
        }
        public void Add(string key, DateTime value)
        {
            _Parameters.Add(new Parameter() { Key = key, Value = value });
        }


        private void AddParam(string key, object paramValue)
        {
            if (!_Parameters.Contains(key))
            {
                _Parameters.Add(new Parameter() { Key = key, Value = paramValue });
            }
        }

        public T GetValue<T>(string key)
        {
            T t = default(T);
            if (_Parameters.Contains(key))
            {
                Parameter p = _Parameters[key];
                if (p.Value is T)
                {
                    t = (T)p.Value;
                }
            }
            return t;
        }

        public bool TryGetValue<T>(string key, out T t)
        {
            bool b = false;
            t = default(T);
            if (_Parameters.Contains(key) && _Parameters[key].Value is T)
            {
                t = (T)_Parameters[key].Value;
                b = true;
            }
            return b;
        }

        public static Parameters SetStrategy(string key, object o = null)
        {
            Parameters p = new Parameters(StrategyKey, key);
            if (o != null)
            {
                p.AddParam(key, o);
            }
            return p;
        }

        public bool HasStrategy()
        {
            return _Parameters.Contains(StrategyKey);
        }
        public string GetStrategyKey()
        {
            string s = String.Empty;
            if (HasStrategy())
            {
                s = (string)_Parameters[StrategyKey].Value;
            }
            return s;
        }

        public IEnumerator<Parameter> GetEnumerator()
        {
            return ((IEnumerable<Parameter>)_Parameters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Parameter>)_Parameters).GetEnumerator();
        }
    }
}
