using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GhostManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public GhostManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - GameObjectNodeManager
            poNodeToFind = new GameObjectNode();

            // Linking the null game object
            // LTN - GameObjectNodeManager
            poGameObj = new NullGameObject();
            poNodeToFind.pGameObject = poGameObj;
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(psInstance == null);

            // Creating the new Ghost Manager
            if (psInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                psInstance = new GhostManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }

        public static void Destroy()
        {
            GhostManager pGhostMan = PrivGetInstance();

            Debug.Assert(pGhostMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        private static GhostManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        public static void Dump()
        {
            GhostManager pGhostMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pGhostMan != null);

            // Calling the Base manager Dump to print
            pGhostMan.BaseDump();
        }

        public static GameObjectNode Link(GameObject pGameObject)
        {
            // Linking the Game Object to the Ghost Manager

            GhostManager pGhostMan = PrivGetInstance();
            Debug.Assert(pGhostMan != null);

            Debug.Assert(pGameObject != null);

            GameObjectNode pGameObjectNode = (GameObjectNode)pGhostMan.BaseAddToFront();
            // Check the SpriteNode is not null
            Debug.Assert(pGameObjectNode != null);

            // Set the data to SpriteNode
            pGameObjectNode.SetValues(pGameObject);

            return pGameObjectNode;
        }

        public static void Remove(GameObjectNode pNode)
        {
            Debug.Assert(pNode != null);

            GhostManager pGhostMan = GhostManager.PrivGetInstance();
            Debug.Assert(pGhostMan != null);

            // remove the node
            pGhostMan.BaseRemove(pNode);
        }

        public static GameObjectNode Find(GameObject.Name name)
        {
            GhostManager pGhostMan = PrivGetInstance();
            Debug.Assert(pGhostMan != null);

            pGhostMan.poNodeToFind.pGameObject.name = name;

            GameObjectNode pGameObjNode = (GameObjectNode)pGhostMan.BaseFind(pGhostMan.poNodeToFind);
            //Debug.Assert(pGameObjNode != null);

            // Return the found node
            return pGameObjNode;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - GameObjectNodeManager
            GameObjectNode pGameObjNode = new GameObjectNode();
            Debug.Assert(pGameObjNode != null);

            // Return a newly created Game Object Node
            return pGameObjNode;
        }

        // Data
        private readonly GameObjectNode poNodeToFind;
        private static GhostManager psInstance = null;
        private readonly NullGameObject poGameObj;
    }
}

// End of file