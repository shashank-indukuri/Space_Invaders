using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Bomb : CategoryBomb
    {
        // Constructor
        public Bomb(GameObject.Name name, GameObject.Name colName,Sprite.Name spriteName, BombFallStrategy pStrategy, float posX, float posY)
            : base(name, spriteName, posX, posY, CategoryBomb.BombType.Bomb)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 3.0f;

            this.colName = colName;

            Debug.Assert(pStrategy != null);
            this.pFallStrategy = pStrategy;

            this.pFallStrategy.Reset(this.y);

            this.poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~Bomb()
        {
        }

        // Methods

        public void SetDelta(float delta)
        {
            this.delta = delta;
        }

        public float FetchBoundingBoxHeight()
        {
            return this.poCollisionObj.poCollisionRect.height;
        }

        public void MultiplySpriteScale(float sx, float sy)
        {
            Debug.Assert(this.pSpriteProxy != null);

            this.pSpriteProxy.sx *= sx;
            this.pSpriteProxy.sy *= sy;
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public void Reset()
        {
            this.y = 700.0f;
            this.pFallStrategy.Reset(this.y);
        }

        public GameObject.Name GetColName()
        {
            return colName;
        }

        // Overriding Methods

        public override void Remove()
        {
            // Update the size to zero as it has parent group
            this.poCollisionObj.poCollisionRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent bomb group
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // remove it
            base.Remove();
        }

        public override void Update()
        {
            base.Update();
            this.y -= delta;

            // Call strategy pattern method
            this.pFallStrategy.BombFall(this);
        }

        public override void Accept(CollisionVistor other)
        {
            // Call the VisitBomb method           
            other.VisitBomb(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs Bomb
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(m);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Bomb
            //Debug.WriteLine("Missile vs Wall Top");
            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }


        // Data
        public float delta;
        private BombFallStrategy pFallStrategy;
        private GameObject.Name colName;
    }
}

// End of file