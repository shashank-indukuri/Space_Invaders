using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ReSpawnShip : BaseCommand
    {
        // Constructor
        public ReSpawnShip()
            : base()
        {

        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            Ship pShip = ShipManager.ActivateShip();
            pShip.SetState(ShipManager.MovementState.MovementBoth);
            pShip.SetState(ShipManager.ShootState.Ready);
        }
    }
}

// End of file
