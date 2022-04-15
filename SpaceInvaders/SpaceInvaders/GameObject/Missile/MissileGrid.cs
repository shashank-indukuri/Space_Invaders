using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileGrid : Composite
    {
        public MissileGrid()
            : base()
        {
            this.name = Name.MissileGroup;

            this.poCollisionObj.pCollisionSBoxProxy.pSpriteBox.SetColor(0, 0, 1);
        }

        ~MissileGrid()
        {

        }

        public override void Accept(CollisionVistor other)
        {
            // Call the VisitMissileGrid method           
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
