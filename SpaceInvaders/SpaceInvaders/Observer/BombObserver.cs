using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombObserver : CollisionObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("BombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            Bomb pBomb = (Bomb)this.pSubject.pGameObjA;
            pBomb.Reset();
        }
    }
}

// End of file
