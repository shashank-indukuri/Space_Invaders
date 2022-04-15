using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShipShootState
    {
        public abstract void ShootMissile(Ship pShip);
    }
}

// End of file