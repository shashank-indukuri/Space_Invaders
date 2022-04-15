using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShipObserver : CollisionObserver
    {
        // Constructor
        public RemoveShipObserver()
        {
            pShip = null;
            pRandom = SpaceInvaders.pRandom;
        }

        public RemoveShipObserver(RemoveShipObserver pShip)
        {
            Debug.Assert(pShip.pShip != null);
            this.pShip = pShip.pShip;

            pRandom = SpaceInvaders.pRandom;
        }

        // Overriding methods
        public override void Notify()
        {
            // Delete missile
            Debug.WriteLine("RemoveShipObserver: {0} {1}", pSubject.pGameObjA, pSubject.pGameObjB);

            // Cast to UFO object
            pShip = (Ship)pSubject.pGameObjB;

            Debug.WriteLine("RemoveShipObserver: --> delete missile {0}", pShip);

            if (pShip.bMarkForDelete == false)
            {
                pShip.bMarkForDelete = true;

                PlayerManager.UpdatePlayerState(true);

                RemoveShipObserver pObserver = new RemoveShipObserver(this);
                // Add the object to Delay Manager
                DelayObjectManager.Add(pObserver);
                TimerEventManager.PauseAnimation(2.0f);
            }

        }

        public override void Execute()
        {
            // Remove the GameObject
            pShip.Remove();
        }

        // Data
        private GameObject pShip;
        private Random pRandom;
    }
}

// End of file