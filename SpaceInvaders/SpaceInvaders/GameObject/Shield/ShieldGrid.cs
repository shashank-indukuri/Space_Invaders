using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldGrid : Composite
    {
        // Constructor
        public ShieldGrid(GameObject.Name name, Sprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.SetCollisionBoxColor(0.0f, 0.0f, 1.0f);
        }

        ~ShieldGrid()
        {
        }

        // Methods
        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            // this creates a new box which is white
            base.Resurrect();

            this.SetCollisionBoxColor(0.0f, 0.0f, 1.0f);
        }

        // Overriding Methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitShieldGrid method           
            other.VisitShieldGrid(this);
        }

        public override void VisitMissile(Missile missile)
        {
            // Missile vs ShieldGrid
            // Debug.WriteLine("--Grid vs Missile");
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(missile, pGameObj);
        }

        public override void VisitBomb(Bomb bomb)
        {
            // Bomb vs ShieldGrid
            // Debug.WriteLine("--Grid vs Bomb");
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
