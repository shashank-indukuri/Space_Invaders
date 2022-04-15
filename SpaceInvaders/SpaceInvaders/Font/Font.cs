using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Font : DoubleLink
    {
        //Enum
        public enum Name
        {
            Score1Text,
            Score2Text,
            HiScoreText,
            Score1,
            Score2,
            HiScore,
            Lifes,
            DelayCharacter,

            NullObject,
            Uninitialized
        }

        // Constructor

        public Font()
        {
            this.name = Name.Uninitialized;
            this.poSpriteFont = new SpriteFont();
        }

        // Methods

        public void SetValues(Font.Name name, string pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            this.poSpriteFont.Set(name, pMessage, glyphName, xStart, yStart);
        }

        public void SetColor(float r, float g, float b)
        {
            this.poSpriteFont.SetColor(r, g, b);
        }

        public void UpdateText(string pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(this.poSpriteFont != null);

            // Update the message
            this.poSpriteFont.UpdateText(pMessage);
        }

        private void PrivClear()
        {
            this.name = Name.Uninitialized;
            this.poSpriteFont.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        // Overriding methods

        public override object GetName()
        {
            return this.name;
        }

        public override void ClearValues()
        {
            this.PrivClear();
        }

        override public bool Compare(BaseNode pTarget)
        {
            Debug.Assert(pTarget != null);

            // Used to compare two nodes
            Font pDataB = (Font)pTarget;

            if (this.name == pDataB.name)
            {
                return true;
            }

            return false;
        }

        override public void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            base.Dump();
        }

        // Data
        public Name name;
        public SpriteFont poSpriteFont;
        static private string pNullString = "null";
    }
}

// End of file