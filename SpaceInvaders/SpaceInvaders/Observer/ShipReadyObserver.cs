using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipReadyObserver : CollisionObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipManager.GetShip();
            pShip.SetState(ShipManager.ShootState.Ready);

        }
    }
}

// End of file