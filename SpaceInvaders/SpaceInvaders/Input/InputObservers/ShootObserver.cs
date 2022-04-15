using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShootObserver : InputObserver
    {
        public override void Notify()
        {
            // Debug.WriteLine("Shoot...");
            Ship pShip = ShipManager.GetShip();
            pShip.ShootMissile();
        }
    }
}

// End of file