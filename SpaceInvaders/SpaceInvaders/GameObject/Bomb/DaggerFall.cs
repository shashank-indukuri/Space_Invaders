using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class DaggerFall : BombFallStrategy
    {
        // Constructor
        public DaggerFall()
        {
            this.oldPositionY = 0.0f;
        }

        // Overiding Methods
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
                pBomb.MultiplySpriteScale(1.0f, -1.0f);
                oldPositionY = targetY;
            }
        }

        // Data
        private float oldPositionY;
    }
}

// End of file
