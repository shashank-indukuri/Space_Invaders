using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Space Invaders");
            this.SetWidthHeight(896, 1024);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            // Loading Managers

            // Singleton
            TextureManager.Create();
            ImageManager.Create(1, 1);
            SpriteManager.Create();
            SpriteBoxManager.Create();
            SpriteBatchManager.Create();
            TimerEventManager.Create();
            HelperTimerManager.Create();
            SpriteProxyManager.Create();
            SpriteBoxProxyManager.Create();
            GameObjectNodeManager.Create();
            CollisionPairManager.Create();
            GameSimulation.Create();
            GhostManager.Create(1, 1);
            GlyphManager.Create(3, 1);
            FontManager.Create(1, 1);
            PlayerManager.Create(1, 1);
            SoundManager.Create(3, 1);
            ShipManager.Create();

            // Sounds
            SoundManager.Add(Sound.Source.FastInvader1, "fastinvader1.wav");
            SoundManager.Add(Sound.Source.FastInvader2, "fastinvader2.wav");
            SoundManager.Add(Sound.Source.FastInvader3, "fastinvader3.wav");
            SoundManager.Add(Sound.Source.FastInvader4, "fastinvader4.wav");

            // Textures
            TextureManager.Add(Texture.Name.SpaceInvaders, "SpaceInvaders.tga");
            TextureManager.Add(Texture.Name.Birds, "birds_N_shield.tga");
            TextureManager.Add(Texture.Name.Consolas36pt, "consolas36pt.tga");
            TextureManager.Add(Texture.Name.HotPink, "HotPink.tga");

            // Images
            ImageManager.Add(Image.Name.UFO, Texture.Name.SpaceInvaders, 99, 3, 16, 8);

            ImageManager.Add(Image.Name.SquidExtend, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageManager.Add(Image.Name.Squid, Texture.Name.SpaceInvaders, 72, 3, 8, 8);
            ImageManager.Add(Image.Name.CrabExtend, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageManager.Add(Image.Name.Crab, Texture.Name.SpaceInvaders, 47, 3, 11, 8);
            ImageManager.Add(Image.Name.OctopusExtend, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageManager.Add(Image.Name.Octopus, Texture.Name.SpaceInvaders, 18, 3, 12, 8);

            ImageManager.Add(Image.Name.Ship, Texture.Name.SpaceInvaders, 3, 14, 13, 8);
            ImageManager.Add(Image.Name.Missile, Texture.Name.SpaceInvaders, 3, 29, 1, 4);
            ImageManager.Add(Image.Name.HotPink, Texture.Name.HotPink, 0, 0, 128, 128);

            ImageManager.Add(Image.Name.ZigZagBomb, Texture.Name.SpaceInvaders, 18, 26, 3, 7);
            ImageManager.Add(Image.Name.DaggerBomb, Texture.Name.SpaceInvaders, 42, 27, 3, 6);
            ImageManager.Add(Image.Name.StraightBomb, Texture.Name.SpaceInvaders, 65, 26, 3, 7);

            ImageManager.Add(Image.Name.Brick, Texture.Name.Birds, 20, 210, 10, 5);
            ImageManager.Add(Image.Name.BrickLeftTop0, Texture.Name.Birds, 15, 180, 10, 5);
            ImageManager.Add(Image.Name.BrickLeftTop1, Texture.Name.Birds, 15, 185, 10, 5);
            ImageManager.Add(Image.Name.BrickLeftBottom, Texture.Name.Birds, 35, 215, 10, 5);
            ImageManager.Add(Image.Name.BrickRightTop0, Texture.Name.Birds, 75, 180, 10, 5);
            ImageManager.Add(Image.Name.BrickRightTop1, Texture.Name.Birds, 75, 185, 10, 5);
            ImageManager.Add(Image.Name.BrickRightBottom, Texture.Name.Birds, 55, 215, 10, 5);

            ImageManager.Add(Image.Name.Splat, Texture.Name.SpaceInvaders, 83, 3, 13, 8);
            ImageManager.Add(Image.Name.UFOSplat, Texture.Name.SpaceInvaders, 118, 3, 21, 8);
            ImageManager.Add(Image.Name.MissileSplat, Texture.Name.SpaceInvaders, 7, 25, 8, 8);
            ImageManager.Add(Image.Name.ShipExplosion, Texture.Name.SpaceInvaders, 19, 14, 16, 8);
            ImageManager.Add(Image.Name.BombSplat, Texture.Name.SpaceInvaders, 86, 25, 6, 8);


            //Sprites

            SpriteManager.Add(Sprite.Name.Squid, Image.Name.Squid, 100.0f, 100.0f, 33.0f, 33.0f);
            SpriteManager.Add(Sprite.Name.Crab, Image.Name.Crab, 100.0f, 300.0f, 45.0f, 33.0f);
            SpriteManager.Add(Sprite.Name.Octopus, Image.Name.Octopus, 100.0f, 200.0f, 49.0f, 33.0f);

            SpriteManager.Add(Sprite.Name.Missile, Image.Name.Missile, 0, 0, 2, 10);
            SpriteManager.Add(Sprite.Name.Ship, Image.Name.Ship, 500, 100, 80, 28);

            SpriteManager.Add(Sprite.Name.ZigZagBomb, Image.Name.ZigZagBomb, 200, 200, 15, 20);
            SpriteManager.Add(Sprite.Name.StraightBomb, Image.Name.StraightBomb, 100, 100, 15, 20);
            SpriteManager.Add(Sprite.Name.DaggerBomb, Image.Name.DaggerBomb, 100, 100, 15, 20);

            SpriteManager.Add(Sprite.Name.Brick, Image.Name.Brick, 50, 25, 20, 10);
            SpriteManager.Add(Sprite.Name.BrickLeftTop0, Image.Name.BrickLeftTop0, 50, 25, 20, 10);
            SpriteManager.Add(Sprite.Name.BrickLeftTop1, Image.Name.BrickLeftTop1, 50, 25, 20, 10);
            SpriteManager.Add(Sprite.Name.BrickLeftBottom, Image.Name.BrickLeftBottom, 50, 25, 20, 10);
            SpriteManager.Add(Sprite.Name.BrickRightTop0, Image.Name.BrickRightTop0, 50, 25, 20, 10);
            SpriteManager.Add(Sprite.Name.BrickRightTop1, Image.Name.BrickRightTop1, 50, 25, 20, 10);
            SpriteManager.Add(Sprite.Name.BrickRightBottom, Image.Name.BrickRightBottom, 50, 25, 20, 10);

            SpriteManager.Add(Sprite.Name.UFO, Image.Name.UFO, 100.0f, 100.0f, 49.0f, 33.0f, new Azul.Color(0.9019f, 0.0784f, 0.0784f, 1.0f));
            SpriteManager.Add(Sprite.Name.BombSplat, Image.Name.BombSplat, 100.0f, 100.0f, 45.0f, 33.0f);

            // Create a Sprite Animation
            // LTN - TimerEventManager (added to TimerEvent that linked to TimerEventManager)
            pSquidAnimation = new AnimationCommand(Sprite.Name.Squid);

            // LTN - TimerEventManager
            pCrabAnimation = new AnimationCommand(Sprite.Name.Crab);

            // LTN - TimerEventManager
            pOctopusAnimation = new AnimationCommand(Sprite.Name.Octopus);

            // LTN - TimerEventManager
            pMoveCommand = new MoveCommand();

            //LTN - TimerEventManager
            pRandomCommand = new RandomBombCommand();

            //LTN - TimerEventManager
            pMarchCommand = new MarchAlienCommand();

            // Loop the Squids 
            pSquidAnimation.Link(Image.Name.Squid);
            pSquidAnimation.Link(Image.Name.SquidExtend);

            // Loop the Crabs 
            pCrabAnimation.Link(Image.Name.Crab);
            pCrabAnimation.Link(Image.Name.CrabExtend);

            // Loop the Squids 
            pOctopusAnimation.Link(Image.Name.Octopus);
            pOctopusAnimation.Link(Image.Name.OctopusExtend);

            // Loop the sounds
            pMarchCommand.Link(Sound.Source.FastInvader1);
            pMarchCommand.Link(Sound.Source.FastInvader2);
            pMarchCommand.Link(Sound.Source.FastInvader3);
            pMarchCommand.Link(Sound.Source.FastInvader4);

            pLaunchUFO = new RandomUFOLaunchCommand();

            pUFOBomb = new RandomUFOBombCommand();

            TimerEventManager poTimerEventManager = new TimerEventManager(3, 1);
            TimerEventManager.SetActiveTimer(poTimerEventManager);
            HelperTimerManager poHelperTimerEventManager = new HelperTimerManager(3, 1);
            HelperTimerManager.SetActiveTimer(poHelperTimerEventManager);

            // Player
            PlayerManager.Add(Player.Name.Player1);
            PlayerManager.Add(Player.Name.Player2);

            pSceneContext = new GameSceneContext();
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {
            SoundManager.GetSoundEngine().Update();

            GlobalClock.Update(this.GetTime());

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true)
            {
                pSceneContext.SetState(GameSceneContext.Scene.Player1);
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) == true)
            {
                PlayerManager.SetGameMode(true);
                pSceneContext.SetState(GameSceneContext.Scene.Player1);
            }

            // Update the scene
            pSceneContext.GetState().Update(this.GetTime());
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            // draw all objects
            pSceneContext.GetState().Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }

        // Data
        public static GameSceneContext pSceneContext = null;
        readonly public static Random pRandom = new Random();
        public static AnimationCommand pSquidAnimation;
        public static AnimationCommand pCrabAnimation;
        public static AnimationCommand pOctopusAnimation;
        public static MoveCommand pMoveCommand;
        public static RandomBombCommand pRandomCommand;
        public static MarchAlienCommand pMarchCommand;
        public static RandomUFOLaunchCommand pLaunchUFO;
        public static RandomUFOBombCommand pUFOBomb;
    }
}

// End of file

