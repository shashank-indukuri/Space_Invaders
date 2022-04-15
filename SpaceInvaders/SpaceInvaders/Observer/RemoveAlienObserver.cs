using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveAlienObserver : CollisionObserver
    {
        // Constructor
        public RemoveAlienObserver()
        {
            pAlien = null;
        }

        public RemoveAlienObserver(RemoveAlienObserver a)
        {
            Debug.Assert(a.pAlien != null);
            pAlien = a.pAlien;
        }

        // Overriding methods
        public override void Notify()
        {
            // Delete Alien
            //Debug.WriteLine("RemoveAlienObserver: {0} {1}", this.pSubject.pGameObjA, this.pSubject.pGameObjB);

            pAlien = (CategoryAlien)pSubject.pGameObjB;

            //Debug.WriteLine("RemoveAlienObserver: --> delete missile {0}", pAlien);

            if (pAlien.bMarkForDelete == false)
            {
                pAlien.bMarkForDelete = true;

                TimerEvent pSquidEvent = TimerEventManager.Find(TimerEvent.Name.Squid);
                TimerEvent pCrabEvent = TimerEventManager.Find(TimerEvent.Name.Crab);
                TimerEvent pOctopusEvent = TimerEventManager.Find(TimerEvent.Name.Octopus);
                TimerEvent pMoveEvent = TimerEventManager.Find(TimerEvent.Name.Move);
                TimerEvent pMarchEvent = TimerEventManager.Find(TimerEvent.Name.March);

                float timeToUpdate = pSquidEvent.GetDeltaTime() - 0.005f;
                if (timeToUpdate > 0.0)
                {
                    pSquidEvent.UpdateDeltaTime(timeToUpdate);
                    pCrabEvent.UpdateDeltaTime(timeToUpdate);
                    pOctopusEvent.UpdateDeltaTime(timeToUpdate);
                    pMoveEvent.UpdateDeltaTime(timeToUpdate);
                    pMarchEvent.UpdateDeltaTime(timeToUpdate);
                }

                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);

                // Add the object to Delay Manager
                DelayObjectManager.Add(pObserver);
            }
        }

  
        public override void Execute()
        {
            // Debug.WriteLine(" Alien {0}  parent {1}", this.pAlien, this.pAlien.pParent);
            GameObject pA = (GameObject)this.pAlien;
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
                TimerEventManager.Add(TimerEvent.Name.ReStartNextLevel, 1.0f, new ReStartNextLevelCommand());
            }

        }

        // Data
        private GameObject pAlien;
    }
}

// End of file
