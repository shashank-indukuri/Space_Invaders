using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    abstract public class ShipMovementState
    {
        public abstract void MoveRight(Ship pShip);
        public abstract void MoveLeft(Ship pShip);
    }
}

// End of file