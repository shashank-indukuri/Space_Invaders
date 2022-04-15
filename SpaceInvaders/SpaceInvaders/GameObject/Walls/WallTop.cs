using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallTop : CategoryWall
    {
        // Constructor
        public WallTop(Name name, Sprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallType.Top)
        {
            this.poCollisionObj.poCollisionRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~WallTop()
        {
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitWallTop method
            other.VisitWallTop(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs WallTop
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(m);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs WallTop
            //Debug.WriteLine("Missile vs Wall Top");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }
    }
}
