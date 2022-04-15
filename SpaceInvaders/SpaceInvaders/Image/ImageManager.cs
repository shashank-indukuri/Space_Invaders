using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public ImageManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - ImageManager
            poNodeToFind = new Image();
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new Image Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new ImageManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);

            Texture pHotPinkTexture = TextureManager.Find(Texture.Name.HotPink);
            Debug.Assert(pHotPinkTexture != null);

            // Adding default images
            Add(Image.Name.HotPink, Texture.Name.HotPink, 0, 0, 128, 128);

            Add(Image.Name.HotPink, Texture.Name.HotPink, 0, 0, 0, 0);
        }

        public static void Destroy()
        {
            ImageManager pImageMan = PrivGetInstance();

            Debug.Assert(pImageMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        private static ImageManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        // Methods
        public static void Dump()
        {
            ImageManager pImageMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pImageMan != null);

            // Calling the Base manager Dump to print
            pImageMan.BaseDump();

        }
        public static Image Add(Image.Name name, Texture.Name textureName, float x, float y, float width, float height)
        {
            Image pImage = (Image)poInstance.BaseAddToFront();
            // Check the Image is not null
            Debug.Assert(pImage != null);

            // Set the data to Image
            pImage.SetValues(name, textureName, x, y, width, height);
            return pImage;
        }

        public static Image Find(Image.Name name)
        {
            poInstance.poNodeToFind.name = name;
            Image pImage = (Image)poInstance.BaseFind(poInstance.poNodeToFind);

            // Return the found node
            return pImage;
        }

        public static void Remove(Image.Name imageName)
        {
            ImageManager pImageManager = PrivGetInstance();
            // Null check on Image Manager and Image
            Debug.Assert(pImageManager != null);

            Image pImage = Find(imageName);

            // Remove Image from the Manager
            pImageManager.BaseRemove(pImage);
        }

        protected override BaseNode derivedConstructNode()
        {
            // LTN - ImageManager
            Image pImage = new Image();
            Debug.Assert(pImage != null);

            // Return a newly created Image
            return pImage;
        }

        // Data
        private readonly Image poNodeToFind;
        private static ImageManager poInstance;
    }
}

// End of file