using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SwiftSceneCommand : BaseCommand
    {
        // Constructor
        public SwiftSceneCommand()
            : base()
        {

        }
        public SwiftSceneCommand(GameSceneContext.Scene name)
            : base()
        {
            sceneName = name;
        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            SpaceInvaders.pSceneContext.SetState(sceneName);
        }

        // Data
        public GameSceneContext.Scene sceneName;
    }
}

// End of file
