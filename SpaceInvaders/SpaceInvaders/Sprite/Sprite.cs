using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Sprite : BaseSprite
    {
        // Enum
        public enum Name
        {

            StraightBomb,
            ZigZagBomb,
            DaggerBomb,

            Octopus,
            Crab,
            Squid,
            OctopusExtend,
            CrabExtend,
            SquidExtend,

            Missile,
            Ship,
            Ship1,
            Ship2,
            ShipExplosion,

            Wall,
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
            BannerSquid,
            BannerCrab,
            BannerOctopus,
            BannerUFO,
            NullObject,

            Uninitialized
        }

        public Sprite()
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

            // LTN - SpriteManager
            poColor = new Azul.Color();
            Debug.Assert(poColor != null);

            // LTN - SpriteManager
            poAzulSprite = new Azul.Sprite();
            Debug.Assert(poAzulSprite != null);

            // creating the Rect and using it everytime directly as it is a Static variable
            // LTN - SpriteManager
            poRect = new Azul.Rect();
            Debug.Assert(poRect != null);
        }

        public void SetValues(Name name, Image.Name imageName, float x, float y, float width, float height, Azul.Color pSpriteColor)
        {

            // Before setting, check the AzulSprite is not null
            Debug.Assert(poAzulSprite != null);

            Image pFoundImage = ImageManager.Find(imageName);
            this.pImage = pFoundImage;

            this.name = name;
            poRect.Set(x, y, width, height);

            // Setting the default color if the existing color is null
            if (pSpriteColor == null)
            {
                poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                poColor.Set(pSpriteColor);
            }

            poAzulSprite.Swap(pImage.pTexture.poTexture, pImage.poAzulRect, poRect, poColor);

            // Updating on the framework side
            poAzulSprite.Update();

            // Updating the values of the Sprite
            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            sx = poAzulSprite.sx;
            sy = poAzulSprite.sy;
            angle = poAzulSprite.angle;
        }

        public void SwapColor(Azul.Color pColor)
        {
            poAzulSprite.SwapColor(pColor);
        }

        public void SwapImage(Image pImage)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(poAzulSprite != null);

            this.pImage = pImage;

            // Swaping the Texture and the Rect of the current Sprite
            poAzulSprite.SwapTexture(pImage.GetAzulTexture());
            poAzulSprite.SwapTextureRect(pImage.GetAzulRect());

        }

        public override void ClearValues()
        {
            // Ensuring the Sprite and Color are not null
            Debug.Assert(poAzulSprite != null);
            Debug.Assert(poColor != null);

            name = Name.Uninitialized;
            this.pImage = null;

            poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);

            Image pImage = ImageManager.Find(Image.Name.HotPink);
            Debug.Assert(pImage != null);

            x = 0.0f;
            y = 0.0f;
            sx = 1.0f;
            sy = 1.0f;
            angle = 0.0f;

            poRect.Set(0.0f, 0.0f, 1.0f, 1.0f);

            // Swapping the Sprite to the default Image and location
            poAzulSprite.Swap(pImage.pTexture.poTexture, pImage.poAzulRect, poRect, poColor);
            poAzulSprite.Update();
        }

        public override object GetName()
        {
            return name;
        }

        public Azul.Rect GetRect()
        {
            return poRect;
        }

        override public void Update()
        {
            // Updating the Azul hardware with the current values
            poAzulSprite.x = x;
            poAzulSprite.y = y;
            poAzulSprite.sx = sx;
            poAzulSprite.sy = sy;
            poAzulSprite.angle = angle;

            poAzulSprite.Update();
        }

        override public void Render()
        {
            poAzulSprite.Render();
        }

        override public bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            Sprite pSprite = (Sprite)pNodeToCompare;

            if (name == pSprite.name)
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
            Debug.WriteLine("        AzulSprite: ({0})", poAzulSprite.GetHashCode());
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
        private Azul.Sprite poAzulSprite;
        private Azul.Rect poRect;
    }
}

// End of file