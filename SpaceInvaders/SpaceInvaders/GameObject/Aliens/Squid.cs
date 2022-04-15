using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Squid : CategoryAlien
    {

        // Constructor
        public Squid(Sprite.Name name, float x, float y)
            : base(Name.Squid, name, x, y)
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

        // Overriding methods

        public override int GetScore()
        {
            return score;
        }
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitSquid method             
            other.VisitSquid(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Squid
            //Debug.WriteLine("Missile vs Squid");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }

        // Data
        private int score = 30;
    }
}

// End of file
