using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        // Constructor
        private AlienFactory()
        {
            this.pSpriteBatch = null;
            this.pSpriteBoxBatch = null;
            this.pTree = null;
        }

        ~AlienFactory()
        {

        }

        // Private Methods
        private void PrivSetValues(SpriteBatch.Name name, SpriteBatch.Name spriteBBoxName, Composite pTree)
        {
            SpriteBatch pSpriteB = SpriteBatchManager.Find(name);
            Debug.Assert(pSpriteB != null);

            pSpriteBatch = pSpriteB;

            SpriteBatch pSpriteBBox = SpriteBatchManager.Find(spriteBBoxName);
            Debug.Assert(pSpriteB != null);

            pSpriteBoxBatch = pSpriteBBox;

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }
        private void PrivSetParent(GameObject pParentNode)
        {
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }

        private GameObject PrivCreate(CategoryAlien.Kind kind, GameObject.Name gameName, float x = 0.0f, float y = 0.0f)
        {
            GameObject pAlien = null;
            GameObjectNode pGameObjNode = GhostManager.Find(gameName);
            if (pGameObjNode != null)
            {
                pAlien = pGameObjNode.pGameObject;
                GhostManager.Remove(pGameObjNode);

                //GhostManager.Dump();
                Sprite pSprite = null;
                switch (kind)
                {
                    case CategoryAlien.Kind.Squid:
                        pSprite = SpriteManager.Find(Sprite.Name.Squid);
                        pAlien.pSpriteProxy.pSprite = pSprite;
                        ((Squid)pAlien).Resurrect(x, y);
                        break;

                    case CategoryAlien.Kind.Crab:
                        pSprite = SpriteManager.Find(Sprite.Name.Crab);
                        pAlien.pSpriteProxy.pSprite = pSprite;
                        ((Crab)pAlien).Resurrect(x, y);
                        break;

                    case CategoryAlien.Kind.Octopus:
                        pSprite = SpriteManager.Find(Sprite.Name.Octopus);
                        pAlien.pSpriteProxy.pSprite = pSprite;
                        ((Octopus)pAlien).Resurrect(x, y);
                        break;

                    case CategoryAlien.Kind.Column:
                        ((AlienColumn)pAlien).Resurrect(x, y);
                        break;

                    case CategoryAlien.Kind.Grid:
                        ((AlienGrid)pAlien).Resurrect(x, y);
                        break;

                    case CategoryAlien.Kind.Group:
                        Debug.Assert(false);
                        break;

                    default:
                        // No type found
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {
                switch (kind)
                {
                    case CategoryAlien.Kind.Squid:
                        // LTN - GameObjectNodeManager
                        pAlien = new Squid(Sprite.Name.Squid, x, y);
                        break;

                    case CategoryAlien.Kind.Crab:
                        // LTN - GameObjectNodeManager
                        pAlien = new Crab(Sprite.Name.Crab, x, y);
                        break;

                    case CategoryAlien.Kind.Octopus:
                        // LTN - GameObjectNodeManager
                        pAlien = new Octopus(Sprite.Name.Octopus, x, y);
                        break;

                    case CategoryAlien.Kind.Column:
                        // LTN - GameObjectNodeManager
                        pAlien = new AlienColumn(gameName, Sprite.Name.NullObject);
                        break;

                    case CategoryAlien.Kind.Grid:
                        // LTN - GameObjectNodeManager
                        pAlien = new AlienGrid(gameName, Sprite.Name.NullObject);
                        break;

                    case CategoryAlien.Kind.Group:
                        // LTN - GameObjectNodeManager
                        pAlien = new AlienGroup();
                        break;

                    default:
                        // No type found
                        Debug.Assert(false);
                        break;
                }
            }

            pTree.Add(pAlien);

            // Add to the GameObject
            pAlien.LinkCollisionSprite(this.pSpriteBoxBatch);
            pAlien.LinkSprite(this.pSpriteBatch);

            return pAlien;
        }

        // Static Methods
        public static GameObject CreateAlienGrid(float start = 800.0f)
        {
            AlienFactory pAlienFactory = AlienFactory.PrivInstance();

            float startLocation = start;

            AlienGroup pAlienGroup = (AlienGroup)GameObjectNodeManager.Find(GameObject.Name.AlienGroup);

            if (pAlienGroup == null)
            {
                pAlienGroup = new AlienGroup(GameObject.Name.AlienGroup, Sprite.Name.NullObject, 0.0f, 0.0f);
                GameObjectNodeManager.Link(pAlienGroup);
            }

            pAlienFactory.PrivSetValues(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes, pAlienGroup);
            GameObject pColumn;
            GameObject pGrid;

            pAlienFactory.PrivSetParent(pAlienGroup);

            pGrid = pAlienFactory.PrivCreate(CategoryAlien.Kind.Grid, GameObject.Name.AlienGrid);

            for (int i = 0; i < 11; i++)
            {
                pAlienFactory.PrivSetParent(pGrid);

                pColumn = pAlienFactory.PrivCreate(CategoryAlien.Kind.Column, GameObject.Name.AlienColumn0 + i);

                pAlienFactory.PrivSetParent(pColumn);

                // Add Sprite proxies to the column
                pAlienFactory.PrivCreate(CategoryAlien.Kind.Squid, GameObject.Name.Squid, 100.0f + (i * 66.0f), startLocation);
                pAlienFactory.PrivCreate(CategoryAlien.Kind.Crab, GameObject.Name.Crab, 100.0f + (i * 66.0f), startLocation - 66.0f);
                pAlienFactory.PrivCreate(CategoryAlien.Kind.Crab, GameObject.Name.Crab, 100.0f + (i * 66.0f), startLocation - 132.0f);
                pAlienFactory.PrivCreate(CategoryAlien.Kind.Octopus, GameObject.Name.Octopus, 100.0f + (i * 66.0f), startLocation - 198.0f);
                pAlienFactory.PrivCreate(CategoryAlien.Kind.Octopus, GameObject.Name.Octopus, 100.0f + (i * 66.0f), startLocation - 264.0f);
            }

            return pAlienGroup;
        }

        public static void RemoveAliens(GameObject pAlienGroup)
        {
            GameObject pAlienGrid = (GameObject)ForwardCompositeIterator.GetChildNode(pAlienGroup);

            ReverseCompositeIterator pReverseItr = new ReverseCompositeIterator(pAlienGrid);

            Component pNode = pReverseItr.First();

            // Walk through the nodes
            while (!pReverseItr.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;

                pGameObj.Remove();

                pNode = pReverseItr.Next();
            }
        }

        private static AlienFactory PrivInstance()
        {
            if (psInstance == null)
            {
                AlienFactory.psInstance = new AlienFactory();
            }

            Debug.Assert(psInstance != null);

            return psInstance;
        }

        // Data
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pSpriteBoxBatch;
        private static AlienFactory psInstance = null;
        private Composite pTree;
    }
}

// End of file