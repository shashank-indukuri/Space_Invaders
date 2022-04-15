using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class StraightFall : BombFallStrategy
    {
        // Constructor
        public StraightFall()
        {
            this.oldPositionY = 0.0f;
        }

        // Overriding Methods
        public override void Reset(float posY)
        {
            this.oldPositionY = posY;
        }

        public override void BombFall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);

            // Do nothing for this strategy
        }

        // Data
        private float oldPositionY;
    }
}

// End of file
