using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class Image : DoubleLink
    {
        // Enum
        public enum Name
        {
            HotPink,

            OctopusExtend,
            Octopus,
            CrabExtend,
            Crab,
            SquidExtend,
            Squid,
            GreenBird,
            RedBird,
            Ship,
            ShipExplosion,
            Missile,
            Wall,
            StraightBomb,
            ZigZagBomb,
            DaggerBomb,
            Brick,
            BrickLeftTop0,
            BrickLeftTop1,
            BrickLeftBottom,
            BrickRightBottom,
            BrickRightTop0,
            BrickRightTop1,
            Splat,
            UFO,
            UFOSplat,
            MissileSplat,
            BombSplat,
            NullObject,

            Uninitialized
        }

        // Constructor
        public Image()
            : base()
        {
            // Create the Azul Rect
            // LTN - ImageManager
            poAzulRect = new Azul.Rect();
            pTexture = null;
            name = Name.Uninitialized;
        }

        // Methods
        public void SetValues(Name name, Texture.Name textureName, float x, float y, float width, float height)
        {
            this.name = name;

            Texture pFoundTexture = TextureManager.Find(textureName);
            this.pTexture = pFoundTexture;

            poAzulRect.Set(x, y, width, height);
        }

        public Azul.Texture GetAzulTexture()
        {
            return pTexture.GetAzulTexture();
        }

        public Azul.Rect GetAzulRect()
        {
            return poAzulRect;
        }

        // Overriding Methods
        public override void ClearValues()
        {
            name = Name.Uninitialized;
            pTexture = null;

            // Checking if the rect is not null
            // else Clear shouldn't be done
            Debug.Assert(poAzulRect != null);
            poAzulRect.Clear();
        }

        public override object GetName()
        {
            return name;
        }

        override public bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            Image pImage = (Image)pNodeToCompare;

            if (name == pImage.name)
            {
                return true;
            }
            return false;
        }
        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poAzulRect.x, this.poAzulRect.y, this.poAzulRect.width, this.poAzulRect.height);

            base.Dump();
        }

        // ---------------------------------------
        // Data:
        // ---------------------------------------
        public Name name;
        public Texture pTexture;
        public Azul.Rect poAzulRect;
    }
}

// End of file