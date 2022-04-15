using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class RandomBombCommand : BaseCommand
    {
        // Constructor
        public RandomBombCommand()
        {
            this.pRandom = SpaceInvaders.pRandom;
        }

        // Overriding Methods
        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);

            GameObject pAlienGroup = GameObjectNodeManager.Find(GameObject.Name.AlienGroup);
            Debug.Assert(pAlienGroup != null);

            GameObject pAlienGrid = (GameObject)ForwardCompositeIterator.GetChildNode(pAlienGroup);

            if (pAlienGrid != null)
            {
                int count = pAlienGrid.GetNumOfChildren();
                // Create Bomb
                int randomColumn = pRandom.Next(0, count);
                GameObject pColumn = pAlienGrid.GetChild(randomColumn);

                ((AlienColumn)pColumn).ShootBomb(pColumn);

                TimerEventManager.Add(TimerEvent.Name.Bomb, pRandom.Next(1, 4), this);
            }
        }

        // Data
        Random pRandom;
    }
}

// End of file