using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public class CascadingListWithSelectedElement<T> : ListWithSelectedElement<T>
        where T : Modifiable
    {
        public event EventHandler SubModification;
        private EventHandler SubElement_ModifiedEventHandler;

        public CascadingListWithSelectedElement()
        {
            Modified += new EventHandler<ContentsModifiedEventArgs<T>>(CascadingListWithSelectedElement_Modified);
            SubElement_ModifiedEventHandler = new EventHandler(SubElement_Modified);
        }

        void CascadingListWithSelectedElement_Modified(object sender, ContentsModifiedEventArgs<T> e)
        {
            if (e.Action == ContentAction.Add)
            {
                e.Element.Modified += SubElement_ModifiedEventHandler;
            }
            else if (e.Action == ContentAction.Remove)
            {
                e.Element.Modified -= SubElement_ModifiedEventHandler;
            }
        }

        void SubElement_Modified(object sender, EventArgs e)
        {
            if (SubModification != null) SubModification(sender, e);
        }
    }
}
