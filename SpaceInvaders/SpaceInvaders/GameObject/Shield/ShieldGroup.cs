using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldGroup : Composite
    {
        // Constructor
        public ShieldGroup(GameObject.Name name, Sprite.Name spriteName, float x, float y)
            : base(name, spriteName)
        {
            this.x = x;
            this.y = y;

        }
        ~ShieldGroup()
        {
        }

        // Methods
        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            base.Resurrect();

            this.SetCollisionBoxColor(1.0f, 1.0f, 1.0f);
        }

        // Overriding Methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitShieldGroup method         
            other.VisitShieldGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup mGroup)
        {
            // MissileRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(mGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }
        public override void VisitMissile(Missile missile)
        {
            // Missile vs ShieldRoot
            //Debug.WriteLine("--ShieldRoot vs Missile");
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(missile, pGameObj);
        }
        public override void VisitBombGroup(BombGroup bGroup)
        {
            // BombRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(bGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }
        public override void VisitBomb(Bomb bomb)
        {
            // Missile vs ShieldRoot
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(bomb, pGameObj);
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
