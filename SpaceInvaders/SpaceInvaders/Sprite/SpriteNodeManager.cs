using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteNodeManager : BaseManager
    {

        // Constructor
        public SpriteNodeManager(int InitialNumReserved = 3, int DeltaGrow = 1)
            : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - SpriteNodeManager
            poNodeToFind = new SpriteNode();
            Debug.Assert(poNodeToFind != null);

            pBackSpriteBatch = null;
        }

        // Methods

        public SpriteNode Link(BaseSprite pSprite)
        {
            SpriteNode pSpriteNode = (SpriteNode)this.BaseAddToFront();
            // Check the SpriteNode is not null
            Debug.Assert(pSpriteNode != null);

            // Set the data to SpriteNode
            pSpriteNode.SetValues(pSprite, this);

            return pSpriteNode;
        }

        public void SetValues(SpriteBatch.Name name, int InitialNumReserved, int DeltaGrow)
        {
            this.name = name;

            // make sure the values are not less than 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            BaseSetReserveList(InitialNumReserved, DeltaGrow);
        }


        public void Draw()
        {
            BaseIterator pIterator = poActiveList.FetchIterator();
            Debug.Assert(pIterator != null);

            SpriteNode pSpriteNode = (SpriteNode)pIterator.First();

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                // Calling the Render method in the Sprite
                pSpriteNode.pBaseSprite.Render();
                pSpriteNode =(SpriteNode)pIterator.Next();
            }
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }
        public void SetSpriteBatch(SpriteBatch pSpriteBatch)
        {
            this.pBackSpriteBatch = pSpriteBatch;
        }

        public void Remove(SpriteNode pSpriteNode)
        {
            Debug.Assert(pSpriteNode != null);

            this.BaseRemove(pSpriteNode);
        }

        public void Dump()
        {
            // Calling the Base manager Dump to print
            BaseDump();
        }

        // Overriding methods
        protected override BaseNode derivedConstructNode()
        {
            // LTN - SpriteNodeManager
            SpriteNode pSpriteNode = new SpriteNode();
            Debug.Assert(pSpriteNode != null);

            // Return a newly created Sprite Node
            return pSpriteNode;
        }

        // Data
        private readonly SpriteNode poNodeToFind;
        public SpriteBatch.Name name;

        // Delete back pointer
        private SpriteBatch pBackSpriteBatch;
    }
}

// End of file
