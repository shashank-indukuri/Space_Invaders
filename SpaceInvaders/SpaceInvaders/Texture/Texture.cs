using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : DoubleLink
    {
        public enum Name
        {
            HotPink,
            SpaceInvaders,
            Consolas36pt,
            Birds,

            Uninitialized
        }

        public Texture()
            : base()
        {
            // Create the Azul Texture
            // LTN - TextureManager
            poTexture = new Azul.Texture();
            Debug.Assert(poTexture != null);
            name = Name.Uninitialized;
        }

        public void SetValues(Name name, String textureName)
        {
            // Texture name shouldn't be empty
            Debug.Assert(textureName != null);
            this.name = name;
            poTexture.Set(textureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
        }

        public Azul.Texture GetAzulTexture()
        {
            return poTexture;
        }

        // Overriding Methods
        public override void ClearValues()
        {
            name = Name.Uninitialized;
            Debug.Assert(poTexture != null);

            // Setting the default texture HotPink to the Texture
            poTexture.Set("HotPink.tga", Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
        }

        public override object GetName()
        {
            return name;
        }

        override public bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            Texture pTexture = (Texture)pNodeToCompare;

            if (name == pTexture.name)
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
            Debug.WriteLine("      poAzulTexture: {0} ", poTexture.GetHashCode());

            base.Dump();
        }

        // Data
        public Name name;
        public Azul.Texture poTexture;
    }
}

// End of file