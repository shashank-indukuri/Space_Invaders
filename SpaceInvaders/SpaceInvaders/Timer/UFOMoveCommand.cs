using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOMoveCommand : BaseCommand
    {
        // Constructor
        public UFOMoveCommand(float delta)
            : base()
        {
            this.delta = delta;
        }

        public override void Execute(float deltaTime)
        {
            // Find the ufo group
            UFOGroup pUFOGroup = (UFOGroup)GameObjectNodeManager.Find(GameObject.Name.UFOGroup);

            // Check if the ufo group has a child
            if (pUFOGroup.GetNumOfChildren() != 0)
            {
                IrrKlang.ISoundEngine pSoundEngine = SoundManager.GetSoundEngine();

                // Play the sound if it is not playing
                if (!pSoundEngine.IsCurrentlyPlaying("ufo_lowpitch.wav"))
                {
                    IrrKlang.ISound pSnd = pSoundEngine.Play2D("ufo_lowpitch.wav");
                }
            }

            // Move the ufo
            pUFOGroup.MoveUFO(delta);


            // Add the event back to the timer
            TimerEventManager.Add(TimerEvent.Name.UFOMove, deltaTime, this);
        }

        // Data
        private float delta;
    }
}

// End of file