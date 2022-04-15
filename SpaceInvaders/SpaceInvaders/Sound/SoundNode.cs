using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SoundNode : DoubleLink
    {
        // Constructor
        public SoundNode(Sound pSound)
            : base()
        {
            // Checking if the pSound is not null
            Debug.Assert(pSound != null);

            this.pSound = pSound;
        }


        // Overriding methods
        public override void ClearValues()
        {
            pSound = null;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   ({0}) node", GetHashCode());

            // Data:
            Debug.WriteLine("   pSound: {0} ({1})", pSound.GetName(), pSound.GetHashCode());

            base.Dump();
        }

        // Data
        public Sound pSound;
    }
}

// End of files