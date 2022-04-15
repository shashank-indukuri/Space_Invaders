using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBoxProxy : BaseSprite
    {
        // Enum
        public enum Name
        {
            BoxProxy,
            NullObject,

            Uninitialized
        }

        // Constructor
        public SpriteBoxProxy()
            : base()
        {
            PrivClearValues();
        }

        protected SpriteBoxProxy(Name name)
            : base()
        {
            proxyName = name;
            PrivClearValues();
        }

        // Methods
        public void SetValues(SpriteBox.Name name)
        {
            proxyName = Name.BoxProxy;

            // Find the SpriteBox and link to pSpriteBox
            pSpriteBox = SpriteBoxManager.Find(name);

            x = 0.0f;
            y = 0.0f;
        }

        // Private methods
        private void PrivClearValues()
        {
            proxyName = Name.Uninitialized;

            x = 0.0f;
            y = 0.0f;

            pSpriteBox = null;
        }

        private void PrivUpdateRealSprite()
        {
            // Check if the real spritebox is not null
            Debug.Assert(pSpriteBox != null);

            pSpriteBox.x = x;
            pSpriteBox.y = y;
        }

        // Overriding methods
        public override void Render()
        {
            // Update the values
            PrivUpdateRealSprite();

            // Update and draw the Azul SpriteBox
            pSpriteBox.Update();
            pSpriteBox.Render();
        }

        public override void Update()
        {
            // Update the values
            PrivUpdateRealSprite();

            // Update the Azul SpriteBox
            pSpriteBox.Update();
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
            SpriteBoxProxy pSpriteBoxProxy = (SpriteBoxProxy)pNodeToCompare;

            if (pSpriteBox.name == pSpriteBoxProxy.pSpriteBox.name)
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
            if (pSpriteBox != null)
            {
                Debug.WriteLine("       Sprite Box:{0} ({1})", pSpriteBox.GetName(), pSpriteBox.GetHashCode());
            }
            else
            {
                Debug.WriteLine("       Sprite Box: null");
            }
            Debug.WriteLine("        (x,y): {0},{1}", x, y);

            base.Dump();
        }

        // Data
        public float x;
        public float y;
        public Name proxyName;
        public SpriteBox pSpriteBox;
    }
}

// End of file