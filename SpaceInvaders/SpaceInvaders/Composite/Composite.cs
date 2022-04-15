using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Composite : GameObject
    {
        // Constructor
        public Composite()
            : base(Type.Composite, Name.NullObject, Sprite.Name.NullObject)
        {
            // LTN - GameObjectNodeManager
            poDoubleLinkMan = new DoubleLinkManager();
        }

        public Composite(Name name, Sprite.Name spriteName)
            : base(Component.Type.Composite, name, spriteName)
        {
            // LTN - GameObjectNodeManager
            poDoubleLinkMan = new DoubleLinkManager();
        }

        // Methods

        public Component GetHead()
        {
            Debug.Assert(poDoubleLinkMan != null);

            // return the poHead
            Component pHeader = (Component)poDoubleLinkMan.poHead;
            return pHeader;
        }

        // Overriding methods

        public override int GetNumOfChildren()
        {
            // Count the children
            int count = 0;

            // Get the iterator
            BaseIterator pIterator = poDoubleLinkMan.FetchIterator();
            Debug.Assert(pIterator != null);

            // Read the head
            GameObject pCurrentNode = (GameObject)pIterator.First();

            // Walk through the nodes
            while (!pIterator.IsDone())
            {
                // Increment
                count++;
                pCurrentNode = (GameObject)pIterator.Next();
            }
            // return the children
            return count;
        }

        public override GameObject GetChild(int location)
        {
            // Count the children
            int count = 0;

            GameObject pMatch = null;

            // Get the iterator
            BaseIterator pIterator = poDoubleLinkMan.FetchIterator();
            Debug.Assert(pIterator != null);

            // Read the head
            GameObject pCurrentNode = (GameObject)pIterator.First();

            // Walk through the nodes
            while (!pIterator.IsDone())
            {
                if (count == location)
                {
                    pMatch = pCurrentNode;
                }

                count += 1;
                pCurrentNode = (GameObject)pIterator.Next();
            }

            // return the children
            return pMatch;
        }

        public override void Resurrect()
        {
            // check the DLinkMan
            Debug.Assert(this.poDoubleLinkMan.poHead == null);

            base.Resurrect();
        }

        public override void Add(Component pComp)
        {
            Debug.Assert(pComp != null);

            // Adding the component to the front of the list
            poDoubleLinkMan.AddNodeToFront(pComp);

            pComp.pParent = this;

        }

        public override void Remove(Component pComp)
        {
            Debug.Assert(pComp != null);

            poDoubleLinkMan.RemoveNode(pComp);
        }

        public override void Print()
        {
            Debug.WriteLine("\nComposite: {0}  ", this);

            BaseIterator pIterator = poDoubleLinkMan.FetchIterator();
            Debug.Assert(pIterator != null);

            GameObject pGameObj = (GameObject)pIterator.First();

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                // Print the details of the current node
                pGameObj.Print();

                // Go to the next node
                pGameObj = (GameObject)pIterator.Next();
            }
        }

        public override void DumpComponent()
        {
            // Print the details of current node and its parent
            if (ForwardCompositeIterator.GetParentNode(this) != null)
            {
                Debug.WriteLine(" Game Object Name:({0}) parent:{1} <---- Composite", this.GetHashCode(), ForwardCompositeIterator.GetParentNode(this).GetHashCode());
            }
            else
            {
                Debug.WriteLine(" Game Object Name:({0}) parent:null <---- Composite", this.GetHashCode());
            }
        }

        public override void ClearValues()
        {
            // Not allowed to clear the values
            Debug.Assert(false);
        }

        // Data
        public DoubleLinkManager poDoubleLinkMan;
    }
}

// End of file
