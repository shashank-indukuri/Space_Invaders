using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGridObserver : CollisionObserver
    {
        // Overriding Methods
        public override void Notify()
        {
            //Debug.WriteLine("AlienGridObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // Move the composite AlienGroup
            AlienGroup pAlienGroup = (AlienGroup)this.pSubject.pGameObjA;

            float delta = pAlienGroup.GetDelta();
            pAlienGroup.SetDelta(-delta);
            pAlienGroup.MoveGrid();
        }
    }
}

// End of file