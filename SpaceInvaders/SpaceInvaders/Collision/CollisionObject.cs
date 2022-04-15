using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionObject
    {
        // Constructor
        public CollisionObject(SpriteProxy pSpriteProxy)
        {
            Debug.Assert(pSpriteProxy != null);

            // Store the Sprite from SpriteProxy
            Sprite pSprite = pSpriteProxy.pSprite;
            Debug.Assert(pSprite != null);

            // Create a new Collision Rect
            poCollisionRect = new CollisionRect(pSprite.GetRect());
            Debug.Assert(poCollisionRect != null); 

            // Create the SpriteBox
            pCollisionSBoxProxy = SpriteBoxManager.Add(SpriteBox.Name.Box, poCollisionRect.x, 
                poCollisionRect.y, poCollisionRect.width, poCollisionRect.height);

            Debug.Assert(pCollisionSBoxProxy != null);
            // Setting the color for SpriteBox
            pCollisionSBoxProxy.SetColor(1.0f, 1.0f, 1.0f);
        }

        public void Update(float x, float y)
        {
            // Updating the position of the SpriteBox
            poCollisionRect.x = x;
            poCollisionRect.y = y;

            pCollisionSBoxProxy.x = poCollisionRect.x;
            pCollisionSBoxProxy.y = poCollisionRect.y;

            // Set the SpriteBox values
            pCollisionSBoxProxy.SetRect(poCollisionRect.x, 
                poCollisionRect.y, poCollisionRect.width, poCollisionRect.height);

            // Update
            pCollisionSBoxProxy.Update();
        }

        public void Resurrect(SpriteProxy pSpriteProxy)
        {
            Debug.Assert(pSpriteProxy != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            // need to refactor if you want it different
            Sprite pSprite = pSpriteProxy.pSprite;
            Debug.Assert(pSprite != null);

            Debug.Assert(this.poCollisionRect != null);
            this.poCollisionRect.Set(pSprite.GetRect());

            Debug.Assert(this.pCollisionSBoxProxy != null);
            this.pCollisionSBoxProxy.SetValues(SpriteBox.Name.Box, this.poCollisionRect.x, 
                this.poCollisionRect.y, this.poCollisionRect.width, this.poCollisionRect.height);
            this.pCollisionSBoxProxy.SetColor(1.0f, 1.0f, 1.0f);
        }

        // Data
        public SpriteBox pCollisionSBoxProxy;
        public CollisionRect poCollisionRect;
    }
}

// End of file
