using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryMissile : Leaf
    {
        // Enum
        public enum MissileType
        {
            Missile,
            MissileGroup,
            Unitialized
        }

        protected CategoryMissile(Name gameName, Sprite.Name spriteName, float x, float y)
        : base(gameName, spriteName, x, y)
        {
        }

        ~CategoryMissile()
        {
        }

        // Overriding Methods
        public override void Resurrect()
        {
            base.Resurrect();
        }
    }
}

//End of file
