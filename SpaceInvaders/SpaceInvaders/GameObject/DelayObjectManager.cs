using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DelayObjectManager
    {
        // Constructor
        private DelayObjectManager()
        {
            this.poSLinkMan = new SingleLinkManager();
            Debug.Assert(this.poSLinkMan != null);
        }

        // Static methods
        private static DelayObjectManager PrivGetInstance()
        {
            // Create an instance
            if (poInstance == null)
            {
                poInstance = new DelayObjectManager();
            }

            // Only one instance can be created
            Debug.Assert(poInstance != null);

            return poInstance;
        }
        public static void Add(CollisionObserver pObserver)
        {
            Debug.Assert(pObserver != null);
            DelayObjectManager pDelayManager = PrivGetInstance();

            // Add the node to front
            pDelayManager.poSLinkMan.AddNodeToFront(pObserver);
        }

        public static void Process()
        {
            DelayObjectManager pDelayManager = PrivGetInstance();

            // Fetch the Iterator
            BaseIterator pIterator = pDelayManager.poSLinkMan.FetchIterator();
            CollisionObserver pNode = (CollisionObserver)pIterator.First();

            // Walk through the list
            while (!pIterator.IsDone())
            {
                // Trigger the listener
                pNode.Execute();

                pNode = (CollisionObserver)pIterator.Next();
            }

            CollisionObserver pTmp = null;
            pIterator = pDelayManager.poSLinkMan.FetchIterator();
            pNode = (CollisionObserver)pIterator.First();

            while (!pIterator.IsDone())
            {
                pTmp = pNode;
                pNode = (CollisionObserver)pIterator.Next();
                // remove the node from linked list
                pDelayManager.poSLinkMan.RemoveNode(pTmp);
            }
        }

        // Data
        private SingleLinkManager poSLinkMan;
        private static DelayObjectManager poInstance = null;
    }
}

// End of file