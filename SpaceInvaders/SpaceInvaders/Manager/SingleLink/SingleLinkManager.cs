using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SingleLinkManager : BaseType
    {

        public SingleLinkManager()
            : base()
        {
            // LTN - SingleLinkManager
            poIter = new SingleLinkIterator();
            poHead = null;
        }

        // Overriding methods
        override public void AddNodeToFront(BaseNode pCurrentNode)
        {
            // Make sure the node pointer is not null
            Debug.Assert(pCurrentNode != null);

            // Downcasting BaseNode to DoubleLink is safe
            SingleLink pNode = (SingleLink)pCurrentNode;

            if (poHead == null)
            {
                poHead = pNode;
                pNode.pNext = null;
            }
            else
            {
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
            SingleLink pNode = (SingleLink)pCurrentNode;
            if (poHead == null)
            {
                poHead = pNode;
                pNode.pNext = null;
            }
            else
            {
                SingleLink pTemp = poHead;
                // Transvering the pTemp to the last node in the list
                while (pTemp.pNext != null)
                {
                    pTemp = pTemp.pNext;
                }

                // Adding the node to the end of the list
                pTemp.pNext = pNode;
            }

            // Check if the node is added successfully to the linked list
            Debug.Assert(poHead != null);
        }

        override public BaseNode RemoveNodeFromFront()
        {
            Debug.Assert(poHead != null);
            // Copy the current head to return
            SingleLink pCurrentHead = poHead;

            // Change the header pointer to next node
            poHead = poHead.pNext;

            // Clear the prev and next values of the first node
            pCurrentHead.Clear();
            return pCurrentHead;
        }

        override public void RemoveNode(BaseNode pNodeToRemove)
        {
            // Make sure the header pointer and node to be deleted are not null
            Debug.Assert(poHead != null);
            Debug.Assert(pNodeToRemove != null);

            // Downcasting BaseNode to SingleLink is safe
            SingleLink pRemoveNode = (SingleLink)pNodeToRemove;

            if (pRemoveNode == poHead)
            {
                // If the node to be deleted is first node
                poHead = pRemoveNode.pNext;
            }
            else
            {
                // Track the previous and current node to remove the link
                SingleLink pCurrentNode = poHead;
                SingleLink pPrevNode = poHead;
                while (pCurrentNode != pRemoveNode)
                {
                    pPrevNode = pCurrentNode;
                    pCurrentNode = pCurrentNode.pNext;
                }

                // Remove the node
                pPrevNode.pNext = pRemoveNode.pNext;
            }

            // Clearing the linkage for the removed node from the list
            pRemoveNode.Clear();
        }

        public override BaseIterator FetchIterator()
        {
            // Resetting the Iterator to current head
            poIter.Reset(poHead);
            return poIter;
        }

        // Data
        public SingleLink poHead;
        public SingleLinkIterator poIter;
    }
}

// End of file