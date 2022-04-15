using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallLeft : CategoryWall
    {
        // Constructor
        public WallLeft(GameObject.Name name, Sprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallType.Left)
        {
            poCollisionObj.poCollisionRect.Set(posX, posY, width, height);

            x = posX;
            y = posY;

            // Set the color
            poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~WallLeft()
        {

        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitWallLeft method           
            other.VisitWallLeft(this);
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public override void VisitGroup(AlienGroup aGroup)
        {
            // AlienGrid vs WallLeft
            //Debug.WriteLine("\ncollide: {0} with {1}", this, aGroup);
            //Debug.WriteLine("               --->DONE<----");

            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(aGroup, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine(" ---> Done");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

    }
}

// End of file
