using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericList
{
    public interface IGenericList<X> : IEnumerable<X>
    {
        /// <summary>
        /// Adds an item to the collection .
        /// </summary>
        void Add(X item);
        /// <summary>
        /// Removes the first occurrence of an item from the collection .
        /// If the item was not found , method does nothing .
        /// </summary>
        bool Remove(X item);
        /// <summary>
        /// Removes the item at the given index in the collection .
        /// </summary>
        bool RemoveAt(int index);
        /// <summary>
        /// Returns the item at the given index in the collection .
        /// </summary>
        X GetElement(int index);
        /// <summary>
        /// Returns the index of the item in the collection .
        /// If item is not found in the collection , method returns -1.
        /// </summary>
        int IndexOf(X item);
        /// <summary>
        /// Readonly property . Gets the number of items contained in the collection.
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Removes all items from the collection .
        /// </summary>
        void Clear();
        /// <summary>
        /// Determines whether the collection contains a specific value .
        /// </summary>
        bool Contains(X item);
    }
    public class NegativeSizeException : Exception
    {
        public override string ToString()
        {
            return "Nema negativno";
        }
    }
    public class GenericList<X> : IGenericList<X>
    {

        X[] _container;
        int _filled = 0;
        public GenericList()
        {
            _container = new X[4];
        }

        public GenericList(int initialSize)
        {
            if (initialSize < 0)
                throw new NegativeSizeException();
            _container = new X[initialSize];
        }

        public void Add(X item)
        {
            if (_container.Length == Count)
            {
                Array.Resize(ref _container, 2 * Count);
            }
            _container[Count] = item;
            _filled++;
        }

        public bool RemoveAt(int index)
        {
            if (index >= Count || index < 0)
                return false;
            for (int i = index; i < Count; i++)
            {
                _container[i] = _container[i + 1];
            }
            _filled--;
            return true;
        }

        public bool Remove(X item)
        {
            return RemoveAt(Array.IndexOf(_container, item));
        }

        public X GetElement(int index)
        {
            return _container[index];
        }

        public int IndexOf(X item)
        {
            return Array.IndexOf(_container, item);
        }

        public IEnumerator GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        public bool Contains(X item)
        {
            return Array.IndexOf(_container, item) > 0;
        }

        public int Count => _filled;

        public void Clear()
        {
            Array.Clear(_container, 0, _container.Length);
            _filled = 0;
        }

        IEnumerator<X> IEnumerable<X>.GetEnumerator()
        {
            return (IEnumerator<X>)GetEnumerator();
        }

        public class GenericListEnumerator<X> : IEnumerator<X>
        {
            GenericList<X> list;
            int _currentIndex = -1;
            public GenericListEnumerator(GenericList<X> list)
            {
                this.list = list;
            }


            public void Reset()
            {
                _currentIndex = 0;
            }
            public X Current => list._container[_currentIndex];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Console.WriteLine("Vrtiiiiiii");
            }
            public bool MoveNext()
            {
                if (_currentIndex < list._container.Length - 1)
                {
                    _currentIndex++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

    }
}
namespace Models
{
    public class ToDoItem
    {
        public Guid ID { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public ToDoItem(string text)
        {
            ID = Guid.NewGuid();
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now;
        }

        public void MarkAsDone()
        {
            if (IsCompleted)
                return;
            IsCompleted = true;
            DateCompleted = DateTime.Now;
        }
    }
}

