using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor
{
    public class ListWithSelectedElement<T> : List<T>
    {
        public event EventHandler<ModificationEventArgs<T>> SelectedChanged;
        public event EventHandler<AddedEventArgs<T>> Added;
        public event EventHandler<RemovedEventArgs<T>> Removed;

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

                    ModificationEventArgs<T> e = new ModificationEventArgs<T>(selected, value);
                    selected = value;
                    if (SelectedChanged != null) SelectedChanged(this, e);
                }
            }
        }

        new public void Add(T item)
        {
            base.Add(item);
            if (Added != null) Added(this, new AddedEventArgs<T>(item));
            if (Count == 1)
            {
                Selected = item;
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
                if (Removed != null) Removed(this, new RemovedEventArgs<T>(item));
            }
            return success;
        }

        new public void Clear()
        {
            base.Clear();
            selected = default(T);
        }

        [Obsolete("Not implemented yet.", true)]
        new public int RemoveAll(Predicate<T> match)        { throw new NotImplementedException(); }
        [Obsolete("Not implemented yet.", true)]
        new public void RemoveAt(int index)                 { throw new NotImplementedException(); }
        [Obsolete("Not implemented yet.", true)]
        new public void RemoveRange(int index, int count)   { throw new NotImplementedException(); }
    }
}
