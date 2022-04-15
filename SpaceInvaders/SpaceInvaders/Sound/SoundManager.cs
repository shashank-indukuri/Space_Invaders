using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public SoundManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - SoundManager
            poNodeToFind = new Sound();
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(psInstance == null);

            // Creating the new Sound Manager
            if (psInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                psInstance = new SoundManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }

        public static void Destroy()
        {
            SoundManager pSoundMan = PrivGetInstance();

            Debug.Assert(pSoundMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        private static SoundManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        public static void Dump()
        {
            SoundManager pSoundMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pSoundMan != null);

            // Calling the Base manager Dump to print
            pSoundMan.BaseDump();
        }

        public static void Add(Sound.Source name, String fileName)
        {
            Sound pSound = (Sound)psInstance.BaseAddToFront();
            // Check the Sound is not null
            Debug.Assert(pSound != null);

            // Set the data to Sound
            pSound.SetValues(name ,fileName);
        }

        public static Sound Find(Sound.Source name)
        {
            psInstance.poNodeToFind.source = name;
            Sound pSound = (Sound)psInstance.BaseFind(psInstance.poNodeToFind);

            // Return the found node
            return pSound;
        }

        public static IrrKlang.ISoundEngine GetSoundEngine()
        {
            SoundManager pSoundMan = PrivGetInstance();

            return pSoundMan.pSoundEngine;
        }

        // Overriding Method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - SoundManager
            Sound pSound = new Sound();
            Debug.Assert(pSound != null);

            // Return a newly created Sound
            return pSound;
        }

        // Data
        private readonly Sound poNodeToFind;
        private static SoundManager psInstance = null;
        private IrrKlang.ISoundEngine pSoundEngine = new IrrKlang.ISoundEngine();
    }
}

// End of files
