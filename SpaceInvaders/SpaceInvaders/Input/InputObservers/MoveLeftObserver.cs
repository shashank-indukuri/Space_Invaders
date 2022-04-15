using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveLeftObserver : InputObserver
    {
        public override void Notify()
        {
           // Debug.WriteLine("Move Left");
            Ship pShip = ShipManager.GetShip();
            pShip.MoveLeft();
        }
    }
}

// End of file