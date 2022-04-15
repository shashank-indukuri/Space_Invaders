using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SingleLinkIterator : BaseIterator
    {
        // Constructor
        public SingleLinkIterator()
        {
            // Initializing the values of currentNode, bIsComplete and PHeadNode
            PrivClear();
        }

        // Methods
        private void PrivClear()
        {
            pCurrentNode = null;
            bIsComplete = true;
            pHeadNode = null;
        }

        // Overridings
        public override bool IsDone()
        {
            // Return the current state
            return bIsComplete;
        }

        public override BaseNode First()
        {
            // This will return the header node and reset the iterator
            if (pHeadNode != null)
            {
                pCurrentNode = pHeadNode;
                bIsComplete = false;
            }
            else
            {
                PrivClear();
            }
            return pCurrentNode;
        }

        public override BaseNode Next()
        {
            // Downcast the BaseNode to SingleLink
            SingleLink pTempNode = (SingleLink)pCurrentNode;

            if (pTempNode != null)
            {
                pTempNode = pTempNode.pNext;
            }

            pCurrentNode = pTempNode;

            // Setting the bIsComplete to false if the next node is not null
            if (pTempNode != null)
            {
                bIsComplete = false;
            }
            else
            {
                bIsComplete = true;
            }

            // Returns the next node
            return pCurrentNode;
        }

        public override BaseNode Current()
        {
            return pCurrentNode;
        }

        public void Reset(BaseNode pCurrentHead)
        {
            // Clearing the values and setting them to default
            if (pCurrentHead != null)
            {
                pCurrentNode = pCurrentHead;
                bIsComplete = false;
                pHeadNode = pCurrentHead;
            }
            else
            {
                PrivClear();
            }
        }

        // Data
        public BaseNode pHeadNode;
        public BaseNode pCurrentNode;
        public bool bIsComplete;
    }
}

// End of file