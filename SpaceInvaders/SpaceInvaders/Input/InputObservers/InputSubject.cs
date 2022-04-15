using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputSubject
    {
        // Constructor
        public InputSubject()
        {
            // Initializing the Single LinkedList
            poSingleLinkMan = new SingleLinkManager();
            Debug.Assert(poSingleLinkMan != null);
        }

        ~InputSubject()
        {
        }

        // Methods
        public void Attach(InputObserver pObserver)
        {
            // pObserver shoudln't be null
            Debug.Assert(pObserver != null);

            pObserver.pSubject = this;

            // Add to front of the list
            poSingleLinkMan.AddNodeToFront(pObserver);
        }

        public void Notify()
        {
            BaseIterator pIterator = poSingleLinkMan.FetchIterator();
            InputObserver pObserverNode = (InputObserver)pIterator.Current();

            while (!pIterator.IsDone())
            {
                // Triger the observer
                pObserverNode.Notify();

                // Next observer
                pObserverNode = (InputObserver)pIterator.Next();
            }

        }

        public void Detach()
        {
        }

        // Data
        private SingleLinkManager poSingleLinkMan;
    }
}

// End of file