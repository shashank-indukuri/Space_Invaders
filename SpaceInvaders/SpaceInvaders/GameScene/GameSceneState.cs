using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameSceneState
    {

        public GameSceneState()
        {
            currentTimeAtPause = TimerEventManager.GetCurrentTime();
        }

        abstract public void Initialize();
        abstract public void Update(float currentTime);
        abstract public void Draw();
        abstract public void Entering();
        abstract public void Leaving();

        virtual public void HanldeState()
        {

        }

        // Data
        public float currentTimeAtPause; 
    }
}

// End of file
