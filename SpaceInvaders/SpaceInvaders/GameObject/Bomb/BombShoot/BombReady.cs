using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombReady : BombShootState
    {
        public override void ShootBomb(GameObject pAlienCol)
        {
            CollisionRect pColRect = pAlienCol.poCollisionObj.poCollisionRect;
            float x = pColRect.x + (pColRect.width / 2);
            float y = pColRect.y - (pColRect.height / 2);

            BombFallStrategy pStrategy = null;

            // Get the random number
            int randomBomb = SpaceInvaders.pRandom.Next(0, 3);

            // Apply the random bomb from three types
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

            Bomb pBomb = new Bomb(GameObject.Name.Bomb, pAlienCol.name,(Sprite.Name)randomBomb, pStrategy, x, y);

            SpriteBatch pSBAliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(pSBAliens != null);

            SpriteBatch pSBBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(pSBBoxes != null);

            pBomb.LinkCollisionSprite(pSBBoxes);
            pBomb.LinkSprite(pSBAliens);

            // Add to GameObject Tree
            GameObject pBombGroup = GameObjectNodeManager.Find(GameObject.Name.BombGroup);
            Debug.Assert(pBombGroup != null);

            pBombGroup.Add(pBomb);

            ((AlienColumn)pAlienCol).SetState(AlienColumn.BombState.BombFlying);
        }
    }
}
