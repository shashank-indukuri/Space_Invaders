using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageNode : DoubleLink
    {

        // Constructor
        public ImageNode(Image pImage)
            : base()
        {
            // Checking if the pImage is not null
            Debug.Assert(pImage != null);

            this.pImage = pImage;
        }


        // Overriding methods
        public override void ClearValues()
        {
            pImage = null;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   ({0}) node", GetHashCode());

            // Data:
            Debug.WriteLine("   pSprite: {0} ({1})", pImage.GetName(), pImage.GetHashCode());

            base.Dump();
        }

        // Data
        public Image pImage;
    }
}

// End of file