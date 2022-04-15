using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerOneScene : GameSceneState
    {

        // Constructor
        public PlayerOneScene()
        {
            this.Initialize();
        }

        // Private Methods
        private void LoadOnEntry()
        {
            Player pPlayer1 = PlayerManager.Find(Player.Name.Player1);
            Player pPlayer2 = PlayerManager.Find(Player.Name.Player2);

            // Reset the state of the player

            if (!pPlayer1.GetGameStatus())
            {
                pPlayer1.Reset();
                pPlayer2.Reset();
                TimerEventManager.Add(TimerEvent.Name.ReStartGame, 0.0f, new ReStartGameCommand());
                pPlayer1.SetGameStatus(true);
            }

            // Update the scores
            FontManager.UpdateScore();
        }

        // Overriding Methods
        public override void Initialize()
        {

            // GameObjectNodeManager

            this.poGameObjManager = new GameObjectNodeManager();
            GameObjectNodeManager.SetActiveGOMan(poGameObjManager);
            // Adding Sprite Batches to the Manager
            this.poSBManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActiveBatch(this.poSBManager);
            SpriteBatch pSBAliens = SpriteBatchManager.Add(SpriteBatch.Name.Aliens);
            SpriteBatch pSBBoxes = SpriteBatchManager.Add(SpriteBatch.Name.Boxes);
            SpriteBatch pSBShips = SpriteBatchManager.Add(SpriteBatch.Name.Ships);
            SpriteBatch pSBShields = SpriteBatchManager.Add(SpriteBatch.Name.Shields);
            SpriteBatch pSBTexts = SpriteBatchManager.Add(SpriteBatch.Name.Texts);


            // Bombs
            BombGroup pBombGroup = new BombGroup(GameObject.Name.BombGroup, Sprite.Name.NullObject, 0.0f, 0.0f);
            pBombGroup.LinkCollisionSprite(pSBBoxes);

            GameObjectNodeManager.Link(pBombGroup);


            // Walls
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, Sprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.LinkSprite(pSBAliens);
            pWallGroup.LinkCollisionSprite(pSBBoxes);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, Sprite.Name.NullObject, 448, 1000, 850, 30);
            pWallTop.LinkCollisionSprite(pSBBoxes);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, Sprite.Name.NullObject, 448, 50, 850, 30);
            pWallBottom.LinkCollisionSprite(pSBBoxes);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, Sprite.Name.NullObject, 30, 500, 50, 800);
            pWallLeft.LinkCollisionSprite(pSBBoxes);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, Sprite.Name.NullObject, 870, 500, 50, 800);
            pWallRight.LinkCollisionSprite(pSBBoxes);

            // Add to the composite the children
            pWallGroup.Add(pWallTop);
            pWallGroup.Add(pWallBottom);
            pWallGroup.Add(pWallLeft);
            pWallGroup.Add(pWallRight);

            GameObjectNodeManager.Link(pWallGroup);


            // Bumpers

            WallBumperGroup pBumperGroup = new WallBumperGroup(GameObject.Name.WallBumperGroup, Sprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.LinkSprite(pSBBoxes);

            WallBumperRight pBumperRight = new WallBumperRight(GameObject.Name.WallBumperRight, Sprite.Name.NullObject, 800, 120, 50, 100);
            pBumperRight.LinkCollisionSprite(pSBBoxes);

            WallBumperLeft pBumperLeft = new WallBumperLeft(GameObject.Name.WallBumperLeft, Sprite.Name.NullObject, 100, 120, 50, 100);
            pBumperLeft.LinkCollisionSprite(pSBBoxes);

            // Add to the group
            pBumperGroup.Add(pBumperRight);
            pBumperGroup.Add(pBumperLeft);

            GameObjectNodeManager.Link(pBumperGroup);

            // Aliens
            GameObject pAlienGroup = AlienFactory.CreateAlienGrid();

            // Missile
            MissileGroup pMissileGroup = new MissileGroup();
            pMissileGroup.LinkSprite(pSBAliens);
            pMissileGroup.LinkCollisionSprite(pSBBoxes);

            GameObjectNodeManager.Link(pMissileGroup);

            // UFO

            UFOGroup pUFOGroup = new UFOGroup(GameObject.Name.UFOGroup, Sprite.Name.NullObject, 0.0f, 0.0f);
            pUFOGroup.LinkSprite(pSBAliens);
            pUFOGroup.LinkCollisionSprite(pSBBoxes);

            GameObjectNodeManager.Link(pUFOGroup);

            // Ship
            this.poShipManager = new ShipManager();
            ShipManager.SetActiveShip(poShipManager);

            ShipGroup pShipGroup = new ShipGroup(GameObject.Name.ShipGroup, Sprite.Name.NullObject, 0.0f, 0.0f);

            GameObjectNodeManager.Link(pShipGroup);
            ShipManager.ActivateShip();
            Sprite pShip1 = SpriteManager.AddSprite(Sprite.Name.Ship1, Image.Name.Ship, 125, 50, 80, 28);
            Sprite pShip2 = SpriteManager.AddSprite(Sprite.Name.Ship2, Image.Name.Ship, 225, 50, 80, 28);

            // Shields
            GameObject pShieldGroup = ShieldFactory.CreateSingleShield(100.0f, 200.0f);
            GameObject pShieldGroup1 = ShieldFactory.CreateSingleShield(300.0f, 200.0f);
            GameObject pShieldGroup2 = ShieldFactory.CreateSingleShield(500.0f, 200.0f);
            GameObject pShieldGroup3 = ShieldFactory.CreateSingleShield(700.0f, 200.0f);

            // Inputs

            InputSubject pInSubject;
            pInSubject = InputManager.GetArrowRightSubject();
            pInSubject.Attach(new MoveRightObserver());

            pInSubject = InputManager.GetArrowLeftSubject();
            pInSubject.Attach(new MoveLeftObserver());

            pInSubject = InputManager.GetSpaceSubject();
            pInSubject.Attach(new ShootObserver());

            GameSimulation.SetState(GameSimulation.State.Realtime);

            // Fonts
            this.poFontManager = new FontManager(3, 1);
            FontManager.SetActiveFont(this.poFontManager);

            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);
            FontManager.Add(Font.Name.Score1Text, SpriteBatch.Name.Texts, "SCORE <1>", Glyph.Name.Consolas36pt, 50, 1000);
            FontManager.Add(Font.Name.Score1, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 80, 950);
            FontManager.Add(Font.Name.HiScoreText, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 370, 1000);
            FontManager.Add(Font.Name.HiScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 400, 950);
            FontManager.Add(Font.Name.Score2Text, SpriteBatch.Name.Texts, "SCORE <2>", Glyph.Name.Consolas36pt, 690, 1000);
            FontManager.Add(Font.Name.Score2, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 720, 950);
            FontManager.Add(Font.Name.Lifes, SpriteBatch.Name.Texts, "3", Glyph.Name.Consolas36pt, 50, 50);

            // TimerEvent Manager
            this.poTimerEventManager = new TimerEventManager(3, 1);
            TimerEventManager.SetActiveTimer(this.poTimerEventManager);

            TimerEventManager.Add(TimerEvent.Name.Squid, 1.0f, SpaceInvaders.pSquidAnimation);
            TimerEventManager.Add(TimerEvent.Name.Crab, 1.0f, SpaceInvaders.pCrabAnimation);
            TimerEventManager.Add(TimerEvent.Name.Octopus, 1.0f, SpaceInvaders.pOctopusAnimation);
            TimerEventManager.Add(TimerEvent.Name.Move, 1.0f, SpaceInvaders.pMoveCommand);
            TimerEventManager.Add(TimerEvent.Name.March, 1.0f, SpaceInvaders.pMarchCommand);
            TimerEventManager.Add(TimerEvent.Name.Bomb, 2.0f, SpaceInvaders.pRandomCommand);
            TimerEventManager.Add(TimerEvent.Name.LaunchUFO, SpaceInvaders.pRandom.Next(25, 70), SpaceInvaders.pLaunchUFO);

            // Collision Pairs

            this.poColPairManager = new CollisionPairManager(3, 1);
            CollisionPairManager.SetActiveCollisionManager(this.poColPairManager);

            // Alien vs Wall
            CollisionPair pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Wall, pAlienGroup, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new AlienGridObserver());

            // UFO vs Wall
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Wall_UFO, pWallGroup, pUFOGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveUFOObserver());

            // Missile vs Wall
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Wall, pMissileGroup, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new AddSplatObserver());

            // Missile vs Alien
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Alien, pMissileGroup, pAlienGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveAlienObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new AlienKilledSoundObserver());
            pColPair.Attach(new AddScoreObserver());
            pColPair.Attach(new AddSplatObserver());

            // Missile vs UFO
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_UFO, pMissileGroup, pUFOGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveUFOObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new AlienKilledSoundObserver());
            pColPair.Attach(new AddScoreObserver());
            pColPair.Attach(new AddSplatObserver());

            // Missile vs Shield
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Misslie_Shield, pMissileGroup, pShieldGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());

            // Missile vs Bomb
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Bomb, pMissileGroup, pBombGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new BombColumnReadyObserver());
            pColPair.Attach(new AddSplatObserver());

            // Bomb vs Shield
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Shield, pBombGroup, pShieldGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new BombColumnReadyObserver());

            // Bomb vs WallBottom
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Wall, pBombGroup, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new BombColumnReadyObserver());

            // Bomb vs Ship
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Ship, pBombGroup, pShipGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new RemoveShipObserver());
            pColPair.Attach(new ShipExplosionObserver());
            pColPair.Attach(new AddSplatObserver());
            pColPair.Attach(new BombColumnReadyObserver());

            // Bumper vs Ship
            pColPair = CollisionPairManager.Add(CollisionPair.Name.WallBumper_Ship, pBumperGroup, pShipGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new ShipMovementObserver());

        }

        public override void Update(float currentTime)
        {
            InputManager.Update();
            // Update the current time
            GameSimulation.Update(currentTime);

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_R) == true)
            {
                GhostManager.Dump();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_E) == true)
            {
                GameObject pShieldRoot = ShieldFactory.CreateSingleShield(100.0f, 200.0f);
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Q) == true)
            {
                SpriteBatch pSBBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
                Debug.Assert(pSBBoxes != null);
                pSBBoxes.SetEnableBoxes(true);
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_W) == true)
            {
                SpriteBatch pSBBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
                Debug.Assert(pSBBoxes != null);
                pSBBoxes.SetEnableBoxes(false);
            }

            // Run on simulation
            if (GameSimulation.GetTimeStep() > 0.0f)
            {
                TimerEventManager.UpdateTimerEvents(GameSimulation.GetTotalTime());

                // Update the Game Objects
                GameObjectNodeManager.Update();

                CollisionPairManager.Process();

                DelayObjectManager.Process();
            }

            // Update the timer events
            if (HelperTimerManager.bIsUpdateRequired)
            {
                HelperTimerManager.UpdateEvents();
            }

            if (PlayerManager.GetPlayerState())
            {
                this.HanldeState();
            }
        }

        public override void HanldeState()
        {
            Font pLifes = FontManager.Find(Font.Name.Lifes);
            Debug.Assert(pLifes != null);

            Player pPlayer1 = PlayerManager.Find(Player.Name.Player1);
            pPlayer1.RemoveLife();
            int lifes = pPlayer1.GetNumOfLifes();
            pLifes.UpdateText(lifes.ToString());
            PlayerManager.UpdatePlayerState(false);

            // Check for two player mode
            if (PlayerManager.GetGameMode())
            {
                // Update the scores
                if (pPlayer1.GetNumOfLifes() == 0)
                {
                    PlayerManager.UpdateHighScore(pPlayer1.GetScore());
                    FontManager.UpdateScore();
                }
                TimerEventManager.Add(TimerEvent.Name.SwiftScene, 0.5f, new SwiftSceneCommand(GameSceneContext.Scene.Player2));
                TimerEventManager.Add(TimerEvent.Name.ReSpawnShip, 1.5f, new ReSpawnShip());
            }
            else
            {
                // Navigate to Game Over scene
                if (lifes == 0)
                {
                    PlayerManager.UpdateHighScore(pPlayer1.GetScore());
                    FontManager.UpdateScore();
                    TimerEventManager.Add(TimerEvent.Name.SwiftScene, 1.0f, new SwiftSceneCommand(GameSceneContext.Scene.GameOver));
                }
                else
                {
                    TimerEventManager.Add(TimerEvent.Name.ReSpawnShip, 1.0f, new ReSpawnShip());
                }
            }
        }

        public override void Draw()
        {
            SpriteBatchManager.Draw();

            Player pPlayer1 = PlayerManager.Find(Player.Name.Player1);
            int lifes = pPlayer1.GetNumOfLifes();
            while (lifes > 1)
            {
                lifes--;
                int i = lifes;
                Sprite pShip = SpriteManager.Find(Sprite.Name.Ship + i);
                pShip.Render();
            }
        }

        public override void Entering()
        {
            SpriteBatchManager.SetActiveBatch(poSBManager);
            FontManager.SetActiveFont(poFontManager);
            TimerEventManager.SetActiveTimer(poTimerEventManager);
            ShipManager.SetActiveShip(poShipManager);
            GameObjectNodeManager.SetActiveGOMan(poGameObjManager);
            CollisionPairManager.SetActiveCollisionManager(poColPairManager);

            PlayerManager.SetActivePlayer(Player.Name.Player1);
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
        public ShipManager poShipManager;
        public GameObjectNodeManager poGameObjManager;
        public CollisionPairManager poColPairManager;
    }
}

// End of file
