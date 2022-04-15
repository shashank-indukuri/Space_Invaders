using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameObjectNodeManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public GameObjectNodeManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - GameObjectNodeManager
            poNodeToFind = new GameObjectNode();

            // Linking the null game object
            // LTN - GameObjectNodeManager
            NullGameObject pNullObj = new NullGameObject();

            poNodeToFind.pGameObject = pNullObj;

            psActiveInstance = null;
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(psInstance == null);

            // Creating the new Game Object Node Manager
            if (psInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                psInstance = new GameObjectNodeManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }

        public static void Destroy()
        {
            GameObjectNodeManager pGameObjNodeMan = psActiveInstance;

            Debug.Assert(pGameObjNodeMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        private static GameObjectNodeManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        public static void Dump()
        {
            GameObjectNodeManager pGameObjNodeMan = psActiveInstance;
            // Make sure the instance is not null
            Debug.Assert(pGameObjNodeMan != null);

            // Calling the Base manager Dump to print
            pGameObjNodeMan.BaseDump();
        }

        public static GameObjectNode Link(GameObject pGameObject)
        {
            // Linking the Game Object to the GameObject Node
            GameObjectNodeManager pGameObjNodeMan = psActiveInstance;
            Debug.Assert(pGameObjNodeMan != null);

            Debug.Assert(pGameObject != null);

            GameObjectNode pGameObjectNode = (GameObjectNode)pGameObjNodeMan.BaseAddToFront();
            // Check the SpriteNode is not null
            Debug.Assert(pGameObjectNode != null);

            // Set the data to SpriteNode
            pGameObjectNode.SetValues(pGameObject);

            return pGameObjectNode;
        }

        public static void Remove(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectNodeManager pGameObjNodeMan = psActiveInstance;

            GameObject pSafetyNode = pNode;

            // Linkedlist of trees

            // 1. Find the root node for the given node

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)ForwardCompositeIterator.GetParentNode(pTmp);
            }

            // 2. Walk through the active list

            BaseIterator pIt = pGameObjNodeMan.BaseFetchIterator();
            GameObjectNode pTree = (GameObjectNode)pIt.First();

            while (!pIt.IsDone())
            {
                // Compare pRoot with the node in the list
                if (pTree.pGameObject == pRoot)
                {
                    // found it
                    break;
                }
                pTree = (GameObjectNode)pIt.Next();
            }

            // 3. pTree is the Tree and remove the node pNode from the tree

            Debug.Assert(pTree != null);
            Debug.Assert(pTree.pGameObject != null);

            // pTree.pGameObject is same as pNode, no as it's always better to not delete the root node

            Debug.Assert(pTree.pGameObject != pNode);

            GameObject pParent = (GameObject)ForwardCompositeIterator.GetParentNode(pNode);
            Debug.Assert(pParent != null);

            // For the node to be delete, the child should be null
            GameObject pChild = (GameObject)ForwardCompositeIterator.GetChildNode(pNode);
            Debug.Assert(pChild == null);

            // remove the node
            pParent.Remove(pNode);

            pParent.Update();
        }

        public static void SetActiveGOMan(GameObjectNodeManager pGOMan)
        {
            GameObjectNodeManager pGameObjNodeMan = PrivGetInstance();
            Debug.Assert(pGameObjNodeMan != null);

            Debug.Assert(pGOMan != null);
            GameObjectNodeManager.psActiveInstance = pGOMan;
        }

        public static GameObject Find(GameObject.Name name)
        {
            GameObjectNodeManager pGameObjNodeMan = psActiveInstance;
            Debug.Assert(pGameObjNodeMan != null);

            GameObjectNodeManager.poNodeToFind.pGameObject.name = name;

            GameObjectNode pGameObjNode = (GameObjectNode)pGameObjNodeMan.BaseFind(GameObjectNodeManager.poNodeToFind);

            GameObject pGameObj = null;

            if (pGameObjNode != null)
            {
                pGameObj = pGameObjNode.pGameObject;
            }

            return pGameObj;
        }

        public static void Update()
        {
            GameObjectNodeManager pGameObjNodeMan = psActiveInstance;
            Debug.Assert(pGameObjNodeMan != null);

            // Get the iterator reference
            BaseIterator pIterator = pGameObjNodeMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            GameObjectNode pGameObjNode = (GameObjectNode)pIterator.First();
            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                ReverseCompositeIterator pRevIterator = new ReverseCompositeIterator(pGameObjNode.pGameObject);

                Component pNode = pRevIterator.First();
                while (!pRevIterator.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;

                    //Debug.WriteLine("update: {0} ({1})", pGameObj, pGameObj.GetHashCode());
                    pGameObj.Update();

                    pNode = pRevIterator.Next();
                }

                // Go to the next node
                pGameObjNode = (GameObjectNode)pIterator.Next();
            }
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
        private static GameObjectNode poNodeToFind;
        private static GameObjectNodeManager psInstance = null;
        private static GameObjectNodeManager psActiveInstance = null;
    }
}

// End of file