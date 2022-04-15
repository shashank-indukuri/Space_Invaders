using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFO : CategoryUFO
    {
        // Constructor
        public UFO(Name name,Sprite.Name spriteName, float x, float y)
            : base(name, spriteName, x, y, CategoryUFO.UFOType.UFO)
        {
            this.x = x;
            this.y = y;
        }

        // Overriding methods

        public override int GetScore()
        {
            int score = 0;
            int rand = pRandom.Next(0, 3);

            switch (rand)
            {
                case 0:
                    score = highScore;
                    break;

                case 1:
                    score = mediumSocre;
                    break;

                case 2:
                    score = lowScore;
                    break;

                default:
                    break;
            }

            return score;
        }

        public override void Accept(CollisionVistor other)
        {
            // Call the VisitUFO method             
            other.VisitUFO(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs UFO
            //Debug.WriteLine("Missile vs Crab");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void VisitWallLeft(WallLeft wLeft)
        {
            // UFO vs WallLeft
            //Debug.WriteLine("Missile vs Crab");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(wLeft, this);
            pColPair.NotifyListeners();
        }

        public override void VisitWallRight(WallRight wRight)
        {
            // UFO vs WallRight
            //Debug.WriteLine("Missile vs Crab");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(wRight, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }

        // Data
        private int highScore = 150;
        private int mediumSocre = 100;
        private int lowScore = 50;
        public Random pRandom = new Random();
    }
}

// End of file