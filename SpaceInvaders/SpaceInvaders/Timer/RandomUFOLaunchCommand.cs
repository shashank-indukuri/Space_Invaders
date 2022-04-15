using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RandomUFOLaunchCommand : BaseCommand
    {
        // Overriding Methods
        public override void Execute(float deltaTime)
        {
            // Find the ufo group
            UFOGroup pUFOGroup = (UFOGroup)GameObjectNodeManager.Find(GameObject.Name.UFOGroup);
            SpriteBatch pSBAliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(pSBAliens != null);
            SpriteBatch pSBBoxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes); 
            Debug.Assert(pSBBoxes != null);

            // Check if the ufo group has a child
            if (pUFOGroup.GetNumOfChildren() == 0)
            {
                // Determine randomly the direction
                int startRandom = random.Next(0, 2);

                float x = 100 + (startRandom * 700.0f);
                float y = 900;
                float delta = (startRandom == 0) ? 7.0f : -7.0f;

                UFO pUFO = new UFO(GameObject.Name.UFO, Sprite.Name.UFO, x, y);

                pUFO.LinkCollisionSprite(pSBBoxes);
                pUFO.LinkSprite(pSBAliens);

                pUFOGroup.Add(pUFO);

                UFOMoveCommand pMoveUFO = new UFOMoveCommand(delta);
                TimerEventManager.Add(TimerEvent.Name.UFOMove, 0.1f, pMoveUFO);
                TimerEventManager.Add(TimerEvent.Name.UFOBomb, (float)random.NextDouble(), new RandomUFOBombCommand());
            }
        }

        //Data
        Random random = SpaceInvaders.pRandom;
    }
}

// End of file
