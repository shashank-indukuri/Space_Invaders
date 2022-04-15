using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileGroup : Composite
    {
        // Constructor
        public MissileGroup()
            : base()
        {
            this.name = Name.MissileGroup;

            this.poCollisionObj.pCollisionSBoxProxy.SetColor(0, 0, 1);
        }

        ~MissileGroup()
        {

        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitMissileGroup method           
            other.VisitMissileGroup(this);
        }

        public override void Update()
        {
            // Update the Bounding Box
            base.BaseBoundingBoxUpdate(this);
            base.Update();
        }
    }
}

// End of file