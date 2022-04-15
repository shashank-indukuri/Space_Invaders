using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RandomUFOBombCommand : BaseCommand
    {
        // Constructor
        public RandomUFOBombCommand()
        {
            this.pRandom = SpaceInvaders.pRandom;
        }

        // Overiridng Methods
        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);

            GameObject pBombGroup = GameObjectNodeManager.Find(GameObject.Name.BombGroup);
            Debug.Assert(pBombGroup != null);

            GameObject pUFOGroup = GameObjectNodeManager.Find(GameObject.Name.UFOGroup);
            Debug.Assert(pUFOGroup != null);

            SpriteBatch pSBAliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(pSBAliens != null);

            SpriteBatch pSBBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(pSBBoxes != null);

            GameObject pUFO = (GameObject)ForwardCompositeIterator.GetChildNode(pUFOGroup);

            if (pUFO != null)
            {
                // Create Bomb
                CollisionRect pColRect = pUFO.poCollisionObj.poCollisionRect;
                float x = pColRect.x + (pColRect.width / 2);
                float y = pColRect.y - (pColRect.height / 2);

                BombFallStrategy pStrategy = null;
                int randomBomb = pRandom.Next(0, 3);

                switch (randomBomb)
                {
                    case 0:
                        pStrategy = new StraightFall();
                        break;

                    case 1:
                        pStrategy = new ZigZagFall();
                        break;

                    case 2:
                        pStrategy = new DaggerFall();
                        break;

                    default:
                        break;
                }

                Bomb pBomb = new Bomb(GameObject.Name.UFOBomb, pUFO.name,(Sprite.Name)randomBomb, pStrategy, x, y);
                //     Debug.WriteLine("----x:{0}", value);

                pBomb.LinkCollisionSprite(pSBBoxes);
                pBomb.LinkSprite(pSBAliens);

                // Add to GameObject Tree
                pBombGroup.Add(pBomb);
            }
        }

        // Data
        Random pRandom;
    }
}

// End of file