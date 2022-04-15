using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallGroup : Composite
    {
        // Constructor
        public WallGroup(Name gameName, Sprite.Name spriteName, float x, float y)
            : base(gameName, spriteName)
        {
            this.x = x;
            this.y = y;

            // Set the color
            poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 1);

            name = gameName;
        }

        ~WallGroup()
        {

        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitWallGroup method           
            other.VisitWallGroup(this);
        }

        public override void VisitGroup(AlienGroup aGroup)
        {
            // Alien Group vs WallGroup
            //Debug.WriteLine("collide: {0} with {1}", aGrid, this);
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(aGroup, pGameObj);
        }

        public override void VisitGrid(AlienGrid aGrid)
        {
            // AlienGrid vs WallGroup
            //Debug.WriteLine("collide: {0} with {1}", aGrid, this);
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(aGrid, pGameObj);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs WallGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(m);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs WallGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(m, pGameObj);
        }

        public override void VisitBombGroup(BombGroup bombGroup)
        {
            // BombRoot vs WallRoot
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(bombGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs WallRoot
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(b, pGameObj);
        }

        public override void Update()
        {
            BaseBoundingBoxUpdate(this);
            base.Update();
        }
    }
}

// End of file