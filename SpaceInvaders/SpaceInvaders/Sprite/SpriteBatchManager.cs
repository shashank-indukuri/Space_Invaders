using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatchManager : BaseManager
    {
        public SpriteBatchManager(int InitialNumReserved = 0, int DeltaGrow = 1)
            : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - SpriteBatchManager
            poNodeToFind = new SpriteBatch();
            psActiveInstance = null;
        }

        // Static methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(psInstance == null);

            // Creating the new SpriteBatch Manager
            if (psInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                psInstance = new SpriteBatchManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }

        public static void Destroy()
        {
            SpriteBatchManager pSpriteMan = psActiveInstance;

            Debug.Assert(pSpriteMan != null);

            // Printing the states
            SpriteManager.Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        private static SpriteBatchManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        public static void Dump()
        {
            SpriteBatchManager pSBManager = psActiveInstance;
            // Make sure the instance is not null
            Debug.Assert(pSBManager != null);

            // Calling the Base manager Dump to print
            pSBManager.BaseDump();
        }

        public static SpriteBatch Add(SpriteBatch.Name name, float priority = 1, int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            SpriteBatchManager pSBManager = psActiveInstance;
            SpriteBatch pSpriteBatch = (SpriteBatch)pSBManager.BaseAddToFront(priority);
            // Check the Sprite Batch is not null
            Debug.Assert(pSpriteBatch != null);

            // Set the data to Sprite Batch
            pSpriteBatch.SetValues(name, InitialNumReserved, DeltaGrow);
            return pSpriteBatch;
        }

        public static void SetActiveBatch(SpriteBatchManager pSBMan)
        {
            SpriteBatchManager pSBManager = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pSBManager != null);

            Debug.Assert(pSBManager != null);
            SpriteBatchManager.psActiveInstance = pSBMan;
        }

        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchManager pSBManager = psActiveInstance;
            SpriteBatchManager.poNodeToFind.name = name;
            SpriteBatch pSpriteBatch = (SpriteBatch)pSBManager.BaseFind(SpriteBatchManager.poNodeToFind);

            // Return the found node
            return pSpriteBatch;
        }

        public static void Draw()
        {
            SpriteBatchManager pSBManager = psActiveInstance;
            Debug.Assert(pSBManager != null);

            BaseIterator pIterator = pSBManager.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            SpriteBatch pSpriteBatch = (SpriteBatch)pIterator.First();


            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                if (pSpriteBatch.GetEnableBoxes())
                {
                    SpriteNodeManager pSpriteNodeManager = pSpriteBatch.GetNodeManager();
                    Debug.Assert(pSpriteNodeManager != null);

                    // Pass on the work to Sprite Node Manager
                    pSpriteNodeManager.Draw();
                }

                // Get the next Sprite Batch
                pSpriteBatch = (SpriteBatch)pIterator.Next();
            }
        }

        public static void Remove(SpriteBatch pSpriteBatch)
        {
            SpriteBatchManager pSBManager = psActiveInstance;

            // Null check on SB Manager and Sprite Batch
            Debug.Assert(pSBManager != null);
            Debug.Assert(pSpriteBatch != null);

            // Remove Sprite Batch from the Manager
            pSBManager.BaseRemove(pSpriteBatch);
        }

        public static void Remove(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SpriteNodeManager pSpriteNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSpriteNodeMan != null);
            pSpriteNodeMan.Remove(pSpriteBatchNode);
        }

        public static void UpdatePriority(SpriteBatch.Name name, int priority)
        {
            // Find the the Sprite Batch
            SpriteBatch pSpriteBatch = Find(name);

            // Remove the Sprite Batch
            Remove(pSpriteBatch);

            // Add the Sprite Batch based on the updated priority
            Add(name, priority);
        }

        // Overriding methods
        protected override BaseNode derivedConstructNode()
        {
            // LTN - SpriteBatchManager
            SpriteBatch pSpriteBatch = new SpriteBatch();
            Debug.Assert(pSpriteBatch != null);

            // Return a newly created SpriteBatch
            return pSpriteBatch;
        }

        // Data
        private static SpriteBatch poNodeToFind;
        private static SpriteBatchManager psInstance = null;
        private static SpriteBatchManager psActiveInstance = null;
    }
}

// End of file
