using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BannerAliensCommand : BaseCommand
    {
        // Constructor
        public BannerAliensCommand()
            : base()
        {

        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            SpriteBatch pSBAliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            Sprite pUFO = SpriteManager.Find(Sprite.Name.BannerUFO);
            Sprite pSquid = SpriteManager.Find(Sprite.Name.BannerSquid);
            Sprite pCrab = SpriteManager.Find(Sprite.Name.BannerCrab);
            Sprite pOctopus = SpriteManager.Find(Sprite.Name.BannerOctopus);

            pSBAliens.Link(pUFO);
            pSBAliens.Link(pSquid);
            pSBAliens.Link(pCrab);
            pSBAliens.Link(pOctopus);
        }
    }
}

// End of file
