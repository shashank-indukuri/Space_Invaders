using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteProxyManager : BaseManager
    {
        public SpriteProxyManager(int InitialNumReserved = 3, int DeltaGrow = 1)
            : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - SpriteProxyManager
            poNodeToFind = new SpriteProxy();
            poNodeToFind.pSprite = SpriteManager.Find(Sprite.Name.NullObject);
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new Sprite Proxy Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new SpriteProxyManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);

            // Sprite Proxy Null is added
            SpriteProxyManager.Add(Sprite.Name.NullObject);
        }

        public static void Destroy()
        {
            SpriteProxyManager pSpriteProxyMan = PrivGetInstance();

            Debug.Assert(pSpriteProxyMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        private static SpriteProxyManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        public static void Dump()
        {
            SpriteProxyManager pSpriteProxyMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pSpriteProxyMan != null);

            // Calling the Base manager Dump to print
            pSpriteProxyMan.BaseDump();
        }

        public static SpriteProxy Add(Sprite.Name name)
        {
            SpriteProxy pSpriteProxy = (SpriteProxy)poInstance.BaseAddToFront();
            // Check the Sprite Proxy is not null
            Debug.Assert(pSpriteProxy != null);

            // Set the data to Sprite
            pSpriteProxy.SetValues(name);
            return pSpriteProxy;
        }

        public static SpriteProxy Find(Sprite.Name name)
        {
            SpriteProxyManager pProxyManager = SpriteProxyManager.PrivGetInstance();
            Debug.Assert(pProxyManager != null);

            poInstance.poNodeToFind.pSprite.name = name;
            SpriteProxy pSpriteProxy = (SpriteProxy)poInstance.BaseFind(poInstance.poNodeToFind);

            // Return the found node
            return pSpriteProxy;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - SpriteProxyManager
            SpriteProxy pSpriteProxy = new SpriteProxy();
            Debug.Assert(pSpriteProxy != null);

            // Return a newly created Sprite Proxy
            return pSpriteProxy;
        }

        // Data
        private readonly SpriteProxy poNodeToFind;
        private static SpriteProxyManager poInstance = null;
    }
}

// End of file
