using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionSubject
    {
        // Constructor
        public CollisionSubject()
        {
            pGameObjA = null;
            pGameObjB = null;

            // Initializing the Single LinkedList
            poSingleLinkMan = new SingleLinkManager();
            Debug.Assert(poSingleLinkMan != null);
        }

        ~CollisionSubject()
        {
            pGameObjA = null;
            pGameObjB = null;
        }

        // Methods
        public void Attach(CollisionObserver pObserver)
        {
            // pObserver shoudln't be null
            Debug.Assert(pObserver != null);

            pObserver.pSubject = this;

            // Add to front of the list
            poSingleLinkMan.AddNodeToFront(pObserver);
        }

        public void Notify()
        {
            // Fetch the Iterator
            BaseIterator pIterator = poSingleLinkMan.FetchIterator();
            CollisionObserver pObserverNode = (CollisionObserver)pIterator.Current();

            while (!pIterator.IsDone())
            {
                // Triger the observer
                pObserverNode.Notify();

                // Next observer
                pObserverNode = (CollisionObserver)pIterator.Next();
            }

        }

        public void Detach()
        {
        }


        // Data: ------------------------
        private SingleLinkManager poSingleLinkMan;
        public GameObject pGameObjA;
        public GameObject pGameObjB;
    }
}

// End of file