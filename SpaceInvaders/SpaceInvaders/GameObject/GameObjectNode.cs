using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameObjectNode : DoubleLink
    {
        // Constructor
        public GameObjectNode()
            : base()
        {
            PrivClearValues();
        }


        // Methods
        private void PrivClearValues()
        {
            pGameObject = null;
        }

        public void SetValues(GameObject pGameObject)
        {
            // Makesure the Game Object is not null
            Debug.Assert(pGameObject != null);

            this.pGameObject = pGameObject;
        }

        // Overriding methods

        public override void ClearValues()
        {
            PrivClearValues();
        }

        public override bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            GameObjectNode pGameObjectNode = (GameObjectNode)pNodeToCompare;

            // Null check
            Debug.Assert(pGameObjectNode != null);
            Debug.Assert(pGameObject != null);

            if (pGameObject.name == pGameObjectNode.pGameObject.name)
            {
                return true;
            }
            return false;
        }

        public override object GetName()
        {
            //Debug.Assert(this.pGameObj != null);

            object pObj = null;
            if (this.pGameObject != null)
            {
                pObj = this.pGameObject.GetName();
            }

            return pObj;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   GameObjectNode: ({0})", GetHashCode());

            // Data:
            if (pGameObject != null)
            {
                Debug.WriteLine("      GameObject.name: {0} ({1})", pGameObject.GetName(), pGameObject.GetHashCode());
            }
            else
            {
                Debug.WriteLine("      GameObject.name: null");
            }

            base.Dump();
        }

        // Data
        public GameObject pGameObject;
    }
}

// End of file