using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallBumperRight : CategoryBumper
    {
        // Constructor
        public WallBumperRight(GameObject.Name name, Sprite.Name spriteName, float x, float y, float width, float height)
            : base(name, spriteName, x, y, CategoryBumper.BumperType.BumperRight)
        {
            this.x = x;
            this.y = y;

            this.poCollisionObj.poCollisionRect.Set(x, y, width, height);
            // Set the color
            poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~WallBumperRight()
        {
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitBumperRight method        
            other.VisitBumperRight(this);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

// End of file