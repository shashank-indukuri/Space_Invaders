using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallBumperGroup : Composite
    {
        // Constructor
        public WallBumperGroup(Name name, Sprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;

            // Set the color
            poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
            this.name = name;
        }

        ~WallBumperGroup()
        {
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitBumperGroup method        
            other.VisitBumperGroup(this);
        }

        public override void Update()
        {
            BaseBoundingBoxUpdate(this);
            base.Update();
        }
    }
}

// End of file