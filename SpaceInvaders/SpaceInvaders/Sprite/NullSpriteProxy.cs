using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class NullSpriteProxy : SpriteProxy
    {

        // Constructor
        public NullSpriteProxy()
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