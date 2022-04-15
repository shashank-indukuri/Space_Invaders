using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BaseManager
    {
        public BaseManager(BaseType poActiveList, BaseType poReserveList, int InitialNumReserved = 5, int DeltaGrow = 2)
        {
            // Validating the initial conditions for the parameters
            Debug.Assert(poActiveList != null);
            Debug.Assert(poReserveList != null);
            Debug.Assert(InitialNumReserved >= 0);
            Debug.Assert(DeltaGrow > 0);

            // Initializing the properties
            mDeltaGrow = DeltaGrow;
            mTotalNumNodes = 0;
            mNumActive = 0;
            mNumReserved = 0;
            this.poActiveList = poActiveList;
            this.poReserveList = poReserveList;

            // Invoking the method to create InitialNumReserved Node and add to Reserve List
            PrivFillReserveList(InitialNumReserved);
        }

        abstract protected BaseNode derivedConstructNode();

        private void PrivFillReserveList(int numberOfNodes)
        {
            // the number of nodes should be greater than zero to fill the Reserve List
            Debug.Assert(numberOfNodes >= 0);

            mTotalNumNodes += numberOfNodes;
            mNumReserved += numberOfNodes;

            int index = 0;
            while (index < numberOfNodes)
            {
                BaseNode pNode = derivedConstructNode();
                Debug.Assert(pNode != null);
                // Add the newly created node to the front of the reserve list
                poReserveList.AddNodeToFront(pNode);
                index += 1;
            }
        }

        public BaseIterator BaseFetchIterator()
        {
            return poActiveList.FetchIterator();
        }

        public void BaseSetReserveList(int InitialNumReserved, int DeltaGrow)
        {
            // Updating the Delta Grow
            mDeltaGrow = DeltaGrow;

            // If the Initial reserve number is greater than current nodes in reserve
            if (InitialNumReserved > mNumReserved)
            {
                // Fill the reserve list with extra nodes
                this.PrivFillReserveList(InitialNumReserved - mNumReserved);
            }
        }
        public BaseNode BaseAddToFront(float priority = 0)
        {
            BaseIterator pIterator = poReserveList.FetchIterator();
            // Iterator is not null
            Debug.Assert(pIterator != null);

            // Checking if the Reserve list is empty
            if (pIterator.First() == null)
            {
                PrivFillReserveList(mDeltaGrow);
            }

            BaseNode pRemovedNode = poReserveList.RemoveNodeFromFront();

            // Check if the removed node is not null
            // and successfully removed the linkage from reserved list
            Debug.Assert(pRemovedNode != null);

            pRemovedNode.ClearValues();
            mNumActive += 1;
            mNumReserved -= 1;

            if (priority == 0)
            {
                // Move the removed node to front of the active list
                poActiveList.AddNodeToFront(pRemovedNode);
            }
            else
            {
                poActiveList.AddNodeOnPriority(pRemovedNode, priority);
            }

            return pRemovedNode;
        }
        public BaseNode BaseAddToEnd()
        {
            BaseIterator pIterator = poReserveList.FetchIterator();
            // Iterator is not null
            Debug.Assert(pIterator != null);

            // Checking if the Reserve list is empty
            if (pIterator.First() == null)
            {
                PrivFillReserveList(mDeltaGrow);
            }

            BaseNode pRemovedNode = poReserveList.RemoveNodeFromFront();

            // Check if the removed node is not null
            // and successfully removed the linkage from reserved list
            Debug.Assert(pRemovedNode != null);


            pRemovedNode.ClearValues();
            mNumActive += 1;
            mNumReserved -= 1;

            // Move the removed node to end of the active list
            poActiveList.AddNodeToEnd(pRemovedNode);

            return pRemovedNode;
        }

        public BaseNode BaseFind(BaseNode pNodeToFind)
        {
            // Make sure the node is not null
            Debug.Assert(pNodeToFind != null);

            BaseIterator pIterator = poActiveList.FetchIterator();

            BaseNode pNode = pIterator.First();

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                if (pNode.Compare(pNodeToFind))
                {
                    // Found the matched node and break the loop
                    break;
                }
                pNode = pIterator.Next();
            }

            // Return the matched node
            return pNode;
        }

        public void BaseRemove(BaseNode pNodeToRemove)
        {
            // The node to be deleted shouldn't be null
            Debug.Assert(pNodeToRemove != null);

            // Remove the node the active list
            poActiveList.RemoveNode(pNodeToRemove);

            // Clear the values of the removed node
            pNodeToRemove.ClearValues();

            // Finally add the node to the reserve list
            poReserveList.AddNodeToFront(pNodeToRemove);

            // Update the active and reserve list count
            mNumActive -= 1;
            mNumReserved += 1;
        }

        public void BaseDump()
        {
            Debug.WriteLine("   --- " + ToString() + " Begin ---\n");

            Debug.WriteLine("         mDeltaGrow: {0} ", mDeltaGrow);
            Debug.WriteLine("     mTotalNumNodes: {0} ", mTotalNumNodes);
            Debug.WriteLine("       mNumReserved: {0} ", mNumReserved);
            Debug.WriteLine("         mNumActive: {0} \n", mNumActive);

            BaseIterator pItActive = poActiveList.FetchIterator();
            Debug.Assert(pItActive != null);

            BaseNode pNodeActive = pItActive.First();
            if (pNodeActive == null)
            {
                Debug.WriteLine("    Active Head: null");
            }
            else
            {
                Debug.WriteLine("    Active Head: ({0})", pNodeActive.GetHashCode());
            }

            BaseIterator pItReserve = poReserveList.FetchIterator();
            Debug.Assert(pItReserve != null);

            BaseNode pNodeReserve = pItReserve.First();
            if (pNodeReserve == null)
            {
                Debug.WriteLine("   Reserve Head: null\n");
            }
            else
            {
                Debug.WriteLine("   Reserve Head: ({0})\n", pNodeReserve.GetHashCode());
            }

            Debug.WriteLine("   ------ Active List: -----------\n");


            int i = 0;
            BaseNode pData = pItActive.First();
            while (!pItActive.IsDone())
            {
                Debug.WriteLine("   {0}: -------------", i);
                pData.Dump();
                i++;
                pData = pItActive.Next();
            }

            Debug.WriteLine("");
            Debug.WriteLine("   ------ Reserve List: ----------\n");

            i = 0;
            pData = pItReserve.First();
            while (!pItReserve.IsDone())
            {
                Debug.WriteLine("   {0}: -------------", i);
                pData.Dump();
                i++;
                pData = pItReserve.Next();
            }

            Debug.WriteLine("   --- " + ToString() + " End ---\n");
        }

        // Data
        public int mDeltaGrow;
        public int mTotalNumNodes;
        public int mNumReserved;
        public int mNumActive;
        public BaseType poActiveList;
        public BaseType poReserveList;
    }
}

// End of file