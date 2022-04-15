using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipManager
    {
        // Enum
        public enum ShootState
        {
            Ready,
            MissileFlying
        }

        public enum MovementState
        {
            MovementLeft,
            MovementRight,
            MovementBoth,
        }

        // Constructor
        public ShipManager()
        {
            // Instantiates the states
            pStateReady = new ShipReady();
            pStateMissileFlying = new ShipMissileFlying();

            pMoveBothState = new ShipMovemenBoth();
            pMoveLeftState = new ShipMovementLeft();
            pMoveRightState = new ShipMovementRight();

            // set active
            pShip = null;
            pMissile = null;
            psActiveInstance = null;
        }

        // Static methods
        public static void Create()
        {
            // Check if it is first time
            Debug.Assert(psInstance == null);

            if (psInstance == null)
            {
                psInstance = new ShipManager();
            }

            Debug.Assert(psInstance != null);
        }


        private static ShipManager PrivGetInstance()
        {
            Debug.Assert(psInstance != null);

            // return the instance
            return psInstance;
        }

        public static Ship GetShip()
        {
            ShipManager pShipManager = psActiveInstance;


            Debug.Assert(pShipManager != null);
            Debug.Assert(pShipManager.pShip != null);

            // return the ship
            return pShipManager.pShip;
        }

        public static ShipMovementState GetState(MovementState state)
        {
            ShipManager pShipManager = psActiveInstance;
            Debug.Assert(pShipManager != null);

            ShipMovementState pShipMoveState = null;
            // Ship current state
            switch (state)
            {
                case MovementState.MovementBoth:
                    pShipMoveState = pShipManager.pMoveBothState;
                    break;

                case MovementState.MovementLeft:
                    pShipMoveState = pShipManager.pMoveLeftState;
                    break;

                case MovementState.MovementRight:
                    pShipMoveState = pShipManager.pMoveRightState;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            // return the state
            return pShipMoveState;
        }

        public static ShipShootState GetState(ShootState state)
        {
            ShipManager pShipManager = psActiveInstance;
            Debug.Assert(pShipManager != null);

            ShipShootState pShipShootState = null;
            // Ship current state
            switch (state)
            {
                case ShootState.MissileFlying:
                    pShipShootState = pShipManager.pStateMissileFlying;
                    break;

                case ShootState.Ready:
                    pShipShootState = pShipManager.pStateReady;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            // return the state
            return pShipShootState;
        }

        public static Missile GetMissile()
        {
            ShipManager pShipManager = psActiveInstance;

            Debug.Assert(pShipManager != null);
            Debug.Assert(pShipManager.pMissile != null);

            // return the missile
            return pShipManager.pMissile;
        }


        public static Missile ActivateMissile()
        {
            ShipManager pShipManager = psActiveInstance;
            Debug.Assert(pShipManager != null);

            // copy over safe copy need to cleanup
            // By using GhostManager, retreving the node
            Missile pMissile = null;
            GameObjectNode pGameObjNode = GhostManager.Find(GameObject.Name.Missile);
            if (pGameObjNode == null)
            {
                pMissile = new Missile(Sprite.Name.Missile, 400, 100);
            }
            else
            {
                // Recycle it
                pMissile = (Missile)pGameObjNode.pGameObject;
                GhostManager.Remove(pGameObjNode);
                Sprite pSprite = SpriteManager.Find(Sprite.Name.Missile);

                pMissile.pSpriteProxy.pSprite = pSprite;
                pMissile.Resurrect(400, 100);
                //GhostManager.Dump();
            }
            pShipManager.pMissile = pMissile;

            // Attached to SpriteBatches
            SpriteBatch pSBAliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSBBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            pMissile.LinkCollisionSprite(pSBBoxes);
            pMissile.LinkSprite(pSBAliens);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectNodeManager.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject tree
            pMissileGroup.Add(pShipManager.pMissile);

            return pShipManager.pMissile;
        }


        public static Ship ActivateShip(float x = 200, float y = 120)
        {
            ShipManager pShipManager = psActiveInstance;
            Debug.Assert(pShipManager != null);

            // copy over safe copy
            Ship pShip = null;
            GameObjectNode pGameObjNode = GhostManager.Find(GameObject.Name.Ship);
            if (pGameObjNode == null)
            {
                // LTN - ShipManager
                pShip = new Ship(GameObject.Name.Ship, Sprite.Name.Ship, x, y);
            }
            else
            {
                // Recycle it
                pShip = (Ship)pGameObjNode.pGameObject;
                GhostManager.Remove(pGameObjNode);
                Sprite pSprite = SpriteManager.Find(Sprite.Name.Ship);

                pShip.pSpriteProxy.pSprite = pSprite;
                pShip.Resurrect(x, y);
                //GhostManager.Dump();
            }

            pShipManager.pShip = pShip;

            // Attach the sprite to the correct sprite batch
            SpriteBatch pSBAliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSBBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pShip.LinkSprite(pSBAliens);
            pShip.LinkCollisionSprite(pSBBoxes);

            // Attach the Ship to the ship group
            GameObject pShipGroup = GameObjectNodeManager.Find(GameObject.Name.ShipGroup);
            Debug.Assert(pShipGroup != null);

            // Add to GameObject tree
            pShipGroup.Add(pShipManager.pShip);

            // Setting the state
            pShip.SetState(MovementState.MovementBoth);
            pShip.SetState(ShootState.Ready);

            return pShipManager.pShip;
        }

        public static void SetActiveShip(ShipManager pShipMan)
        {
            ShipManager pShipManager = ShipManager.PrivGetInstance();
            Debug.Assert(pShipManager != null);

            Debug.Assert(pShipMan != null);
            ShipManager.psActiveInstance = pShipMan;
        }

        public static void RemoveShips(GameObject pShipGroup)
        {
            ReverseCompositeIterator pReverseItr = new ReverseCompositeIterator(pShipGroup);

            Component pNode = pReverseItr.First();

            // Walk through the nodes
            while (!pReverseItr.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;

                if ((GameObject.Name)pGameObj.GetName() != GameObject.Name.ShipGroup)
                {
                    pGameObj.Remove();
                }

                pNode = pReverseItr.Next();
            }
        }

        // Data
        private static ShipManager psInstance = null;
        private static ShipManager psActiveInstance = null;

        // Current
        private Ship pShip;
        private Missile pMissile;

        // Reference
        private ShipReady pStateReady;
        private ShipMissileFlying pStateMissileFlying;
        private ShipMovemenBoth pMoveBothState;
        private ShipMovementLeft pMoveLeftState;
        private ShipMovementRight pMoveRightState;
    }
}

// End of file