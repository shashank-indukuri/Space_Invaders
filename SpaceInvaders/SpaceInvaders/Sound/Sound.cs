using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Sound : DoubleLink
    {
        // Enum
        public enum Source
        {
            FastInvader1,
            FastInvader2,
            FastInvader3,
            FastInvader4,
            Explosion,
            Player2,

            Uninitialized
        }

        // Constructor
        public Sound()
            : base()
        {
            ClearValues();
        }

        // Methods
        public void SetValues(Source source, String fileName)
        {
            this.source = source;
            soundVader = SoundManager.GetSoundEngine().AddSoundSourceFromFile(fileName);
        }

        // Overriding Methods
        public override void ClearValues()
        {
            source = Source.Uninitialized;
        }

        public override bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            Sound pSound = (Sound)pNodeToCompare;

            if (source == pSound.source)
            {
                return true;
            }
            return false;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", source, GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", source, GetHashCode());

            base.Dump();
        }

        // Data
        public Source source;
        public IrrKlang.ISoundSource soundVader;
    }
}

// End of file
