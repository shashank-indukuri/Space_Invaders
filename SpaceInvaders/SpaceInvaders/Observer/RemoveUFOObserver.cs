using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveUFOObserver : CollisionObserver
    {
        // Constructor
        public RemoveUFOObserver()
        {
            pUFO = null;
        }

        public RemoveUFOObserver(RemoveUFOObserver ufo)
        {
            Debug.Assert(ufo.pUFO != null);
            this.pUFO = ufo.pUFO;
        }

        // Overriding methods
        public override void Notify()
        {
            // Delete missile
            Debug.WriteLine("RemoveUFOObserver: {0} {1}", pSubject.pGameObjA, pSubject.pGameObjB);

            // Cast to UFO object
            pUFO = (UFO)pSubject.pGameObjB;

            Debug.WriteLine("RemoveUFOObserver: --> delete missile {0}", pUFO);

            if (pUFO.bMarkForDelete == false)
            {
                pUFO.bMarkForDelete = true;

                // TODO - reduce the new functions
                RemoveUFOObserver pObserver = new RemoveUFOObserver(this);
                // Add the object to Delay Manager
                DelayObjectManager.Add(pObserver);

                TimerEvent pTimerEvent = TimerEventManager.Find(TimerEvent.Name.UFOMove);
                TimerEventManager.Remove(pTimerEvent);
                TimerEventManager.Add(TimerEvent.Name.LaunchUFO, pRandom.Next(25, 70), SpaceInvaders.pLaunchUFO);
            }

        }

        public override void Execute()
        {
            // Remove the GameObject
            pUFO.Remove();
        }


        // Data
        private GameObject pUFO;
        private Random pRandom = new Random();
    }
}

// End of file