using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AddSplatObserver : CollisionObserver
    {
        public AddSplatObserver()
        {
            pSplat = null;
        }

        // Overriding methods
        public override void Notify()
        {
            // Delete Alien
            //Debug.WriteLine("RemoveAlienObserver: {0} {1}", this.pSubject.pGameObjA, this.pSubject.pGameObjB);

            GameObject pSprite = pSubject.pGameObjB;

            if (pSprite.name == GameObject.Name.WallTop || pSprite.name == GameObject.Name.WallBottom || pSprite.name == GameObject.Name.ShieldBrick)
            {
                pSprite = pSubject.pGameObjA;
            }

            float x = pSprite.pSpriteProxy.x;
            float y = pSprite.pSpriteProxy.y;

            switch (pSprite.name)
            {
                case GameObject.Name.Squid:
                case GameObject.Name.Crab:
                case GameObject.Name.Octopus:
                    pSplat = SpriteManager.AddSprite(Sprite.Name.Splat, Image.Name.Splat, x, y, 45.0f, 33.0f);
                    break;

                case GameObject.Name.Missile:
                    pSplat = SpriteManager.AddSprite(Sprite.Name.MissileSplat, Image.Name.MissileSplat, x, y, 45.0f, 33.0f, new Azul.Color(1.0f, 0.0f, 0.0f));
                    break;

                case GameObject.Name.Bomb:
                    pSplat = SpriteManager.AddSprite(Sprite.Name.BombSplat, Image.Name.BombSplat, x, y, 45.0f, 33.0f);
                    break;

                case GameObject.Name.UFO:
                    pSplat = SpriteManager.AddSprite(Sprite.Name.UFOSplat, Image.Name.UFOSplat, x, y, 45.0f, 33.0f, new Azul.Color(1.0f, 0.0f, 0.0f));
                    break;

                case GameObject.Name.Ship:
                    pSplat = SpriteManager.AddSprite(Sprite.Name.ShipExplosion, Image.Name.ShipExplosion, x, y, 45.0f, 33.0f);
                    break;
            }

            //Debug.WriteLine("RemoveAlienObserver: --> delete missile {0}", pSprite);
            SpriteBatch pSBAliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);

            pSBAliens.Link(pSplat);

            RemoveSplatCommand pRmvSplatCommand = new RemoveSplatCommand(this);
            TimerEventManager.Add(TimerEvent.Name.Splat, 0.5f, pRmvSplatCommand);
        }

        public override void RemoveSplat()
        {
            Debug.Assert(pSplat != null);
            SpriteNode pSpriteNode = pSplat.GetSpriteNode();

            // Remove it from the manager
            Debug.Assert(pSpriteNode != null);
            SpriteBatchManager.Remove(pSpriteNode);
        }



        // Data
        Sprite pSplat;
    }
}

// End of file