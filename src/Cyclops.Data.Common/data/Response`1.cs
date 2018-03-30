using System;
using System.Collections;
using System.Collections.Generic;

namespace Cyclops.Data.Common
{
    public class Response<T> : IEnumerable<T>
    {
        private List<T> _Items = new List<T>();
        public List<T> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public T Model
        {
            get { return _Items.Count > 0 ? _Items[0] : default(T); }
        }

        public bool IsOkay { get; private set; }

        public string Message { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T t in _Items)
            {
                yield return t;
            }
        }

        public void SetStatus(Exception ex)
        {
            Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            IsOkay = false;
        }

        public void SetStatus(bool isOkay)
        {
            IsOkay = isOkay;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)_Items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_Items).GetEnumerator();
        }
    }
}
