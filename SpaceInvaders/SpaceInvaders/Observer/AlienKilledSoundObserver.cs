using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienKilledSoundObserver : CollisionObserver
    {

        // Overriding Methods
        public override void Notify()
        {
            //Debug.WriteLine(" SoundObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            IrrKlang.ISoundEngine pSoundEngine = SoundManager.GetSoundEngine();
            pSoundEngine.SoundVolume = 0.2f;

            // Play the alien killed sound
            IrrKlang.ISound pSnd = pSoundEngine.Play2D("invaderkilled.wav");
        }
    }
}

// End of file
