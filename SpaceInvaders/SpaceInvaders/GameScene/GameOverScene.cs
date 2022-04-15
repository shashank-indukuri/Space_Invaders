using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameOverScene : GameSceneState
    {
        // Constructor
        public GameOverScene()
        {
            this.Initialize();
        }

        // Private Methods
        private void LoadOnEntry()
        {
            // Add the fonts to the timer
            BannerTextFactory.LoadTexts("GAME OVER", 1.0f, 0.1f, 350, 800, 0.9019f, 0.0784f, 0.0784f);
            TimerEventManager.Add(TimerEvent.Name.SwiftScene, 4.0f, new SwiftSceneCommand(GameSceneContext.Scene.Home));

            // Set the initial state of the players to false
            Player pPlayer1 = PlayerManager.Find(Player.Name.Player1);
            pPlayer1.SetGameStatus(false);
            Player pPlayer2 = PlayerManager.Find(Player.Name.Player2);
            pPlayer2.SetGameStatus(false);
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

            SpriteBatch pSBTexts = SpriteBatchManager.Add(SpriteBatch.Name.Texts);

            Texture pTexture = TextureManager.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            this.poColPairManager = new CollisionPairManager(3, 1);
            CollisionPairManager.SetActiveCollisionManager(this.poColPairManager);
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
            CollisionPairManager.SetActiveCollisionManager(poColPairManager);
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
        public CollisionPairManager poColPairManager;
    }
}

// End of file