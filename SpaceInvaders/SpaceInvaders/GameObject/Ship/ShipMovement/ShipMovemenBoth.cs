using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMovemenBoth : ShipMovementState
    {
        // Overriding methods

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
        }
    }
}

// End of file