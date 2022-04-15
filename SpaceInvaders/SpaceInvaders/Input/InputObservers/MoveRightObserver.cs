﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveRightObserver : InputObserver
    {
        public override void Notify()
        {
            // Debug.WriteLine("Move Right");
            Ship pShip = ShipManager.GetShip();
            pShip.MoveRight();
        }
    }
}

// End of file