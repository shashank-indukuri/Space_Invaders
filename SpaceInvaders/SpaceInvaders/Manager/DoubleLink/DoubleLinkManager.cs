using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class DoubleLinkManager : BaseType
    {

        public DoubleLinkManager()
            : base()
        {
            // LTN - DoubleLinkManager
            poIter = new DoubleLinkIterator();
            poHead = null;
        }

        override public void AddNodeToFront(BaseNode pCurrentNode)
        {
            // Make sure the node pointer is not null
            Debug.Assert(pCurrentNode != null);

            // Downcasting BaseNode to DoubleLink is safe
            DoubleLink pNode = (DoubleLink)pCurrentNode;

            if (poHead == null)
            {
                poHead = pNode;
                pNode.pPrev = null;
                pNode.pNext = null;
            }
            else
            {
                poHead.pPrev = pNode;
                pNode.pNext = poHead;
                poHead = pNode;
            }

            // Check if the node is added successfully to the linked list
            Debug.Assert(poHead != null);
        }

        override public void AddNodeToEnd(BaseNode pCurrentNode)
        {
            // Make sure the node pointer is not null
            Debug.Assert(pCurrentNode != null);

            // Downcasting BaseNode to DoubleLink is safe
            DoubleLink pNode = (DoubleLink)pCurrentNode;
            if (poHead == null)
            {
                poHead = pNode;
                pNode.pPrev = null;
                pNode.pNext = null;
            }
            else
            {
                DoubleLink pTemp = poHead;

                // Transvering the pTemp to the last node in the list
                while (pTemp.pNext != null)
                {
                    pTemp = pTemp.pNext;
                }

                // Adding the node to the end of the list
                pTemp.pNext = pNode;
                pNode.pPrev = pTemp;
            }

            // Check if the node is added successfully to the linked list
            Debug.Assert(poHead != null);
        }

        public override void AddNodeOnPriority(BaseNode pCurrentNode, float priority)
        {
            // Make sure the node pointer is not null
            Debug.Assert(pCurrentNode != null);

            // Downcasting BaseNode to DoubleLink is safe
            DoubleLink pNode = (DoubleLink)pCurrentNode;
            if (poHead == null)
            {
                poHead = pNode;
                pNode.pPrev = null;
                pNode.pNext = null;
            }
            else
            {
                DoubleLink pTemp = poHead;

                // Transvering the pTemp to the last node in the list
                while (pTemp.pNext != null && priority > pTemp.priority)
                {
                    pTemp = pTemp.pNext;
                }

                if (pTemp.pNext == null && priority > pTemp.priority)
                {
                    // Adding the node to the end of the list
                    pTemp.pNext = pNode;
                    pNode.pPrev = pTemp;
                }
                else if (pTemp.pPrev == null)
                {
                    // Adding the node to the beginning of the list
                    pTemp.pPrev = pNode;
                    pNode.pNext = pTemp;
                    poHead = pNode;
                }
                else
                {
                    // Adding before the current node of the list
                    pTemp.pPrev.pNext = pNode;
                    pNode.pPrev = pTemp.pPrev;
                    pNode.pNext = pTemp;
                    pTemp.pPrev = pNode;
                }
            }

            pNode.SetPriority(priority);
            // Check if the node is added successfully to the linked list
            Debug.Assert(poHead != null);
        }

        override public BaseNode RemoveNodeFromFront()
        {
            Debug.Assert(poHead != null);
            // Copy the current head to return
            DoubleLink pCurrentHead = poHead;

            // Change the header pointer to next node
            poHead = poHead.pNext;
            if (poHead != null)
            {
                poHead.pPrev = null;
            }
            else
            {
                Debug.Assert(poHead == null);
            }

            // Clear the prev and next values of the first node
            pCurrentHead.ClearDoubleLink();
            return pCurrentHead;
        }

        override public void RemoveNode(BaseNode pNodeToRemove)
        {
            // Make sure the header pointer and node to be deleted are not null
            Debug.Assert(poHead != null);
            Debug.Assert(pNodeToRemove != null);

            // Downcasting BaseNode to DoubleLink is safe
            DoubleLink pRemoveNode = (DoubleLink)pNodeToRemove;

            // List has only one node
            if (poHead.pPrev == null && poHead.pNext == null)
            {
                poHead = null;
            }
            // The node to be deleted is the first node
            else if (pRemoveNode.pPrev == null && pRemoveNode.pNext != null)
            {
                poHead = pRemoveNode.pNext;
                poHead.pPrev = pRemoveNode.pPrev;
            }
            // The node to be deleted is the last node
            else if (pRemoveNode.pPrev != null && pRemoveNode.pNext == null)
            {
                pRemoveNode.pPrev.pNext = pRemoveNode.pNext;
            }
            // Any other case
            else
            {
                pRemoveNode.pPrev.pNext = pRemoveNode.pNext;
                pRemoveNode.pNext.pPrev = pRemoveNode.pPrev;
            }

            // Clearing the linkage for the removed node from the list
            pRemoveNode.ClearDoubleLink();
        }

        public override BaseIterator FetchIterator()
        {
            // Resetting the Iterator to current head
            poIter.Reset(poHead);
            return poIter;
        }

        // Data
        public DoubleLink poHead;
        public DoubleLinkIterator poIter;
    }
}

// End of file