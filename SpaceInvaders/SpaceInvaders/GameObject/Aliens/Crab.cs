using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Crab : CategoryAlien
    {

        // Constructor
        public Crab(Sprite.Name name, float x, float y)
            : base(Name.Crab, name, x, y)
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
            // Call the VisitCrab method             
            other.VisitCrab(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Crab
            //Debug.WriteLine("Missile vs Crab");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }

        // Data
        private int score = 20;
    }
}

// End of file
