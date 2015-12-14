using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CustomControls
{
    public interface IEmploymentable<T> :IEnumerable<T>
    {
        T Find(string key);
        T[] FindAll(string key);
        void Edit(T item);
        string Show(T item);

    }


    public class EmploymentCollection<T> : CollectionBase,ICollection<T>
    {
        public  IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly { get; }
    }
}
