using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class SpriteNode : DoubleLink
    {

        // Constructor
        public SpriteNode()
            : base()
        {
            // Initializing the reference to sprite as null
            pBaseSprite = null;
            pBackSpriteNodeMan = null;
        }

        // Methods
        public void SetValues(BaseSprite pNode, SpriteNodeManager pSpriteNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pBaseSprite = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pBaseSprite != null);
            this.pBaseSprite.SetSpriteNode(this);

            Debug.Assert(pSpriteNodeMan != null);
            this.pBackSpriteNodeMan = pSpriteNodeMan;
        }
        public BaseSprite GetSpriteBase()
        {
            return this.pBaseSprite;
        }
        public SpriteNodeManager GetSBNodeMan()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan;
        }
        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan.GetSpriteBatch();
        }

        // Overriding methods
        public override void ClearValues()
        {
            pBaseSprite = null;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   ({0}) node", GetHashCode());

            // Data:
            Debug.WriteLine("   pSprite: {0} ({1})", pBaseSprite.GetName(), pBaseSprite.GetHashCode());

            base.Dump();
        }

        // Data
        public BaseSprite pBaseSprite;
        // Delete back pointer
        private SpriteNodeManager pBackSpriteNodeMan;
    }
}

// End of file