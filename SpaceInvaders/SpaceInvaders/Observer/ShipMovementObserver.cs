using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMovementObserver : CollisionObserver
    {
        // Overriding Methods
        public override void Notify()
        {
            Ship pShip = ShipManager.GetShip();

            CategoryBumper pBumper = (CategoryBumper)this.pSubject.pGameObjA;

            if (pBumper.GetCategoryType() == CategoryBumper.BumperType.BumperRight)
            {
                pShip.SetState(ShipManager.MovementState.MovementLeft);
            }
            else if (pBumper.GetCategoryType() == CategoryBumper.BumperType.BumperLeft)
            {
                pShip.SetState(ShipManager.MovementState.MovementRight);
            }
            else
            {
                // Just Nothing
            }
        }
    }
}

// End of file
