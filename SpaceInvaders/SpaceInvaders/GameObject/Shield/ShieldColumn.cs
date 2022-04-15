using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldColumn : Composite
    {
        // Constructor
        public ShieldColumn(GameObject.Name name, Sprite.Name spriteName, float x, float y)
            : base(name, spriteName)
        {
            this.x = x;
            this.y = y;
            this.SetCollisionBoxColor(1.0f, 0.0f, 0.0f);
        }
        ~ShieldColumn()
        {
        }

        // Methods
        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            base.Resurrect();
            this.SetCollisionBoxColor(1.0f, 0.0f, 0.0f);
        }

        // Overriding Methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitShieldColumn method           
            other.VisitShieldColumn(this);
        }

        public override void VisitMissile(Missile missile)
        {
            // Missile vs ShieldColumn
            //Debug.WriteLine("--Column vs Missile");
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(missile, pGameObj);
        }
        public override void VisitBomb(Bomb bomb)
        {
            // Bomb vs ShieldColumn
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
