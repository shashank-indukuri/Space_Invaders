using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimerEvent : DoubleLink
    {
        // Enum
        public enum Name
        {
            Animate,
            March,
            Move,
            Octopus,
            Crab,
            Squid,
            Missile,
            Bomb,
            Splat,
            LaunchUFO,
            UFOMove,
            UFOBomb,
            ReSpawnShip,
            ReSpawnAlienGrid,
            ReStartNextLevel,
            BannerText,
            SwiftScene,
            ReStartGame,
            BannerAliens,

            Uninitialized
        }

        // Constructor
        public TimerEvent()
            : base()
        {
            name = Name.Uninitialized;
            timeToTrigger = 0.0f;
            deltaTime = 0.0f;
            pCommand = null;
        }

        // Methods
        public void SetValues(Name name, float deltaTime, BaseCommand pCommand)
        {
            this.name = name;
            this.deltaTime = deltaTime;
            timeToTrigger = TimerEventManager.GetCurrentTime() + deltaTime;
            this.pCommand = pCommand;
        }

        public void Perform()
        {
            Debug.Assert(pCommand != null);

            // Navigate to Command to execute onbehalf
            pCommand.Execute(deltaTime);
        }

        public void UpdateDeltaTime(float delta)
        {
            this.deltaTime = delta;
        }

        public void UpdateTimeToTrigger(float delta)
        {
            this.timeToTrigger += delta;
        }

        public float GetDeltaTime()
        {
            return deltaTime;
        }

        // Overridng methods
        public override void ClearValues()
        {
            ClearDoubleLink();
            name = Name.Uninitialized;
            timeToTrigger = 0.0f;
            deltaTime = 0.0f;
        }

        public override bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            TimerEvent pTimerEvent = (TimerEvent)pNodeToCompare;

            // Null check
            Debug.Assert(pTimerEvent != null);

            if (this.name == pTimerEvent.name)
            {
                return true;
            }
            return false;
        }

        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());

            // Data:
            Debug.WriteLine("   Event Name: {0}", name);
            Debug.WriteLine(" Trigger Time: {0}", timeToTrigger);
            Debug.WriteLine("   Delta Time: {0}", deltaTime);

            base.Dump();
        }


        // Data
        public Name name;
        public float timeToTrigger;
        public float deltaTime;
        public BaseCommand pCommand;
    }
}

// End of file
