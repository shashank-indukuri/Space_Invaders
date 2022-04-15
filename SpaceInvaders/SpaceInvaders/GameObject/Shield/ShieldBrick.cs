using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBrick : CategoryShield
    {
        // Constructor
        public ShieldBrick(GameObject.Name name, Sprite.Name spriteName, float x, float y)
            : base(name, spriteName, x, y, CategoryShield.ShieldType.Brick)
        {
            this.x = x;
            this.y = y;

            this.SetCollisionBoxColor(1.0f, 1.0f, 1.0f);
        }
        ~ShieldBrick()
        {

        }

        // Methods
        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            // this creates a new box which is white
            base.Resurrect();

            // Set it to desired color
            this.SetCollisionBoxColor(1.0f, 1.0f, 1.0f);
        }

        // Overriding Methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitShieldBrick method               
            other.VisitShieldBrick(this);
        }

        public override void VisitMissile(Missile missile)
        {
            // Missile vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            // Debug.WriteLine("--Brick vs Missile");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(missile, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBomb(Bomb bomb)
        {
            // Bomb vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(bomb, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

// End of file