using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class HomeScene : GameSceneState
    {
        public HomeScene()
        {
            this.Initialize();
        }

        // Private Methods
        private void LoadOnEntry()
        {
            // Update the score
            FontManager.UpdateScore();

            // Add the fonts to the timer
            BannerTextFactory.LoadTexts("PLAY", 2.0f, 0.10f, 400, 800, 0.9f, 0.9f, 0.9f);
            BannerTextFactory.LoadTexts("SPACE  INVADERS", 4.0f, 0.10f, 300, 700, 0.9f, 0.9f, 0.9f);
            BannerTextFactory.LoadTexts("*SCORE ADVANCE TABLE*", 6.0f, 0.1f, 250, 600, 0.9f, 0.9f, 0.9f);
            TimerEventManager.Add(TimerEvent.Name.BannerAliens, 6.0f, new BannerAliensCommand());
            BannerTextFactory.LoadTexts("= ? MYSTERY", 8.0f, 0.10f, 360, 500, 0.9f, 0.9f, 0.9f);
            BannerTextFactory.LoadTexts("= 30 POINTS", 10.0f, 0.10f, 360, 450, 0.9f, 0.9f, 0.9f);
            BannerTextFactory.LoadTexts("= 20 POINTS", 12.0f, 0.10f, 360, 400, 0.9f, 0.9f, 0.9f);
            BannerTextFactory.LoadTexts("= 10 POINTS", 14.0f, 0.10f, 360, 350, 0.2f, 0.8f, 0.2f);
            BannerTextFactory.LoadTexts("PRESS 1 TO START", 15.0f, 0.10f, 280, 250, 0.9f, 0.9f, 0.9f);
            BannerTextFactory.LoadTexts("PRESS 2 TO START TWO PLAYER MODE", 16.0f, 0.10f, 180, 200, 0.9f, 0.9f, 0.9f);
        }

        // Overriding Methods
        public override void Initialize()
        {
            this.poSBManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActiveBatch(this.poSBManager);
            this.poFontManager = new FontManager(3, 1);
            FontManager.SetActiveFont(this.poFontManager);

            this.poTimerEventManager = new TimerEventManager(3, 1);
            TimerEventManager.SetActiveTimer(this.poTimerEventManager);

            SpriteManager.Add(Sprite.Name.BannerUFO, Image.Name.UFO, 330.0f, 500.0f, 49.0f, 33.0f, new Azul.Color(1.0f, 0.0f, 0.0f));
            SpriteManager.Add(Sprite.Name.BannerSquid, Image.Name.Squid, 330.0f, 450.0f, 33.0f, 33.0f);
            SpriteManager.Add(Sprite.Name.BannerCrab, Image.Name.Crab, 330.0f, 400.0f, 45.0f, 33.0f);
            SpriteManager.Add(Sprite.Name.BannerOctopus, Image.Name.Octopus, 330.0f, 350.0f, 49.0f, 33.0f, new Azul.Color(0.2f, 0.8f, 0.2f));

            SpriteBatch pSBTexts = SpriteBatchManager.Add(SpriteBatch.Name.Texts);
            SpriteBatch pSBAliens = SpriteBatchManager.Add(SpriteBatch.Name.Aliens);

            Texture pTexture = TextureManager.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            FontManager.Add(Font.Name.Score1Text, SpriteBatch.Name.Texts, "SCORE <1>", Glyph.Name.Consolas36pt, 50, 1000);
            FontManager.Add(Font.Name.Score1, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 80, 950);
            FontManager.Add(Font.Name.HiScoreText, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 370, 1000);
            FontManager.Add(Font.Name.HiScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 400, 950);
            FontManager.Add(Font.Name.Score2Text, SpriteBatch.Name.Texts, "SCORE <2>", Glyph.Name.Consolas36pt, 690, 1000);
            FontManager.Add(Font.Name.Score2, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 720, 950);
        }

        public override void Update(float currentTime)
        {
            InputManager.Update();
            // Update the current time
            GameSimulation.Update(currentTime);

            // Run on simulation
            if (GameSimulation.GetTimeStep() > 0.0f)
            {
                TimerEventManager.UpdateTimerEvents(GameSimulation.GetTotalTime());

                // Update the Game Objects
                GameObjectNodeManager.Update();

                CollisionPairManager.Process();

                DelayObjectManager.Process();
            }
        }

        public override void Draw()
        {
            SpriteBatchManager.Draw();
        }

        public override void Entering()
        {
            SpriteBatchManager.SetActiveBatch(poSBManager);
            FontManager.SetActiveFont(poFontManager);
            TimerEventManager.SetActiveTimer(poTimerEventManager);
            this.LoadOnEntry();

            // Update the timer events
            float time0 = GlobalClock.GetCurrentTime();
            float time1 = this.currentTimeAtPause;
            float delta = time0 - time1;
            TimerEventManager.PauseTimerEvents(delta);
        }

        public override void Leaving()
        {
            this.currentTimeAtPause = TimerEventManager.GetCurrentTime();
        }

        // Data
        public SpriteBatchManager poSBManager;
        public FontManager poFontManager;
        public TimerEventManager poTimerEventManager;
    }
}

// End of file