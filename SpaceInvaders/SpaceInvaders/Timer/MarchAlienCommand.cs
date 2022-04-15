using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MarchAlienCommand : BaseCommand
    {
        // Constructor
        public MarchAlienCommand()
            : base()
        {
            // Initialize the DoubleLink Manager
            // LTN - TimerEventManager
            poDoubleLinkMan = new DoubleLinkManager();
            Debug.Assert(poDoubleLinkMan != null);

            // Fecth the Iterator
            pIterator = poDoubleLinkMan.FetchIterator();
            Debug.Assert(pIterator != null);
        }

        // Method
        public void Link(Sound.Source name)
        {
            // Find the Sound
            Sound pSound = SoundManager.Find(name);
            Debug.Assert(pSound != null);

            // Create a new link
            // LTN - TimerEventManager
            SoundNode pSoundNode = new SoundNode(pSound);
            Debug.Assert(pSoundNode != null);

            // Add the new Sound Node to the front of the March Alien Sound Animation
            poDoubleLinkMan.AddNodeToFront(pSoundNode);

            // Update the current iterator
            pIterator = poDoubleLinkMan.FetchIterator();
        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            // Go to next sound
            SoundNode pSoundNode = (SoundNode)pIterator.Current();
            Debug.Assert(pSoundNode != null);

            if (pIterator.Next() == null)
            {
                // Loop it 
                pIterator.First();
            }

            IrrKlang.ISoundEngine pSoundEngine = SoundManager.GetSoundEngine();
            pSoundEngine.SoundVolume = 0.2f;

            IrrKlang.ISound pSnd = pSoundEngine.Play2D(pSoundNode.pSound.soundVader, false, false, false);

            // Add it back to the timer
            TimerEventManager.Add(TimerEvent.Name.March, deltaTime, this);
        }

        // Data
        public DoubleLinkManager poDoubleLinkMan;
        public BaseIterator pIterator;
    }
}

// End of file