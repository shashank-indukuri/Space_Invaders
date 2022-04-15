using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBombObserver : CollisionObserver
    {
        // Constructor
        public RemoveBombObserver()
        {
            pBomb = null;
        }

        public RemoveBombObserver(RemoveBombObserver b)
        {
            Debug.Assert(b.pBomb != null);
            pBomb = b.pBomb;
        }

        // Overriding methods
        public override void Notify()
        {
            // Delete bomb
            //Debug.WriteLine("RemoveBombObserver: {0} {1}", pSubject.pGameObjA, pSubject.pGameObjB);

            // Cast to Bomb object
            if (pSubject.pGameObjA.name == GameObject.Name.Bomb || pSubject.pGameObjA.name == GameObject.Name.UFOBomb)
            {
                pBomb = (Bomb)pSubject.pGameObjA;
            }
            else
            {
                pBomb = (Bomb)pSubject.pGameObjB;
            }

            //Debug.WriteLine("RemoveBombObserver: --> delete missile {0}", pBomb);

            if (pBomb.bMarkForDelete == false)
            {
                pBomb.bMarkForDelete = true;

                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                // Add the object to Delay Manager
                DelayObjectManager.Add(pObserver);
            }

        }

        public override void Execute()
        {
            // Remove the GameObject
            pBomb.Remove();

            if (pBomb.name == GameObject.Name.UFOBomb)
            {
                TimerEventManager.Add(TimerEvent.Name.UFOBomb, (float)SpaceInvaders.pRandom.NextDouble(), new RandomUFOBombCommand());
            }
        }

        // Data
        private GameObject pBomb;
    }
}
