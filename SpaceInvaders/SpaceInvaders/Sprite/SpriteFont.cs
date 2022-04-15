using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteFont :BaseSprite
    {
        // Enum
        public enum Name
        {
            HotPink,

            RedBird,
            YellowBird,
            GreenBird,
            WhiteBird,
            BlueBird,

            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,

            NullObject,
            Uninitialized
        }

        // Constructor

        public SpriteFont()
            : base()
        {
            // Create a dummy sprite, it will get correctly linked in Set()

            this.poAzulSprite = new Azul.Sprite();
            this.poScreenRect = new Azul.Rect();
            this.poColor = new Azul.Color(1.0f, 1.0f, 1.0f);

            this.pMessage = null;
            this.glyphName = Glyph.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
        }

        // Methods

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;

            this.x = xStart;
            this.y = yStart;

            this.name = name;

            this.glyphName = glyphName;

            // Force color to white
            Debug.Assert(this.poColor != null);
            this.poColor.Set(1.0f, 1.0f, 1.0f);
        }

        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.poColor != null);
            this.poColor.Set(red, green, blue, alpha);
        }

        public void UpdateText(String pMessage)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;
        }

        override public void Update()
        {
            Debug.Assert(this.poAzulSprite != null);
        }

        override public void Render()
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poScreenRect != null);
            Debug.Assert(this.pMessage != null);
            Debug.Assert(this.pMessage.Length > 0);

            float xTmp = this.x;
            float yTmp = this.y;

            float xEnd = this.x;

            for (int i = 0; i < this.pMessage.Length; i++)
            {
                Debug.Assert(this.pMessage != null);
                int key = Convert.ToByte(pMessage[i]);

                Glyph pGlyph = GlyphManager.Find(this.glyphName, key);
                Debug.Assert(pGlyph != null);

                xTmp = xEnd + pGlyph.GetAzulRect().width / 2;
                this.poScreenRect.Set(xTmp, yTmp, pGlyph.GetAzulRect().width, pGlyph.GetAzulRect().height);

                poAzulSprite.Swap(pGlyph.GetAzulTexture(), pGlyph.GetAzulRect(), this.poScreenRect, this.poColor);

                poAzulSprite.Update();
                poAzulSprite.Render();

                // move the starting to the next character
                xEnd = pGlyph.GetAzulRect().width / 2 + xTmp;

            }
        }

        private void PrivClear()
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poScreenRect != null);

            this.poScreenRect.Set(0, 0, 0, 0);
            this.poColor.Set(1.0f, 1.0f, 1.0f);

            this.pMessage = null;
            this.glyphName = Glyph.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
        }

        public String GetMessage()
        {
            return this.pMessage;
        }

        // Overriding methods

        public override object GetName()
        {
            return this.name;
        }

        override public void ClearValues()
        {
            this.PrivClear();
        }

        override public bool Compare(BaseNode pTarget)
        {
            Debug.Assert(pTarget != null);

            // Used to compare two nodes
            SpriteFont pDataB = (SpriteFont)pTarget;

            if (this.name == pDataB.name)
            {
                return true;
            }

            return false;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            base.Dump();
        }

        // Data
        public Font.Name name;
        private Azul.Sprite poAzulSprite;
        private Azul.Rect poScreenRect;
        private Azul.Color poColor;   // this color is multplied by the texture

        private string pMessage;
        public Glyph.Name glyphName;

        public float x;
        public float y;
    }
}

// End of file
