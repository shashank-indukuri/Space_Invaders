using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallRight : CategoryWall
    {
        // Constructor
        public WallRight(Name name, Sprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallType.Right)
        {
            poCollisionObj.poCollisionRect.Set(posX, posY, width, height);

            x = posX;
            y = posY;

            poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~WallRight()
        {

        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitWallRight method
            other.VisitWallRight(this);
        }

        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public override void VisitGroup(AlienGroup aGroup)
        {
            // AlienGrid vs WallRight
            Debug.WriteLine("\ncollide: {0} with {1}", this, aGroup);
            Debug.WriteLine("               --->DONE<----");

            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(aGroup, this);
            pColPair.NotifyListeners();
        }

        //public override void VisitGroup(AlienGroup aGroup)
        //{
        //    // AlienGrid vs WallRight
        //    //Debug.WriteLine("collide: {0} with {1}", aGrid, this);
        //    GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(aGroup);
        //    CollisionPair.CollidePair(pGameObj, this);
        //}

        //public override void VisitGrid(AlienGrid aGrid)
        //{
        //    // AlienGrid vs WallRight
        //    Debug.WriteLine("\ncollide: {0} with {1}", this, aGrid);
        //    Debug.WriteLine("               --->DONE<----");

        //    CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
        //    Debug.Assert(pColPair != null);

        //    pColPair.SetCollision(aGrid, this);
        //    pColPair.NotifyListeners();
        //}

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