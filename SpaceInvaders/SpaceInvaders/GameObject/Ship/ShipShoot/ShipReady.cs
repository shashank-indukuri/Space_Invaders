using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipReady : ShipShootState
    {
        // Overriding methods
        public override void ShootMissile(Ship pShip)
        {
            // Acitvate the Missile
            Missile pMissile = ShipManager.ActivateMissile();

            // Set the position and missile
            pMissile.SetPosition(pShip.x, pShip.y + 20);
            //pMissile.SetMissile(true);

            // Change the state
            pShip.SetState(ShipManager.ShootState.MissileFlying);

            IrrKlang.ISoundEngine pSoundEngine = SoundManager.GetSoundEngine();
            pSoundEngine.SoundVolume = 0.2f;
            IrrKlang.ISound pSnd = pSoundEngine.Play2D("shoot.wav");
        }
    }
}

// End of file