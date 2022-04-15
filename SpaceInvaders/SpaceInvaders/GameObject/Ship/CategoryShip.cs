using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryShip : Leaf
    {
        // Enum
        public enum ShipType
        {
            Ship,
            ShipGroup,
            Unitialized
        }

        // Contructor
        protected CategoryShip(GameObject.Name name, Sprite.Name spriteName, float posX, float posY, CategoryShip.ShipType shipType)
            : base(name, spriteName, posX, posY)
        {
            this.shipType = shipType;
        }

        ~CategoryShip()
        {
        }

        public override void Resurrect()
        {
            base.Resurrect();
        }


        // Data
        protected CategoryShip.ShipType shipType;
    }
}

// End of file