using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteBoxProxyManager : BaseManager
    {
        public SpriteBoxProxyManager(int InitialNumReserved = 3, int DeltaGrow = 1)
            : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - SpriteBoxProxyManager
            poNodeToFind = new SpriteBoxProxy();
            poNodeToFind.pSpriteBox = SpriteBoxManager.Find(SpriteBox.Name.NullObject);
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new SpriteBox Proxy Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new SpriteBoxProxyManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);

            // SpriteBox Proxy Null is added
            SpriteBoxProxyManager.Add(SpriteBox.Name.NullObject);
        }

        public static void Destroy()
        {
            SpriteBoxProxyManager pSpriteBoxProxyMan = PrivGetInstance();

            Debug.Assert(pSpriteBoxProxyMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        private static SpriteBoxProxyManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        public static void Dump()
        {
            SpriteBoxProxyManager pSpriteBoxProxyMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pSpriteBoxProxyMan != null);

            // Calling the Base manager Dump to print
            pSpriteBoxProxyMan.BaseDump();
        }

        public static SpriteBoxProxy Add(SpriteBox.Name name)
        {
            SpriteBoxProxy pSpriteBoxProxy = (SpriteBoxProxy)poInstance.BaseAddToFront();
            // Check the SpriteBox Proxy is not null
            Debug.Assert(pSpriteBoxProxy != null);

            // Set the data to SpriteBox
            pSpriteBoxProxy.SetValues(name);
            return pSpriteBoxProxy;
        }

        public static SpriteBoxProxy Find(SpriteBox.Name name)
        {
            SpriteBoxProxyManager pBoxProxyManager = SpriteBoxProxyManager.PrivGetInstance();
            Debug.Assert(pBoxProxyManager != null);

            poInstance.poNodeToFind.pSpriteBox.name = name;
            SpriteBoxProxy pSpriteBoxProxy = (SpriteBoxProxy)poInstance.BaseFind(poInstance.poNodeToFind);

            // Return the found node
            return pSpriteBoxProxy;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - SpriteBoxProxyManager
            SpriteBoxProxy pSpriteBoxProxy = new SpriteBoxProxy();
            Debug.Assert(pSpriteBoxProxy != null);

            // Return a newly created SpriteBox Proxy
            return pSpriteBoxProxy;
        }

        // Data
        private readonly SpriteBoxProxy poNodeToFind;
        private static SpriteBoxProxyManager poInstance = null;
    }
}
