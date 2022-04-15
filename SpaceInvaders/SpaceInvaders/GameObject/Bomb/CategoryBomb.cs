using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryBomb : Leaf
    {
        // Enum
        public enum BombType
        {
            Bomb,
            BombGroup,

            Unintialized
        }

        protected CategoryBomb(GameObject.Name name, Sprite.Name spriteName, float x, float y, CategoryBomb.BombType bombType)
            : base(name, spriteName, x, y)
        {
            this.bombType = bombType;
        }

        ~CategoryBomb()
        {
        }


        // Data
        protected CategoryBomb.BombType bombType;
    }
}

// End of file