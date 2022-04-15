using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameSceneContext
    {
        // Enum
        public enum Scene
        {
            Home,
            Player1,
            Player2,
            GameOver
        }

        // Constructor
        public GameSceneContext()
        {
            // Instantiate the Scenes
            this.poHomeScene = new HomeScene();
            this.poPlayOneScene = new PlayerOneScene();
            this.poPlayTwoScene = new PlayerTwoScene();
            this.poGameOverScene = new GameOverScene();

            // Initialiaze current scene state with Home scene
            this.pSceneState = this.poHomeScene;
            this.pSceneState.Entering();
        }

        // Methods
        public GameSceneState GetState()
        {
            return this.pSceneState;
        }

        public void SetState(Scene eScene)
        {
            switch (eScene)
            {
                case Scene.Home:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poHomeScene;
                    this.pSceneState.Entering();
                    break;

                case Scene.Player1:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poPlayOneScene;
                    this.pSceneState.Entering();
                    break;

                case Scene.Player2:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poPlayTwoScene;
                    this.pSceneState.Entering();
                    break;

                case Scene.GameOver:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poGameOverScene;
                    this.pSceneState.Entering();
                    break;

            }
        }

         //Data
        GameSceneState pSceneState;
        HomeScene poHomeScene;
        PlayerOneScene poPlayOneScene;
        PlayerTwoScene poPlayTwoScene;
        GameOverScene poGameOverScene;
    }
}

// End of file
