using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryWall : Leaf
    {
        // Enum
        public enum WallType
        {
            WallGroup,
            Right,
            Left,
            Bottom,
            Top,

            Unintialized
        }

        // Constructor
        protected CategoryWall(Name gameName, Sprite.Name spriteName, float x, float y, WallType wallType)
        : base(gameName, spriteName, x, y)
        {
            this.wallType = wallType;
        }

        ~CategoryWall()
        {
        }

        // Method
        public WallType GetCategoryType()
        {
            return wallType;
        }

        // Data
        protected WallType wallType;
    }
}

// End of file