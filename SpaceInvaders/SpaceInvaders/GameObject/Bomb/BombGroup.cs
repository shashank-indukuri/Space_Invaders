using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombGroup : Composite
    {
        
        // Constructor
        public BombGroup(GameObject.Name name, Sprite.Name spriteName, float x, float y)
            : base(name, spriteName)
        {
            this.x = x;
            this.y = y;

            this.poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 1);
        }

        ~BombGroup()
        {
        }

        // Overriding Methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitBombGroup method
            other.VisitBombGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs BombGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(m);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs BombGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(m, pGameObj);
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