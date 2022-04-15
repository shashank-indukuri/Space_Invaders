using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        // Constructor
        private ShieldFactory()
        {
            this.pSpriteBatch = null;
            this.pSpriteBoxBatch = null;
            this.pTree = null;
        }

        ~ShieldFactory()
        {

        }


        // Private Methods
        private void PrivSetValues(SpriteBatch.Name spriteBatchName, SpriteBatch.Name spriteBBoxName, Composite pTree)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pSpriteBoxBatch = SpriteBatchManager.Find(spriteBBoxName);
            Debug.Assert(this.pSpriteBoxBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }
        private void PrivSetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }

        private GameObject PrivCreate(CategoryShield.ShieldType type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;
            GameObjectNode pGameObjNode = GhostManager.Find(gameName);

            if (pGameObjNode != null)
            {
                pShield = pGameObjNode.pGameObject;
                GhostManager.Remove(pGameObjNode);

                switch (type)
                {
                    case CategoryShield.ShieldType.Brick:
                    case CategoryShield.ShieldType.LeftTop1:
                    case CategoryShield.ShieldType.LeftTop0:
                    case CategoryShield.ShieldType.LeftBottom:
                    case CategoryShield.ShieldType.RightTop1:
                    case CategoryShield.ShieldType.RightTop0:
                    case CategoryShield.ShieldType.RightBottom:
                        ((ShieldBrick)pShield).Resurrect(posX, posY);
                        break;

                    case CategoryShield.ShieldType.Group:
                        Debug.Assert(false);
                        break;

                    case CategoryShield.ShieldType.Grid:
                        ((ShieldGrid)pShield).Resurrect(posX, posY);
                        break;

                    case CategoryShield.ShieldType.Column:
                        ((ShieldColumn)pShield).Resurrect(posX, posY); ;
                        break;

                    default:
                        // something is wrong
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case CategoryShield.ShieldType.Brick:
                        pShield = new ShieldBrick(gameName, Sprite.Name.Brick, posX, posY);
                        break;

                    case CategoryShield.ShieldType.LeftTop1:
                        pShield = new ShieldBrick(gameName, Sprite.Name.BrickLeftTop1, posX, posY);
                        break;

                    case CategoryShield.ShieldType.LeftTop0:
                        pShield = new ShieldBrick(gameName, Sprite.Name.BrickLeftTop0, posX, posY);
                        break;

                    case CategoryShield.ShieldType.LeftBottom:
                        pShield = new ShieldBrick(gameName, Sprite.Name.BrickLeftBottom, posX, posY);
                        break;

                    case CategoryShield.ShieldType.RightTop1:
                        pShield = new ShieldBrick(gameName, Sprite.Name.BrickRightTop1, posX, posY);
                        break;

                    case CategoryShield.ShieldType.RightTop0:
                        pShield = new ShieldBrick(gameName, Sprite.Name.BrickRightTop0, posX, posY);
                        break;

                    case CategoryShield.ShieldType.RightBottom:
                        pShield = new ShieldBrick(gameName, Sprite.Name.BrickRightBottom, posX, posY);
                        break;

                    case CategoryShield.ShieldType.Group:
                        pShield = new ShieldGroup(gameName, Sprite.Name.NullObject, posX, posY);
                        pShield.SetCollisionBoxColor(0.0f, 0.0f, 1.0f);
                        Debug.Assert(false);
                        break;

                    case CategoryShield.ShieldType.Grid:
                        pShield = new ShieldGrid(gameName, Sprite.Name.NullObject, posX, posY);
                        pShield.SetCollisionBoxColor(0.0f, 0.0f, 1.0f);
                        break;

                    case CategoryShield.ShieldType.Column:
                        pShield = new ShieldColumn(gameName, Sprite.Name.NullObject, posX, posY);
                        pShield.SetCollisionBoxColor(1.0f, 0.0f, 0.0f);
                        break;

                    default:
                        // No type found
                        Debug.Assert(false);
                        break;
                }
            }

            // Add to the tree
            this.pTree.Add(pShield);

            // Add to the GameObject
            pShield.LinkSprite(this.pSpriteBatch);
            pShield.LinkCollisionSprite(this.pSpriteBoxBatch);

            return pShield;
        }

        // Static Methods
        public static GameObject CreateSingleShield(float startX, float startY)
        {
            ShieldFactory pShieldFactory = ShieldFactory.PrivInstance();

            ShieldGroup pShieldGroup = (ShieldGroup)GameObjectNodeManager.Find(GameObject.Name.ShieldGroup);

            if (pShieldGroup == null)
            {
                pShieldGroup = new ShieldGroup(GameObject.Name.ShieldGroup, Sprite.Name.NullObject, 0.0f, 0.0f);
                GameObjectNodeManager.Link(pShieldGroup);
            }

            pShieldFactory.PrivSetValues(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldGroup);

            // load by column
            {
                int j = 0;

                GameObject pColumn;
                GameObject pGrid;

                pShieldFactory.PrivSetParent(pShieldGroup);

                pGrid = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Grid, GameObject.Name.ShieldGrid);

                pShieldFactory.PrivSetParent(pGrid);

                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + j++);

                pShieldFactory.PrivSetParent(pColumn);

                float start_x = startX;
                float start_y = startY;
                float off_x = 0;
                float brickWidth = 20.0f;
                float brickHeight = 10.0f;

                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 7 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.LeftTop1, GameObject.Name.ShieldLeftTop1, start_x, start_y + 8 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.LeftTop0, GameObject.Name.ShieldLeftTop0, start_x, start_y + 9 * brickHeight);

                pShieldFactory.PrivSetParent(pGrid);
                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + j++);

                pShieldFactory.PrivSetParent(pColumn);

                off_x += brickWidth;
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                pShieldFactory.PrivSetParent(pGrid);
                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + j++);

                pShieldFactory.PrivSetParent(pColumn);

                off_x += brickWidth;
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.LeftBottom, GameObject.Name.ShieldLeftBottom, start_x + off_x, start_y + 2 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                pShieldFactory.PrivSetParent(pGrid);
                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + j++);

                pShieldFactory.PrivSetParent(pColumn);

                off_x += brickWidth;
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                pShieldFactory.PrivSetParent(pGrid);
                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + j++);

                pShieldFactory.PrivSetParent(pColumn);

                off_x += brickWidth;
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.RightBottom, GameObject.Name.ShieldRightBottom, start_x + off_x, start_y + 2 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                pShieldFactory.PrivSetParent(pGrid);
                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + j++);

                pShieldFactory.PrivSetParent(pColumn);

                off_x += brickWidth;
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                pShieldFactory.PrivSetParent(pGrid);
                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + j++);

                pShieldFactory.PrivSetParent(pColumn);

                off_x += brickWidth;
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.RightTop1, GameObject.Name.ShieldRightTop1, start_x + off_x, start_y + 8 * brickHeight);
                pShieldFactory.PrivCreate(CategoryShield.ShieldType.RightTop0, GameObject.Name.ShieldRightTop0, start_x + off_x, start_y + 9 * brickHeight);

            }

            return pShieldGroup;
        }

        public static void CreateBottomShield()
        {
            ShieldFactory pShieldFactory = ShieldFactory.PrivInstance();

            ShieldGroup pShieldGroup = (ShieldGroup)GameObjectNodeManager.Find(GameObject.Name.ShieldGroup);

            if (pShieldGroup == null)
            {
                pShieldGroup = new ShieldGroup(GameObject.Name.ShieldGroup, Sprite.Name.NullObject, 0.0f, 0.0f);
                GameObjectNodeManager.Link(pShieldGroup);
            }

            pShieldFactory.PrivSetValues(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldGroup);
            GameObject pColumn;
            GameObject pGrid;

            pShieldFactory.PrivSetParent(pShieldGroup);

            pGrid = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Grid, GameObject.Name.ShieldGrid);

            float start_x = 50;
            float start_y = 80;
            float off_x = 0;
            float brickWidth = 20.0f;

            for (int i = 0; i < 100; i++)
            {
                pShieldFactory.PrivSetParent(pGrid);

                pColumn = pShieldFactory.PrivCreate(CategoryShield.ShieldType.Column, GameObject.Name.ShieldColumn0 + i++);

                pShieldFactory.PrivSetParent(pColumn);

                pShieldFactory.PrivCreate(CategoryShield.ShieldType.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
                off_x += brickWidth;
            }
        }

        public static void RemoveShields(GameObject pShieldGroup)
        {
            //GameObject pShieldGrid = (GameObject)ForwardCompositeIterator.GetChildNode(pShieldGroup);
            ReverseCompositeIterator pReverseItr = new ReverseCompositeIterator(pShieldGroup);

            Component pNode = pReverseItr.First();

            // Walk through the nodes
            while (!pReverseItr.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;

                if (pGameObj.name == GameObject.Name.ShieldGroup)
                {
                    break;
                }

                pGameObj.Remove();

                pNode = pReverseItr.Next();
            }
        }

        private static ShieldFactory PrivInstance()
        {
            if (psInstance == null)
            {
                ShieldFactory.psInstance = new ShieldFactory();
            }

            Debug.Assert(psInstance != null);

            return psInstance;
        }

        // Data
        private static ShieldFactory psInstance = null;
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pSpriteBoxBatch;
        private Composite pTree;
    }
}

// End of file