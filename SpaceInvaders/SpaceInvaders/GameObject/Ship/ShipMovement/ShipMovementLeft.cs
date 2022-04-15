using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMovementLeft : ShipMovementState
    {
        // Overriding methods

        public override void MoveRight(Ship pShip)
        {
            // Just Nothing
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
            pShip.SetState(ShipManager.MovementState.MovementBoth);
        }
    }
}

// End of file