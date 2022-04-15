using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipGroup : Composite
    {
        // Constructor
        public ShipGroup(Name name, Sprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;

            // Set the color
            poCollisionObj.pCollisionSBoxProxy.SetColor(0, 0, 1);
        }

        ~ShipGroup()
        {
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitShipGroup method        
            other.VisitShipGroup(this);
        }
        public override void VisitBumperGroup(WallBumperGroup wbGroup)
        {
            // ShipGroup vs BumperGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(wbGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitBumperLeft(WallBumperLeft wbLeft)
        {
            // ShipGroup vs BumperGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(wbLeft, pGameObj);
        }

        public override void VisitBumperRight(WallBumperRight wbRight)
        {
            // ShipGroup vs BumperGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(wbRight, pGameObj);
        }

        public override void VisitBombGroup(BombGroup bGroup)
        {
            // ShipGroup vs BombGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(bGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitBomb(Bomb bomb)
        {
            // ShipGroup vs Bomb
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(bomb, pGameObj);
        }

        public override void Update()
        {
            BaseBoundingBoxUpdate(this);
            base.Update();
        }
    }
}

// End of file