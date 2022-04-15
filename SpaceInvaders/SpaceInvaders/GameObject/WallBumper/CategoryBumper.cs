using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryBumper : Leaf
    {
        // Enum
        public enum BumperType
        {
            BumperLeft,
            BumperRight,

            Unitialized
        }

        // Constructor
        protected CategoryBumper(GameObject.Name gameName, Sprite.Name spriteName, float x, float y, CategoryBumper.BumperType type)
        : base(gameName, spriteName, x, y)
        {
            bumperType = type;
        }

        ~CategoryBumper()
        {
        }

        public CategoryBumper.BumperType GetCategoryType()
        {
            return this.bumperType;
        }

        // Data
        protected CategoryBumper.BumperType bumperType;
    }
}

// End of file