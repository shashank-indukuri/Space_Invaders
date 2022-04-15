using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Ship : CategoryShip
    {
        // Constructor
        public Ship(Name name, Sprite.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ShipType.Ship)
        {
            x = posX;
            y = posY;

            shipSpeed = 3.0f;
            pMovement = null;
            pShoot = null;
        }

        // Methods

        public void MoveRight()
        {
            pMovement.MoveRight(this);
        }

        public void MoveLeft()
        {
            pMovement.MoveLeft(this);
        }

        public void ShootMissile()
        {
            pShoot.ShootMissile(this);
        }

        public void SetState(ShipManager.MovementState state)
        {
            pMovement = ShipManager.GetState(state);
        }

        public void SetState(ShipManager.ShootState state)
        {
             pShoot = ShipManager.GetState(state);
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
            base.Resurrect();
            this.poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        // Overriding methods

        public override void Update()
        {
            base.Update();
        }

        public override void VisitBumperGroup(WallBumperGroup wbGroup)
        {
            // Ship vs BumperGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(wbGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitBumperLeft(WallBumperLeft wbLeft)
        {
            // Ship vs BumperLeft
            //Debug.WriteLine("\ncollide: {0} with {1}", this, aGroup);
            //Debug.WriteLine("               --->DONE<----");

            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(wbLeft, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBumperRight(WallBumperRight wbRight)
        {
            // Ship vs BumperRight
            //Debug.WriteLine("\ncollide: {0} with {1}", this, aGroup);
            //Debug.WriteLine("               --->DONE<----");

            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(wbRight, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBombGroup(BombGroup bGroup)
        {
            // Ship vs BombGroup
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(bGroup);
            CollisionPair.CollidePair(pGameObj, this);
        }

        public override void VisitBomb(Bomb bomb)
        {
            // Ship vs Bomb
            //Debug.WriteLine("\ncollide: {0} with {1}", this, aGroup);
            //Debug.WriteLine("               --->DONE<----");

            CollisionPair pColPair = CollisionPairManager.GetCurrentColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(bomb, this);
            pColPair.NotifyListeners();
        }

        public override void Accept(CollisionVistor other)
        {
            // Call the VisitShip method
            other.VisitShip(this);
        }

        // Data
        public float shipSpeed;
        private ShipMovementState pMovement;
        private ShipShootState pShoot;
    }
}

// End of file