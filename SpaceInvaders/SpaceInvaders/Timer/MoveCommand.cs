using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveCommand : BaseCommand
    {
        // Constructor
        public MoveCommand()
            : base()
        {
            
        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            // Find the alien grid
            AlienGroup pAlienGroup = (AlienGroup)GameObjectNodeManager.Find(GameObject.Name.AlienGroup);

            // Move the aliens in a sync with a discrete steps
            pAlienGroup.MoveGrid();

            // Add the event back to the timer
            TimerEventManager.Add(TimerEvent.Name.Move, deltaTime, this);
        }
    }
}

// End of file
