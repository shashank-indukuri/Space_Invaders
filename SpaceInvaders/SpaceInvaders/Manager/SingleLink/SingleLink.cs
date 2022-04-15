using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SingleLink : BaseNode
    {
        // Constructor
        protected SingleLink()
        {
            Clear();
        }

        public void Clear()
        {
            pNext = null;
        }

        // Overriding methods
        override public void Dump()
        {
            // Printing the current node next values with hash codes
            if (pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                BaseNode pTmp = pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }
        }

        // Data
        public SingleLink pNext;
    }
}

// End of file