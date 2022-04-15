using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public SpriteManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - SpriteManager
            poNodeToFind = new Sprite();
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new Sprite Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new SpriteManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);

            // Add a Null Sprite to Manager
            Image pImage = ImageManager.Find(Image.Name.HotPink);
            Debug.Assert(pImage != null);

            SpriteManager.Add(Sprite.Name.NullObject, Image.Name.HotPink, 0.0f, 0.0f, 0.0f, 0.0f, null);
        }

        public static void Destroy()
        {
            SpriteManager pSpriteMan = PrivGetInstance();

            Debug.Assert(pSpriteMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        private static SpriteManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        public static void Dump()
        {
            SpriteManager pSpriteMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pSpriteMan != null);

            // Calling the Base manager Dump to print
            pSpriteMan.BaseDump();
        }

        public static void Add(Sprite.Name name, Image.Name imageName, float x, float y, float width, float height, Azul.Color pSpriteColor = null)
        {
            Sprite pSprite = (Sprite)poInstance.BaseAddToFront();
            // Check the Sprite is not null
            Debug.Assert(pSprite != null);

            // Set the data to Sprite
            pSprite.SetValues(name, imageName, x, y, width, height, pSpriteColor);
        }

        public static Sprite AddSprite(Sprite.Name name, Image.Name imageName, float x, float y, float width, float height, Azul.Color pSpriteColor = null)
        {
            Sprite pSprite = (Sprite)poInstance.BaseAddToFront();
            // Check the Sprite is not null
            Debug.Assert(pSprite != null);

            // Set the data to Sprite
            pSprite.SetValues(name, imageName, x, y, width, height, pSpriteColor);
            return pSprite;
        }

        public static Sprite Find(Sprite.Name name)
        {
            poInstance.poNodeToFind.name = name;
            Sprite pSprite = (Sprite)poInstance.BaseFind(poInstance.poNodeToFind);

            // Return the found node
            return pSprite;
        }

        // Overriding methods
        protected override BaseNode derivedConstructNode()
        {
            // LTN - SpriteManager
            Sprite pSprite = new Sprite();
            Debug.Assert(pSprite != null);

            // Return a newly created Sprite
            return pSprite;
        }

        // Data
        private readonly Sprite poNodeToFind;
        private static SpriteManager poInstance = null;
    }
}

// End of file