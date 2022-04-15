using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGroup : Composite
    {
        // Constructor
        public AlienGroup()
            : base()
        {
            name = Name.AlienGroup;
            delta = 5.0f;
            positionY = -15.0f;
            bWallCollision = false;
            levelGridSpeed = 1.0f;
            initialGridSpeed = 1.0f;
            gridLocation = 800.0f;
        }

        public AlienGroup(GameObject.Name name, Sprite.Name spriteName, float x, float y)
            : base(name, spriteName)
        {
            this.x = x;
            this.y = y;

            this.name = name;
            delta = 5.0f;
            positionY = -15.0f;
            bWallCollision = false;
            levelGridSpeed = 1.0f;
            initialGridSpeed = 1.0f;
            gridLocation = 800.0f;
        }

        // Methods

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            base.Resurrect();

            this.SetCollisionBoxColor(1.0f, 1.0f, 1.0f);
        }
        public void MoveGrid()
        {

            ForwardCompositeIterator pForwardItr = new ForwardCompositeIterator(this);

            Component pNode = pForwardItr.First();

            // Walk through the nodes
            while (!pForwardItr.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;

                // Update the delta value of each gameobject
                pGameObj.x += this.delta;
                if (bWallCollision)
                {
                    pGameObj.y += positionY;
                }
                pNode = pForwardItr.Next();
            }

            bWallCollision = false;
        }

        public float GetDelta()
        {
            return this.delta;
        }

        public void SetDelta(float delta)
        {
            bWallCollision = true;
            this.delta = delta;
        }

        public void ResetDelta(float delta)
        {
            this.delta = delta;
        }

        public void SetGridSpeed(float delta)
        {
            this.levelGridSpeed = delta;
        }

        public float GetGridSpeed()
        {
            return this.levelGridSpeed;
        }

        public void SetInitialGridSpeed(float delta)
        {
            this.initialGridSpeed = delta;
        }

        public float GetGridLocation()
        {
            return this.gridLocation;
        }

        public void SetGridLocation(float delta)
        {
            this.gridLocation = delta;
        }

        public float GetInitialGridSpeed()
        {
            return this.initialGridSpeed;
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitRoot method           
            other.VisitGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs AlienGrid
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(m);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs AlienGrid
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(m, pGameObj);
        }

        public override void Update()
        {
            base.BaseBoundingBoxUpdate(this);
            base.Update();
        }

        // Data
        private float delta;
        private float positionY;
        private bool bWallCollision;
        private float levelGridSpeed;
        private float initialGridSpeed;
        private float gridLocation;
    }
}

// End of file