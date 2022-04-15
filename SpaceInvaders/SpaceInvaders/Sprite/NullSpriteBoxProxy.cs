using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class NullSpriteBoxProxy : SpriteBoxProxy
    {
        // Constructor
        public NullSpriteBoxProxy()
            : base(Name.NullObject)
        {

        }

        public override void Render()
        {
            // Just nothing
        }

        public override void Update()
        {
            // Just nothing
        }
    }
}

// End of file
