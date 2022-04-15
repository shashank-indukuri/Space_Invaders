using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ForwardCompositeIterator : BaseCompositeIterator
    {
        public ForwardCompositeIterator(Component pHead)
        {
            // pHead should be composite and not null
            Debug.Assert(pHead != null);
            Debug.Assert(pHead.type == Component.Type.Composite);

            // Initializing the current and first pointers
            pCurrent = pHead;
            pFirst = pHead;
        }

        // Overriding methods
        public override Component First()
        {
            return pFirst;
        }

        public override Component Current()
        {
            return pCurrent;
        }

        public override bool IsDone()
        {
            if (pCurrent == null)
            {
                return true;
            }
            return false;
        }

        public override Component Next()
        {
            Debug.Assert(pCurrent != null);

            Component pNextNode = pCurrent;

            // getting the neighbours of the current node with helper methods
            Component pChild = GetChildNode(pNextNode);
            Component pSibling = GetSiblingNode(pNextNode);
            Component pParent = GetParentNode(pNextNode);

            // Start the DFS
            pNextNode = this.PrivNextStep(pNextNode, pParent, pChild, pSibling);

            pCurrent = pNextNode;

            return pNextNode;
        }

        // Helper methods to perfomr DFS on a Composite Tree
        private Component PrivNextStep(Component pCurrentNode, Component pParent, Component pChild, Component pSibling)
        {
            pCurrentNode = null;
            // If current node is a composite, navigate to child
            if (pChild != null)
            {
                pCurrentNode = pChild;
            }
            else
            {
                if (pSibling != null)
                {
                    pCurrentNode = pSibling;
                }
                else
                {
                    // No siblings, childern and navigate to upper level

                    while (pParent != null)
                    {
                        pCurrentNode = GetSiblingNode(pParent);
                        if (pCurrentNode != null)
                        {
                            // Found the node
                            break;
                        }
                        else
                        {
                            // Found the parent
                            pParent = GetParentNode(pParent);
                        }
                    }
                }
            }

            return pCurrentNode;
        }

        public static Component GetParentNode(Component pCurrentNode)
        {
            Debug.Assert(pCurrentNode != null);

            // return parent node
            return pCurrentNode.pParent;

        }
        public static Component GetChildNode(Component pCurrentNode)
        {
            Debug.Assert(pCurrentNode != null);

            Component pChild;
            // if composite, return child
            if (pCurrentNode.type == Component.Type.Composite)
            {
                pChild = ((Composite)pCurrentNode).GetHead();
            }
            // return null
            else
            {
                pChild = null;
            }

            return pChild;
        }
        public static Component GetSiblingNode(Component pCurrentNode)
        {
            Debug.Assert(pCurrentNode != null);

            // return the next sibiling node
            return (Component)pCurrentNode.pNext;
        }

        // Data
        private Component pCurrent;
        private Component pFirst;
    }
}

// End of file
