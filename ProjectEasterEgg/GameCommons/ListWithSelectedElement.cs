using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public class ListWithSelectedElement<T> : List<T>, Modifiable<ContentsModifiedEventArgs<T>>
    {
        public event EventHandler<ReplacedEventArgs<T>> SelectedChanged;
        public event EventHandler<ContentsModifiedEventArgs<T>> Modified;

        new public T this[int index]
        {
            get { return base[index]; }
            set
            {
                if (EqualityComparer<T>.Default.Equals(base[index], selected))
                {
                    Selected = value;
                }
                base[index] = value;
            }
        }

        protected T selected;
        public T Selected
        {
            get { return selected; }
            set
            {
                if (!EqualityComparer<T>.Default.Equals(selected, value))
                {
                    if (!Contains(value))
                    {
                        throw new ArgumentException("Can't select an element that isn't already in the list");
                    }

                    ReplacedEventArgs<T> e = new ReplacedEventArgs<T>(selected, value);
                    selected = value;
                    if (SelectedChanged != null) SelectedChanged(this, e);
                }
            }
        }

        public T Previous
        {
            get { return this[((IndexOf(selected)-1)+Count) % Count]; }
        }

        public T Next
        {
            get { return this[(IndexOf(selected)+1) % Count]; }
        }

        new public void Add(T item)
        {
            base.Add(item);
            if (Modified != null) Modified(this, new ContentsModifiedEventArgs<T>(ContentAction.Add, item));
            if (Count == 1)
            {
                Selected = item;
            }
        }

        new public void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                Add(item);
            }
        }


        new public bool Remove(T item)
        {
            bool success = base.Remove(item);
            if (success)
            {
                if (EqualityComparer<T>.Default.Equals(selected, item) && !Contains(item))
                {
                    selected = default(T);
                }
                if (Modified != null) Modified(this, new ContentsModifiedEventArgs<T>(ContentAction.Remove, item));
            }
            return success;
        }

        new public void Clear()
        {
            base.Clear();
            selected = default(T);
            if (Modified != null) Modified(this, new ContentsModifiedEventArgs<T>(ContentAction.Clear, default(T)));
        }

        new public int RemoveAll(Predicate<T> match)
        {
            int removedCount = 0;
            for (int i = 0; i < Count; )
            {
                if (match(this[i]))
                {
                    Remove(this[i]);
                    removedCount++;
                }
                else
                {
                    i++;
                }
            }
            return removedCount;
        }

        [Obsolete("Not implemented yet.", true)]
        new public void RemoveAt(int index)                 { throw new NotImplementedException(); }
        [Obsolete("Not implemented yet.", true)]
        new public void RemoveRange(int index, int count)   { throw new NotImplementedException(); }
    }
}
