using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionPairManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public CollisionPairManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - CollisionPairManager
            poNodeToFind = new CollisionPair();

            pCurrentColPair = null;

            psActiveInstance = null;
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new Game Object Node Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new CollisionPairManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);
        }

        public static CollisionPair Add(CollisionPair.Name colPairName, GameObject pCompositeA, GameObject pCompositeB)
        {
            // Get the instance
            CollisionPairManager pColPairObjNodeMan = psActiveInstance;
            Debug.Assert(pColPairObjNodeMan != null);

            // Add the node to active and return the node
            CollisionPair pCollisionPair = (CollisionPair)pColPairObjNodeMan.BaseAddToEnd();
            Debug.Assert(pCollisionPair != null);

            // Set the data for Collision Pair
            pCollisionPair.SetValues(colPairName, pCompositeA, pCompositeB);

            // return the Collision Pair
            return pCollisionPair;
        }

        public static void SetActiveCollisionManager(CollisionPairManager pCollManager)
        {
            CollisionPairManager pColPairObjMan = CollisionPairManager.PrivGetInstance();
            Debug.Assert(pColPairObjMan != null);

            Debug.Assert(pCollManager != null);
            CollisionPairManager.psActiveInstance = pCollManager;
        }

        static public CollisionPair GetCurrentColPair()
        {
            // Get the instance
            CollisionPairManager pColPairObjNodeMan = psActiveInstance;

            return pColPairObjNodeMan.pCurrentColPair;
        }

        public static void Process()
        {
            // Get the instance
            CollisionPairManager pColPairObjNodeMan = psActiveInstance;

            // Fetch the Iterator
            BaseIterator pIterator = pColPairObjNodeMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            CollisionPair pCurrentNode = (CollisionPair)pIterator.First();

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                Debug.Assert(pCurrentNode != null);

                // Setting the currentPair to the pCurrentColPair
                pColPairObjNodeMan.pCurrentColPair = pCurrentNode;
                // Process the pair
                pCurrentNode.Process();

                // Get the Next pair
                pCurrentNode = (CollisionPair)pIterator.Next();
            }
        }

        public static void Destroy()
        {
            CollisionPairManager pColPairObjNodeMan = psActiveInstance;

            Debug.Assert(pColPairObjNodeMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        private static CollisionPairManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        public static void Dump()
        {
            CollisionPairManager pColPairObjNodeMan = psActiveInstance;
            // Make sure the instance is not null
            Debug.Assert(pColPairObjNodeMan != null);

            // Calling the Base manager Dump to print
            pColPairObjNodeMan.BaseDump();
        }

        public static CollisionPair Find(CollisionPair.Name name)
        {
            CollisionPairManager pColPairObjNodeMan = psActiveInstance;
            Debug.Assert(pColPairObjNodeMan != null);

            poNodeToFind.name = name;

            CollisionPair pCollisionPair = (CollisionPair)pColPairObjNodeMan.BaseFind(poNodeToFind);
            Debug.Assert(pCollisionPair != null);

            // Return the found node
            return pCollisionPair;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - CollisionPairManager
            CollisionPair pCollisionPair = new CollisionPair();
            Debug.Assert(pCollisionPair != null);

            // Return a newly created Collision Pair
            return pCollisionPair;
        }

        // Data
        private static CollisionPair poNodeToFind;
        private static CollisionPairManager poInstance = null;
        private static CollisionPairManager psActiveInstance = null;
        private CollisionPair pCurrentColPair;
    }
}

// End of file
