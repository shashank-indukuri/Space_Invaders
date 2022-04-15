using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMovementRight : ShipMovementState
    {
        // Overriding methods

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
            pShip.SetState(ShipManager.MovementState.MovementBoth);
        }

        public override void MoveLeft(Ship pShip)
        {
            // Just Nothing
        }
    }
}

// End of file
