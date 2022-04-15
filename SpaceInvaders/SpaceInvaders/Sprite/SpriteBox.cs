using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBox : BaseSprite
    {
        // Enum
        public enum Name
        {
            Box,
            Octopus,
            Crab,
            Squid,
            Grid,
            Column,
            NullObject,

            Uninitialized
        }

        public SpriteBox()
            : base()
        {
            x = 0.0f;
            y = 0.0f;
            sx = 1.0f;
            sy = 1.0f;
            angle = 0.0f;

            // Setting the values of name to Uninitialized and Image to null
            name = Name.Uninitialized;
            pImage = null;

            // LTN - SpriteBoxManager
            poColor = new Azul.Color();
            Debug.Assert(poColor != null);

            // LTN - SpriteBoxManager
            poAzulSpriteBox = new Azul.SpriteBox();
            Debug.Assert(poAzulSpriteBox != null);

            Debug.Assert(psbRect != null);
            psbRect.Set(0, 0, 1, 1);
        }

        // Methods
        public void SetValues(Name name, float x, float y, float width, float height, Azul.Color pSpriteColor)
        {
            // Before setting, check the AzulSpriteBox is not null
            Debug.Assert(poAzulSpriteBox != null);

            this.name = name;

            Debug.Assert(psbRect != null);
            psbRect.Set(x, y, width, height);

            // Setting the default color if the existing color is null
            if (pSpriteColor == null)
            {
                poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                poColor.Set(pSpriteColor);
            }

            poAzulSpriteBox.Swap(psbRect, poColor);

            // Updating the values of the Sprite
            this.x = poAzulSpriteBox.x;
            this.y = poAzulSpriteBox.y;
            sx = poAzulSpriteBox.sx;
            sy = poAzulSpriteBox.sy;
            angle = poAzulSpriteBox.angle;
        }

        public void SetValues(Name name, float x, float y, float width, float height)
        {
            // Before setting, check the AzulSpriteBox is not null
            Debug.Assert(poAzulSpriteBox != null);

            this.name = name;

            Debug.Assert(psbRect != null);
            psbRect.Set(x, y, width, height);

            poAzulSpriteBox.Swap(psbRect, poColor);

            // Updating the values of the Sprite
            this.x = poAzulSpriteBox.x;
            this.y = poAzulSpriteBox.y;
            sx = poAzulSpriteBox.sx;
            sy = poAzulSpriteBox.sy;
            angle = poAzulSpriteBox.angle;
        }

        public void SetColor(float r, float g, float b, float alpha = 1.0f)
        {
            Debug.Assert(poColor != null);

            // Set the color
            poColor.Set(r, g, b, alpha);

            // Swap the color for AzulSpriteBox
            poAzulSpriteBox.SwapColor(poColor);
        }

        public void SetRect(float x, float y, float width, float height)
        {
            Debug.Assert(this.poAzulSpriteBox != null);
            Debug.Assert(this.poColor != null);

            Debug.Assert(psbRect != null);
            SpriteBox.psbRect.Set(x, y, width, height);

            this.poAzulSpriteBox.Swap(psbRect, this.poColor);

            this.x = poAzulSpriteBox.x;
            this.y = poAzulSpriteBox.y;
            this.sx = poAzulSpriteBox.sx;
            this.sy = poAzulSpriteBox.sy;
            this.angle = poAzulSpriteBox.angle;
        }

        // Overriding methods
        override public void Update()
        {
            // Updating the Azul hardware with the current values
            poAzulSpriteBox.x = x;
            poAzulSpriteBox.y = y;
            poAzulSpriteBox.sx = sx;
            poAzulSpriteBox.sy = sy;
            poAzulSpriteBox.angle = angle;

            poAzulSpriteBox.Update();
        }

        public override void ClearValues()
        {
            // Ensuring the SpriteBox and Color are not null
            Debug.Assert(poAzulSpriteBox != null);
            Debug.Assert(poColor != null);

            name = Name.Uninitialized;
            pImage = null;

            poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);

            x = 0.0f;
            y = 0.0f;
            sx = 1.0f;
            sy = 1.0f;
            angle = 0.0f;
        }

        override public void Render()
        {
            poAzulSpriteBox.Render();
        }

        public override object GetName()
        {
            return name;
        }

        override public bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            SpriteBox pSpriteBox = (SpriteBox)pNodeToCompare;

            if (name == pSpriteBox.name)
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
            Debug.WriteLine("        AzulSprite: ({0})", poAzulSpriteBox.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", x, y);
            Debug.WriteLine("           (sx,sy): {0},{1}", sx, sy);
            Debug.WriteLine("           (angle): {0}", angle);

            base.Dump();
        }

        // Data
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        public Name name;
        public Image pImage;
        public Azul.Color poColor;
        private Azul.SpriteBox poAzulSpriteBox;

        //  Making the static to able to access the Screen rect
        // LTN - SpriteBoxManager
        private static Azul.Rect psbRect = new Azul.Rect();
    }
}

// End of file
