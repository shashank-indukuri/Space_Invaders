using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class DoubleLink : BaseNode
    {
        protected DoubleLink()
        {
            ClearDoubleLink();
        }

        public void ClearDoubleLink()
        {
            pPrev = null;
            pNext = null;
            priority = 1;
        }

        public void SetPriority(float priority)
        {
            this.priority = priority;
        }

        override public void Dump()
        {
            // Printing the current node previous and next values with hash codes
            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                BaseNode pTmp = (BaseNode)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                BaseNode pTmp = (BaseNode)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }
        }


        // Data
        public DoubleLink pPrev;
        public DoubleLink pNext;
        public float priority;
    }
}

// End of file