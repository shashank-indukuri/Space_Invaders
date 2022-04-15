using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallBumperLeft : CategoryBumper
    {
        // Constructor
        public WallBumperLeft(GameObject.Name name, Sprite.Name spriteName, float x, float y, float width, float height)
            : base(name, spriteName, x, y, CategoryBumper.BumperType.BumperLeft)
        {
            this.x = x;
            this.y = y;

            this.poCollisionObj.poCollisionRect.Set(x, y, width, height);
            // Set the color
            poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~WallBumperLeft()
        {
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitBumperLeft method        
            other.VisitBumperLeft(this);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

// End of file