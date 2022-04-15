using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class TextureManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public TextureManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - Owned by TextureManager
            poNodeToFind = new Texture();
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new Texture Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new TextureManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);

            // Adding the HotPink as a default texture
            Texture pHotPinkTexture = Add(Texture.Name.HotPink, "HotPink.tga");
            Debug.Assert(pHotPinkTexture != null);
        }

        public static void Destroy()
        {
            TextureManager pTextureMan = PrivGetInstance();

            Debug.Assert(pTextureMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        private static TextureManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        public static void Dump()
        {
            TextureManager pTextureMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pTextureMan != null);

            // Calling the Base manager Dump to print
            pTextureMan.BaseDump();

        }

        public static Texture Add(Texture.Name name, string textureName)
        {
            TextureManager pTextureManager = PrivGetInstance();

            Texture pTexture = (Texture)pTextureManager.BaseAddToFront();
            // Check the Texture is not null
            Debug.Assert(pTexture != null);

            // Set the data to Texture
            pTexture.SetValues(name, textureName);
            return pTexture;
        }

        public static Texture Find(Texture.Name name)
        {
            poInstance.poNodeToFind.name = name;
            Texture pTexture = (Texture)poInstance.BaseFind(poInstance.poNodeToFind);

            // Return the found node
            return pTexture;
        }

        protected override BaseNode derivedConstructNode()
        {
            // LTN - TextureManager
            Texture pTexture = new Texture();
            Debug.Assert(pTexture != null);

            // Return a newly created Texture
            return pTexture;
        }

        // Data
        private readonly Texture poNodeToFind;
        private static TextureManager poInstance;
    }
}

// End of file