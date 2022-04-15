using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatch : DoubleLink
    {
        public enum Name
        {
            Aliens,
            AngryBirds,
            Others,
            Boxes,
            Shields,
            Texts,
            Ships,

            Uninitialized
        }

        // Constructor
        public SpriteBatch()
            : base()
        {
            // Initializing the name and Sprite Node Manager
            name = Name.Uninitialized;

            // LTN - SpriteBatchManager
            poSpriteNodeMan = new SpriteNodeManager();
            Debug.Assert(poSpriteNodeMan != null);

            bEnableBoxes = true;
        }

        // Methods
        public void SetValues(Name name, int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            this.name = name;
            poSpriteNodeMan.SetValues(name, InitialNumReserved, DeltaGrow);
            bEnableBoxes = true;
        }

        public void SetName(Name name)
        {
            // Setting the name of the Batch
            this.name = name;
        }

        public SpriteNodeManager GetNodeManager()
        {
            return poSpriteNodeMan;
        }

        public void SetEnableBoxes(bool toggle)
        {
            bEnableBoxes = toggle;
        }

        public bool GetEnableBoxes()
        {
            return bEnableBoxes;
        }

        public SpriteNode Link(BaseSprite pBaseSprite)
        {
            // Attaching the Sprite to Sprite Node
            SpriteNode pSpriteNode = poSpriteNodeMan.Link(pBaseSprite);

            // Initialize SpriteBatchNode
            pSpriteNode.SetValues(pBaseSprite, this.poSpriteNodeMan);

            // Setting the back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);
            return pSpriteNode;
        }

        public SpriteNode Link(GameObject pGameObject)
        {
            // Attaching the Sprite Proxy to SpriteNode
            SpriteNode pSpriteNode = poSpriteNodeMan.Link(pGameObject.pSpriteProxy);

            // Initialize SpriteBatchNode
            pSpriteNode.SetValues(pGameObject.pSpriteProxy, this.poSpriteNodeMan);

            // Setting the back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);
            return pSpriteNode;
        }

        // Overriding methods
        public override object GetName()
        {
            // Returns the current name of the batch
            return name;
        }

        public override void ClearValues()
        {
            name = Name.Uninitialized;
        }

        override public bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            SpriteBatch pSpriteBatch = (SpriteBatch)pNodeToCompare;

            if (name == pSpriteBatch.name)
            {
                return true;
            }
            return false;
        }
        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", name, GetHashCode());

            base.Dump();
        }

        // Data
        public Name name;
        private readonly SpriteNodeManager poSpriteNodeMan;
        private bool bEnableBoxes;
    }
}

// End of file