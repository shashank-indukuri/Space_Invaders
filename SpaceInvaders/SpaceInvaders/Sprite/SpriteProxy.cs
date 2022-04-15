using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteProxy : BaseSprite
    {
        // Enum
        public enum Name
        {
            Proxy,
            NullObject,

            Uninitialized
        }

        // Constructor
        public SpriteProxy()
            : base()
        {
             PrivClearValues();
        }

        protected SpriteProxy(Name name)
            : base()
        {
            proxyName = name;
            PrivClearValues();
        }

        // Method
        public void SetValues(Sprite.Name name)
        {
            proxyName = Name.Proxy;

            // Find the Sprite and link to pSprite
            pSprite = SpriteManager.Find(name);

            x = 0.0f;
            y = 0.0f;
            sx = 1.0f;
            sy = 1.0f;
        }

        // Private methods
        private void PrivClearValues()
        {
            proxyName = Name.Uninitialized;

            x = 0.0f;
            y = 0.0f;
            sx = 1.0f;
            sy = 1.0f;

            pSprite = null;
        }

        private void PrivUpdateRealSprite()
        {
            // Check if the real sprite is not null
            Debug.Assert(pSprite != null);

            pSprite.x = x;
            pSprite.y = y;
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }

        // Overriding methods
        public override void Render()
        {
            // Update the values
            PrivUpdateRealSprite();

            // Update and draw the Azul Sprite
            pSprite.Update();
            pSprite.Render();
        }

        public override void Update()
        {
            // Update the values
            PrivUpdateRealSprite();

            // Update the Azul Sprite
            pSprite.Update();
        }

        public override object GetName()
        {
            return proxyName;
        }
        public override void ClearValues()
        {
            PrivClearValues();
        }

        public override bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            SpriteProxy pSpriteProxy = (SpriteProxy)pNodeToCompare;

            if (pSprite.name == pSpriteProxy.pSprite.name)
            {
                return true;
            }
            return false;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", proxyName, GetHashCode());

            // Data:
            if (pSprite != null)
            {
                Debug.WriteLine("       Sprite:{0} ({1})", pSprite.GetName(), pSprite.GetHashCode());
            }
            else
            {
                Debug.WriteLine("       Sprite: null");
            }
            Debug.WriteLine("        (x,y): {0},{1}", x, y);

            base.Dump();
        }

        // Data
        public float x;
        public float y;
        public float sx;
        public float sy;
        public Name proxyName;
        public Sprite pSprite;
    }
}

// End of file