using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryShield : Leaf
    {
        // Enum
        public enum ShieldType
        {
            Group,
            Grid,
            Column,
            Brick,

            LeftTop0,
            LeftTop1,
            LeftBottom,
            RightTop0,
            RightTop1,
            RightBottom,

            Unitialized
        }

        protected CategoryShield(GameObject.Name name, Sprite.Name spriteName, float posX, float posY, CategoryShield.ShieldType shieldType)
            : base(name, spriteName, posX, posY)
        {
            this.shieldType = shieldType;
        }

        ~CategoryShield()
        {
        }

        public ShieldType GetCategoryType()
        {
            return this.shieldType;
        }

        // Overriding Methods
        public override void Resurrect()
        {
            base.Resurrect();
        }

        // Data
        protected CategoryShield.ShieldType shieldType;
    }
}

// End of file
