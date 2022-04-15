using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BannerTextCommand : BaseCommand
    {
        // COnstructor
        public BannerTextCommand(BannerTextCommand pOldCmd, string pChar, float x, float y, 
            float red, float green, float blue)
        {
            this.pCharacter = pChar;

            this.red = red;
            this.green = green;
            this.blue = blue;

            this.x = x;
            this.y = y;
            this.poFont = null;
            this.pOldCommand = pOldCmd;

            if (this.pOldCommand != null)
            {
                //Debug.WriteLine(" {0}  old:{1}", this.GetHashCode(), this.pOldCommand.GetHashCode());
            }
            else
            {
                //Debug.WriteLine(" {0}  old:{1}", this.GetHashCode(), "null");

            }
        }
        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("exec start: {0} ", this.GetHashCode());


            // Remove the old character
            if (this.pOldCommand != null)
            {
                //Debug.WriteLine("{0} remove this one", this.pOldCommand.GetHashCode());
                FontManager.Remove(this.pOldCommand.poFont);
            }

            // New Character
            Font pFont = FontManager.Add(Font.Name.DelayCharacter,
                                     SpriteBatch.Name.Texts,
                                     this.pCharacter,
                                     Glyph.Name.Consolas36pt,
                                     this.x,
                                     this.y);

            pFont.SetColor(red, green, blue);


            this.poFont = pFont;

            Debug.WriteLine("exec exit: {0} this.poFont: {1}", this.GetHashCode(), this.poFont.GetHashCode());
        }

        // Data

        private float x;
        private float y;
        private float red;
        private float green;
        private float blue;
        private string pCharacter;
        private Font poFont;
        private BannerTextCommand pOldCommand;
    }
}

// End of file
