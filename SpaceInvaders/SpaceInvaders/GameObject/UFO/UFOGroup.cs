using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOGroup : Composite
    {
        // Constructor
        public UFOGroup(Name name, Sprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;

            // Set the color
            poCollisionObj.pCollisionSBoxProxy.SetColor(0, 0, 1);
        }

        ~UFOGroup()
        {
        }

        // Methods
        public void MoveUFO(float delta)
        {

            ForwardCompositeIterator pForwardItr = new ForwardCompositeIterator(this);

            Component pNode = pForwardItr.First();

            // Walk through the nodes
            while (!pForwardItr.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;

                // Update the delta value of each gameobject
                pGameObj.x += delta;

                pNode = pForwardItr.Next();
            }
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitUFOGroup method        
            other.VisitUFOGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs UFOGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(m);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs UFOGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(m, pGameObj);
        }

        public override void VisitWallGroup(WallGroup wGroup)
        {
            // UFOGroup vs WallGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(wGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitWallLeft(WallLeft wLeft)
        {
            // UFOGroup vs WallLeft
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(wLeft, pGameObj);
        }

        public override void VisitWallRight(WallRight wRight)
        {
            // UFOGroup vs WallLeft
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(wRight, pGameObj);
        }

        public override void Update()
        {
            BaseBoundingBoxUpdate(this);
            base.Update();
        }
    }
}

// End of file