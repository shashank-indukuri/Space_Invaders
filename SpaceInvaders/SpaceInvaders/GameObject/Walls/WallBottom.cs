using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class WallBottom : CategoryWall
    {
        // Constructor
        public WallBottom(GameObject.Name name, Sprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, CategoryWall.WallType.Bottom)
        {
            this.poCollisionObj.poCollisionRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~WallBottom()
        {
        }

        // Overriding Methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitWallBottom method           
            other.VisitWallBottom(this);
        }

        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine(" ---> Done");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            // Go to first child
            base.Update();
        }
    }
}

// End of file
