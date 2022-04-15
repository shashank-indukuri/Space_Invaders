using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRemoveMissileObserver : CollisionObserver
    {
        // Constructor
        public ShipRemoveMissileObserver()
        {
            pMissile = null;
        }

        public ShipRemoveMissileObserver(ShipRemoveMissileObserver m)
        {
            Debug.Assert(m.pMissile != null);
            pMissile = m.pMissile;
        }

        // Overriding methods
        public override void Notify()
        {
            // Delete missile
             Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", pSubject.pGameObjA, pSubject.pGameObjB);

            // Cast to Missile object
            pMissile = (Missile)pSubject.pGameObjA;

            Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);

            if (pMissile.bMarkForDelete == false)
            {
                pMissile.bMarkForDelete = true;

                // TODO - reduce the new functions
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver(this);
                // Add the object to Delay Manager
                DelayObjectManager.Add(pObserver);
            }

        }

        public override void Execute()
        {
            // Remove the GameObject
            pMissile.Remove();
        }

        // Data
        private GameObject pMissile;
    }
}

// End of file
