using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Glyph : DoubleLink
    {
        // Enum
        public enum Name
        {
            Consolas36pt,
            NullObject,

            Uninitialized
        }

        // Constructor

        public Glyph()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.poRect = new Azul.Rect();
            this.key = 0;
        }

        // Methods

        public void SetValues(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            Debug.Assert(this.poRect != null);
            this.name = name;

            this.pTexture = TextureManager.Find(textName);
            Debug.Assert(this.pTexture != null);

            this.poRect.Set(x, y, width, height);

            this.key = key;

        }

        private void PrivClearValues()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.poRect.Set(0, 0, 1, 1);
            this.key = 0;
        }

        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.poRect != null);
            return this.poRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.pTexture != null);
            return this.pTexture.GetAzulTexture();
        }

        //- Overriding methods
        public override object GetName()
        {
            return this.name;
        }

        public override void ClearValues()
        {
            PrivClearValues();
        }

        override public bool Compare(BaseNode pTarget)
        {
            Debug.Assert(pTarget != null);

            // Used to compare two nodes
            Glyph pDataB = (Glyph)pTarget;

            if (this.name == pDataB.name && this.key == pDataB.key)
            {
                return true;
            }

            return false;
        }

        override public void Dump()
        {
            Debug.WriteLine("\t\tname: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("\t\t\tkey: {0}", this.key);
            if (this.pTexture != null)
            {
                Debug.WriteLine("\t\t   pTexture: {0}", this.pTexture.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pTexture: null");
            }
            Debug.WriteLine("\t\t      pRect: {0}, {1}, {2}, {3}", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            base.Dump();
        }

        // Data
        public Name name;
        public int key;
        private Azul.Rect poRect;
        private Texture pTexture;
    }
}

// End of file
