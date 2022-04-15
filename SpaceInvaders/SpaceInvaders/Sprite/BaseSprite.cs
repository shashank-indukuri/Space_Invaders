using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BaseSprite : DoubleLink
    {
        public BaseSprite()
            : base()
        {
            this.pPrevSpriteNode = null;
        }
        abstract public void Update();

        abstract public void Render();

        public SpriteNode GetSpriteNode()
        {
            Debug.Assert(this.pPrevSpriteNode != null);
            return this.pPrevSpriteNode;
        }
        public void SetSpriteNode(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pPrevSpriteNode = pSpriteBatchNode;
        }

        // Data
        private SpriteNode pPrevSpriteNode;
    }
}

// End of file
