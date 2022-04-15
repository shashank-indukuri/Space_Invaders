using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObject : Component
    {
        // Enum
        public enum Name {
            AlienGroup,
            AlienGrid,
            AlienColumn,
            AlienColumn0,
            AlienColumn1,
            AlienColumn2,
            AlienColumn3,
            AlienColumn4,
            AlienColumn5,
            AlienColumn6,
            AlienColumn7,
            AlienColumn8,
            AlienColumn9,
            AlienColumn10,
            Octopus,
            OctopusExtend,
            Crab,
            CrabExtend,
            Squid,
            SquidExtend,
            Missile,
            MissileGroup,
            WallLeft,
            WallRight,
            WallTop,
            WallBottom,
            WallGroup,
            Ship,
            ShipGroup,
            UFOBomb,
            Bomb,
            BombGroup,
            ShieldGroup,
            ShieldGrid,
            ShieldColumn0,
            ShieldColumn1,
            ShieldColumn2,
            ShieldColumn3,
            ShieldColumn4,
            ShieldColumn5,
            ShieldColumn6,
            ShieldBrick,
            ShieldLeftTop0,
            ShieldLeftTop1,
            ShieldLeftBottom,
            ShieldRightTop0,
            ShieldRightTop1,
            ShieldRightBottom,

            UFO,
            UFOGroup,
            WallBumperGroup,
            WallBumperLeft,
            WallBumperRight,
            NullObject,

            Uninitialized
        }

        // Constructors
        public GameObject(Type type,Name gameObjectName, Sprite.Name pNameProxy)
            :base(type)
        {
            // Initializing the values
            name = gameObjectName;
            bMarkForDelete = false;

            this.spriteName = pNameProxy;
            SpriteProxy pSpriteProxy = SpriteProxyManager.Add(pNameProxy);
            Debug.Assert(pSpriteProxy != null);
            this.pSpriteProxy = pSpriteProxy;

            x = 0.0f;
            y = 0.0f;

            // Initializing the Collision Object
            this.poCollisionObj = new CollisionObject(this.pSpriteProxy);
            Debug.Assert(this.poCollisionObj != null);
        }

        public GameObject(Type type, Name gameObjectName, Sprite.Name spriteName, float x, float y)
            :base(type)
        {
            //Setting the values of Sprite and creating the Sprite proxy
            name = gameObjectName;
            bMarkForDelete = false;

            this.spriteName = spriteName;
            pSpriteProxy = SpriteProxyManager.Add(spriteName);

            this.x = x;
            this.y = y;

            // Initializing the Collision Object
            // Owned by Game Object. Goes to GhostManager
            this.poCollisionObj = new CollisionObject(pSpriteProxy);
            Debug.Assert(this.poCollisionObj != null);
        }

        // Methods
        public override void Resurrect()
        {
            this.bMarkForDelete = false;
            Debug.Assert(pSpriteProxy != null);
            Debug.Assert(poCollisionObj != null);

            // Resucrrect the collObj from GhostManager
            this.poCollisionObj.Resurrect(pSpriteProxy);
            Debug.Assert(this.poCollisionObj != null);

            base.Resurrect();
        }
        public virtual void Remove()
        {
            //Debug.WriteLine("REMOVE: {0}", this);
            // Delete the GameObjects

            // Remove from SpriteBatch

            // Find the SpriteNode
            Debug.Assert(pSpriteProxy != null);
            SpriteNode pSpriteNode = pSpriteProxy.GetSpriteNode();

            // Remove it from the manager
            Debug.Assert(pSpriteNode != null);
            SpriteBatchManager.Remove(pSpriteNode);

            // Remove collision sprite from spriteBatch

            Debug.Assert(poCollisionObj != null);
            Debug.Assert(poCollisionObj.pCollisionSBoxProxy != null);
            pSpriteNode = poCollisionObj.pCollisionSBoxProxy.GetSpriteNode();

            Debug.Assert(pSpriteNode != null);
            SpriteBatchManager.Remove(pSpriteNode);

            // Remove from GameObjectManager
            GameObjectNodeManager.Remove(this);

            // Add to Ghost manager
            GhostManager.Link(this);

        }
        public virtual void Update()
        {
            Debug.Assert(pSpriteProxy != null);

            // Updating the Sprite proxy actual values with x and y
            pSpriteProxy.x = x;
            pSpriteProxy.y = y;

            // Updating the Collision Rect and SpriteBox values
            Debug.Assert(poCollisionObj != null);
            poCollisionObj.Update(x, y);

            // Updating the Collision Sprite actual values in SpriteBoxProxy
            Debug.Assert(poCollisionObj.pCollisionSBoxProxy != null);
            poCollisionObj.pCollisionSBoxProxy.Update();
        }

        protected void BaseBoundingBoxUpdate(Component pStart)
        {
            GameObject pGameObj = (GameObject)pStart;

            // Create a copy
            CollisionRect pCollisionTotal = this.poCollisionObj.poCollisionRect;

            // First child
            pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(pGameObj);

            if (pGameObj != null)
            {
                // Set the first child in the column to the pCollisionTotal
                pCollisionTotal.Set(pGameObj.poCollisionObj.poCollisionRect);

                // Walk through the sibilings
                while (pGameObj != null)
                {
                    // Inside Union the next GameObject and the values are updated
                    pCollisionTotal.Union(pGameObj.poCollisionObj.poCollisionRect);

                    // Go to next sibiling node
                    pGameObj = (GameObject)ForwardCompositeIterator.GetSiblingNode(pGameObj);
                }

                //Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", pCollisionTotal.x, pCollisionTotal.y, pCollisionTotal.width, pCollisionTotal.height);
                // Transfer to the game object its center
                x = poCollisionObj.poCollisionRect.x;
                y = poCollisionObj.poCollisionRect.y;
            }
            //no children, clear the rect
            else
            {
                poCollisionObj.poCollisionRect.Clear();
            }
        }

        public void LinkCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(poCollisionObj != null);

            // Link the SpriteBoxProxy to SpriteBatch
            pSpriteBatch.Link(poCollisionObj.pCollisionSBoxProxy);
        }
        public void LinkSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);

            // Link the SpriteProxy to SpriteBatch
            pSpriteBatch.Link(pSpriteProxy);
        }

        public void SetCollisionBoxColor(float red, float green, float blue)
        {
            Debug.Assert(this.poCollisionObj != null);
            Debug.Assert(this.poCollisionObj.pCollisionSBoxProxy != null);

            this.poCollisionObj.pCollisionSBoxProxy.SetColor(red, green, blue);
        }

        public CollisionObject GetColObject()
        {
            Debug.Assert(poCollisionObj != null);

            // return the collision object
            return poCollisionObj;
        }

        // Overriding methods
        public override void Dump()
        {
            // Data:
            Debug.WriteLine("\t\t\t       name: {0} ({1})", name, GetHashCode());

            if (this.pSpriteProxy != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", pSpriteProxy.proxyName);
                Debug.WriteLine("\t\t    pRealSprite: {0}", pSpriteProxy.pSprite.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", x, y);
            Debug.WriteLine("\t\t\t poCollisionObj: {0}, {1}", poCollisionObj.pCollisionSBoxProxy.x, poCollisionObj.pCollisionSBoxProxy.y);
            Debug.WriteLine("\t\t\t poCollisionObj width height: {0}, {1}", poCollisionObj.poCollisionRect.width, poCollisionObj.poCollisionRect.width);
            base.Dump();
        }

        public override object GetName()
        {
            // Returnt the Game Object name
            return name;
        }

        // Data
        public Name name;
        public Sprite.Name spriteName;
        public SpriteProxy pSpriteProxy;
        public CollisionObject poCollisionObj;
        public float x;
        public float y;
        public bool bMarkForDelete;
    }
}

// End of file
