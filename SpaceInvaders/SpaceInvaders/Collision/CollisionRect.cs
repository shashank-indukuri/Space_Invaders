using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionRect : Azul.Rect
    {
        // Constructor
        public CollisionRect(CollisionRect pRect)
            : base(pRect)
        {
        }

        public CollisionRect(Azul.Rect pRect)
            : base(pRect)
        {
        }

        public CollisionRect(float x, float y, float width, float height)
            : base(x, y, width, height)
        {
        }

        public CollisionRect()
            : base()
        {
        }

        // Methods
        public void SetValues(float x, float y, float width, float height)
        {
            base.Set(x, y, width, height);
        }
        public void SetValues(Azul.Rect pRect)
        {
            Debug.Assert(pRect != null);
            base.Set(pRect);
        }
        public void Union(CollisionRect ColRect)
        {
            // Declaring the variables
            float minX;
            float minY;
            float maxX;
            float maxY;

            // Calculating the min and max of X coordinate
            if ((x - width / 2) < (ColRect.x - ColRect.width / 2))
            {
                minX = (x - width / 2);
            }
            else
            {
                minX = (ColRect.x - ColRect.width / 2);
            }

            if ((x + width / 2) > (ColRect.x + ColRect.width / 2))
            {
                maxX = (x + width / 2);
            }
            else
            {
                maxX = (ColRect.x + ColRect.width / 2);
            }

            // Calculating the min and max of Y coordinate
            if ((y + height / 2) > (ColRect.y + ColRect.height / 2))
            {
                maxY = (y + height / 2);
            }
            else
            {
                maxY = (ColRect.y + ColRect.height / 2);
            }

            if ((y - height / 2) < (ColRect.y - ColRect.height / 2))
            {
                minY = (y - height / 2);
            }
            else
            {
                minY = (ColRect.y - ColRect.height / 2);
            }

            // Union the both objects and find the max coordinates
            width = (maxX - minX);
            height = (maxY - minY);
            x = minX + width / 2;
            y = minY + height / 2;
        }

        static public bool Intersect(CollisionRect pColRectA, CollisionRect pColRectB)
        {
            bool status = false;

            // Find the coordinates
            float aMinX = pColRectA.x - pColRectA.width / 2;
            float aMaxX = pColRectA.x + pColRectA.width / 2;
            float aMinY = pColRectA.y - pColRectA.height / 2;
            float aMaxY = pColRectA.y + pColRectA.height / 2;

            float bMinX = pColRectB.x - pColRectB.width / 2;
            float bMaxX = pColRectB.x + pColRectB.width / 2;
            float bMinY = pColRectB.y - pColRectB.height / 2;
            float bMaxY = pColRectB.y + pColRectB.height / 2;

            // Check and if not collide, reject
            if ((bMaxX < aMinX) || (bMinX > aMaxX) || (bMaxY < aMinY) || (bMinY > aMaxY))
            {
                status = false;
            }
            else
            {
                status = true;
            }

            return status;
        }
    }
}

// End of file