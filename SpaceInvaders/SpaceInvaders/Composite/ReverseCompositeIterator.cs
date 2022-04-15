using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ReverseCompositeIterator : BaseCompositeIterator
    {
        // Constructor
        public ReverseCompositeIterator(Component pHead)
        {
            // pHead should be composite and not null
            Debug.Assert(pHead != null);
            Debug.Assert(pHead.type == Component.Type.Composite);

            // Initializing the current, first and previous pointers
            pCurrent = pHead;
            pFirst = pHead;
            pPrevious = null;

            // Hack that needs to corrected
            ForwardCompositeIterator pForwardItr = new ForwardCompositeIterator(pHead);


            Component pPrevNode = this.pFirst;

            // Initialize the reverse pointer
            Component pCurrentNode = pForwardItr.First();

            while (!pForwardItr.IsDone())
            {
                // Store in current node as a temp
                pPrevNode = pCurrentNode;

                // Get the next node
                pCurrentNode = pForwardItr.Next();

                if (pCurrentNode != null)
                {
                    pCurrentNode.pReverse = pPrevNode;
                }
            }

            pFirst.pReverse = pPrevNode;
        }

        // Overiding methods
        public override Component First()
        {
            Debug.Assert(this.pFirst != null);

            this.pCurrent = this.pFirst.pReverse;
            return pCurrent;
        }

        public override Component Current()
        {
            return pCurrent;
        }

        public override bool IsDone()
        {
            if (pFirst == pPrevious)
            {
                return true;
            }
            return false;
        }

        public override Component Next()
        {
            Debug.Assert(pCurrent != null);

            this.pPrevious = this.pCurrent;

            // Setting the current reverse node as current node
            this.pCurrent = this.pCurrent.pReverse;
            return this.pCurrent;
        }

        // Data
        private Component pCurrent;
        private Component pPrevious;
        private Component pFirst;
    }
}

// End of file