using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienColumn : Composite
    {
        public enum BombState
        {
            BombFlying,
            BombReady,

            Uninitialized
        }
        // Constructor
        public AlienColumn(GameObject.Name name, Sprite.Name spriteName)
            : base(name, spriteName)
        {
            poCollisionObj.pCollisionSBoxProxy.SetColor(1, 0.5f, 0);

            pBombReady = new BombReady();
            pBombFlying = new BombFlying();

            pBombState = pBombReady;
        }

        // Method

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            base.Resurrect();
            this.SetCollisionBoxColor(1.0f, 0.0f, 0.0f);
        }

        public void ShootBomb(GameObject pColumn)
        {
            pBombState.ShootBomb(pColumn);
        }

        public void SetState(AlienColumn.BombState name)
        {
            switch (name)
            {
                case BombState.BombReady:
                    pBombState = pBombReady;
                    break;

                case BombState.BombFlying:
                    pBombState = pBombFlying;
                    break;
            }
        }


        // Override methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitColumn method           
            other.VisitColumn(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs AlienColumn
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(m, pGameObj);
        }

        public override void Update()
        {
            // Update the Bounding Box
            BaseBoundingBoxUpdate(this);
            base.Update();
        }

        public override void Print()
        {
            Debug.WriteLine("\nColumn:");

            BaseIterator pIterator = poDoubleLinkMan.FetchIterator();
            Debug.Assert(pIterator != null);

            GameObject pGameObj = (GameObject)pIterator.First();

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {
                // Print the details of the current node
                pGameObj.Dump();

                // Go to the next node
                pGameObj = (GameObject)pIterator.Next();
            }
        }

        // Static field for Null Sprite Proxy
        // LTN - GameObjectNodeManager
        private static NullSpriteProxy pNullSpriteProxy = new NullSpriteProxy();

        // Reference
        private BombReady pBombReady;
        private BombFlying pBombFlying;
        private BombShootState pBombState;

    }
}

// End of file