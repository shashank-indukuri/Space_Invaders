using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipExplosionObserver : CollisionObserver
    {
        // overriding Methods
        public override void Notify()
        {
            //Debug.WriteLine(" SoundObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            IrrKlang.ISoundEngine pSoundEngine = SoundManager.GetSoundEngine();
            pSoundEngine.SoundVolume = 0.2f;
            IrrKlang.ISound pSnd = pSoundEngine.Play2D("explosion.wav");
        }
    }
}

// End of file