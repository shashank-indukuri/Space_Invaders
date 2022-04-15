using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteBoxManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public SpriteBoxManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - SpriteBoxManager
            poNodeToFind = new SpriteBox();
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new SpriteBox Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new SpriteBoxManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);

            // Add the Null SpriteBox to the Manager
            SpriteBoxManager.Add(SpriteBox.Name.NullObject, 0, 0, 0, 0, null);
        }

        public static void Destroy()
        {
            SpriteBoxManager pSpriteBoxMan = PrivGetInstance();

            Debug.Assert(pSpriteBoxMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        private static SpriteBoxManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        public static void Dump()
        {
            SpriteBoxManager pSpriteBoxMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pSpriteBoxMan != null);

            // Calling the Base manager Dump to print
            pSpriteBoxMan.BaseDump();
        }

        public static SpriteBox Add(SpriteBox.Name name, float x, float y, float width, float height, Azul.Color pSpriteBoxColor = null)
        {
            SpriteBox pSpriteBox = (SpriteBox)poInstance.BaseAddToFront();
            // Check the Sprite is not null
            Debug.Assert(pSpriteBox != null);

            // Set the data to Sprite
            pSpriteBox.SetValues(name, x, y, width, height, pSpriteBoxColor);
            return pSpriteBox;
        }

        public static SpriteBox Find(SpriteBox.Name name)
        {
            poInstance.poNodeToFind.name = name;
            SpriteBox pSpriteBox = (SpriteBox)poInstance.BaseFind(poInstance.poNodeToFind);

            // Return the found node
            return pSpriteBox;
        }

        public static void Update()
        {
            SpriteBoxManager pSpriteBoxMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pSpriteBoxMan != null);

            // Get the iterator reference
            BaseIterator pIterator = pSpriteBoxMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            SpriteBox pSpriteBox = (SpriteBox)pIterator.First();

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                // Update the SpriteBox
                pSpriteBox.Update();

                // Go to the next node
                pSpriteBox = (SpriteBox)pIterator.Next();
            }
        }

        // Overriding methods
        protected override BaseNode derivedConstructNode()
        {
            // LTN - SpriteBoxManager
            SpriteBox pSpriteBox = new SpriteBox();
            Debug.Assert(pSpriteBox != null);

            // Return a newly created SpriteBox
            return pSpriteBox;
        }

        // Data
        private readonly SpriteBox poNodeToFind;
        private static SpriteBoxManager poInstance = null;
    }
}

// End of file