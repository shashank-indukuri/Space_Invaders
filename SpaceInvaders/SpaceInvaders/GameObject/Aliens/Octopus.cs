using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Octopus : CategoryAlien
    {

        // Constructor
        public Octopus(Sprite.Name name, float x, float y)
            : base(Name.Octopus, name, x, y)
        {

        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            // Create a new box with white color
            base.Resurrect();

            this.SetCollisionBoxColor(1.0f, 1.0f, 1.0f);
        }

        // Overriding methods

        public override int GetScore()
        {
            return score;
        }
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitOctopus method            
            other.VisitOctopus(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Octopus
            //Debug.WriteLine("Missile vs Octopus");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }

        // Data
        private int score = 10;
    }
}

// End of file
