using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBrickObserver : CollisionObserver
    {
        // Constructor
        public RemoveBrickObserver()
        {
            pBrick = null;
        }

        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            Debug.Assert(b.pBrick != null);
            pBrick = b.pBrick;
        }

        // Overriding methods
        public override void Notify()
        {
            // Delete Brick
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pGameObjA, this.pSubject.pGameObjB);

            pBrick = (CategoryShield)pSubject.pGameObjB;

            //Debug.WriteLine("RemoveBrickObserver: --> delete missile {0}", pBrick);

            if (pBrick.bMarkForDelete == false)
            {
                pBrick.bMarkForDelete = true;

                RemoveBrickObserver pObserver = new RemoveBrickObserver(this);
                // Add the object to Delay Manager
                DelayObjectManager.Add(pObserver);
            }
        }

        public override void Execute()
        {
            // Debug.WriteLine(" Brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pBrick;
            GameObject pB = (GameObject)ForwardCompositeIterator.GetParentNode(pA);
            GameObject pC = (GameObject)ForwardCompositeIterator.GetParentNode(pB);

            // Root shouldn't be deleted

            // Alien
            if (pA.GetNumOfChildren() == 0)
            {
                pA.Remove();
            }

            // Column 
            if (pB.GetNumOfChildren() == 0)
            {
                pB.Remove();
            }

            // Grid 
            if (pC.GetNumOfChildren() == 0)
            {
                pC.Remove();
            }

        }

        // Data
        private GameObject pBrick;
    }
}

// End of file
