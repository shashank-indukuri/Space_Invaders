using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ZigZagFall : BombFallStrategy
    {
        // Constructor
        public ZigZagFall()
        {
            this.oldPositionY = 0.0f;
        }

        // Overriding Emthods
        public override void Reset(float posY)
        {
            this.oldPositionY = posY;
        }

        public override void BombFall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);

            float targetY = oldPositionY - 1.0f * pBomb.FetchBoundingBoxHeight();

            if (pBomb.y < targetY)
            {
                pBomb.MultiplySpriteScale(-1.0f, 1.0f);
                oldPositionY = targetY;
            }
        }

        // Data
        private float oldPositionY;
    }
}

// End of file
