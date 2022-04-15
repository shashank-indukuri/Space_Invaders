using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimerEventManager : BaseManager
    {
        public TimerEventManager(int InitialNumReserved = 3, int DeltaGrow = 1)
            : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - TimerEventManager
            poNodeToFind = new TimerEvent();
            psActiveInstance = null;
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(psInstance == null);

            // Creating the new Timer Event Manager
            if (psInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                psInstance = new TimerEventManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }

        public static void Destroy()
        {
            TimerEventManager pTimerEventMan = psActiveInstance;

            Debug.Assert(pTimerEventMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        private static TimerEventManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        public static void Dump()
        {
            TimerEventManager pTimerEventMan = psActiveInstance;
            // Make sure the instance is not null
            Debug.Assert(pTimerEventMan != null);

            // Calling the Base manager Dump to print
            pTimerEventMan.BaseDump();

        }

        public static TimerEvent Add(TimerEvent.Name name, float deltaTime, BaseCommand pCommand)
        {
            TimerEventManager pTimerEventMan = psActiveInstance;

            TimerEvent pTimerEvent = (TimerEvent)pTimerEventMan.BaseAddToFront(deltaTime);
            // Check the Timer Event is not null
            Debug.Assert(pTimerEvent != null);

            // Set the data to Timer Event
            pTimerEvent.SetValues(name, deltaTime, pCommand);
            return pTimerEvent;
        }

        public static void PauseTimerEvents(float delta)
        {
            // Get the static instance
            TimerEventManager pTimerEventMan = psActiveInstance;
            Debug.Assert(pTimerEventMan != null);

            BaseIterator pIterator = pTimerEventMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            TimerEvent pEvent = (TimerEvent)pIterator.First();

            // Walk throught the list
            while (!pIterator.IsDone())
            {
                pEvent.timeToTrigger += delta;
                pEvent = (TimerEvent)pIterator.Next();
            }

        }

        public static void PauseAnimation(float delta)
        {
            // Get the static instance
            TimerEventManager pTimerEventMan = psActiveInstance;
            Debug.Assert(pTimerEventMan != null);

            TimerEvent pSquidEvent = TimerEventManager.Find(TimerEvent.Name.Squid);
            TimerEvent pCrabEvent = TimerEventManager.Find(TimerEvent.Name.Crab);
            TimerEvent pOctopusEvent = TimerEventManager.Find(TimerEvent.Name.Octopus);
            TimerEvent pMoveEvent = TimerEventManager.Find(TimerEvent.Name.Move);
            TimerEvent pMarchEvent = TimerEventManager.Find(TimerEvent.Name.March);
            TimerEvent pBombEvent = TimerEventManager.Find(TimerEvent.Name.Bomb);

            pSquidEvent.UpdateTimeToTrigger(delta);
            pCrabEvent.UpdateTimeToTrigger(delta);
            pOctopusEvent.UpdateTimeToTrigger(delta);
            pMoveEvent.UpdateTimeToTrigger(delta);
            pMarchEvent.UpdateTimeToTrigger(delta);
            pBombEvent.UpdateTimeToTrigger(delta);
        }

        public static void SetActiveTimer(TimerEventManager pTimerMan)
        {
            TimerEventManager pTimerEventMan = TimerEventManager.PrivGetInstance();
            Debug.Assert(pTimerEventMan != null);

            Debug.Assert(pTimerMan != null);
            TimerEventManager.psActiveInstance = pTimerMan;
        }

        public static TimerEvent Find(TimerEvent.Name name)
        {
            TimerEventManager pTimerEventMan = psActiveInstance;
            TimerEventManager.poNodeToFind.name = name;
            TimerEvent pTimerEvent = (TimerEvent)pTimerEventMan.BaseFind(TimerEventManager.poNodeToFind);

            // Return the found node
            return pTimerEvent;
        }

        public static void Remove(TimerEvent pTimerEvent)
        {
            Debug.Assert(pTimerEvent != null);

            TimerEventManager pTimerEventMan = psActiveInstance;
            Debug.Assert(pTimerEventMan != null);

            pTimerEventMan.BaseRemove(pTimerEvent);
        }

        public static void RemoveAll()
        {
            // current active instance
            TimerEventManager pTimerEventMan = TimerEventManager.psActiveInstance;


            BaseIterator pIterator = pTimerEventMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            TimerEvent pNode = (TimerEvent)pIterator.First();
            TimerEvent pNextNode = null;

            // Walk through the nodes
            while (!pIterator.IsDone())
            {
                pNextNode = (TimerEvent)pIterator.Next();

                // Remove from the current list
                pTimerEventMan.BaseRemove(pNode);

                // Next node
                pNode = pNextNode;
            }
        }

        public static void RemoveTimerEventsOnScreen()
        {
            // current active instance
            TimerEventManager pTimerEventMan = TimerEventManager.psActiveInstance;


            BaseIterator pIterator = pTimerEventMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            TimerEvent pNode = (TimerEvent)pIterator.First();
            TimerEvent pNextNode = null;

            // Walk through the nodes
            while (!pIterator.IsDone())
            {
                pNextNode = (TimerEvent)pIterator.Next();

                if (pNode.name == TimerEvent.Name.UFOMove || pNode.name == TimerEvent.Name.UFOBomb || pNode.name == TimerEvent.Name.Bomb)
                {
                    continue;
                }
                // Remove from the current list
                pTimerEventMan.BaseRemove(pNode);

                // Next node
                pNode = pNextNode;
            }
        }

        public static void UpdateTimerEvents(float timeToUpdate)
        {
            TimerEventManager pTimerEventMan = psActiveInstance;
            Debug.Assert(pTimerEventMan != null);

            BaseIterator pIterator = pTimerEventMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            pTimerEventMan.mCurrentTime = timeToUpdate;

            TimerEvent pTimerEvent = (TimerEvent)pIterator.First();
            TimerEvent pNextEvent = null;

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {

                // Get the next Timer Event
                pNextEvent = (TimerEvent)pIterator.Next();

                if (pTimerEventMan.mCurrentTime >= pTimerEvent.timeToTrigger)
                {
                    pTimerEvent.Perform();

                    pTimerEventMan.BaseRemove(pTimerEvent);
                }

                pTimerEvent = pNextEvent;

            }

        }

        public static float GetCurrentTime()
        {
            TimerEventManager pTimerEventMan = psActiveInstance;

            // Check the current manager instance is not null
            Debug.Assert(pTimerEventMan != null);

            return pTimerEventMan.mCurrentTime;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - TimerEventManager
            TimerEvent pTimerEvent = new TimerEvent();
            Debug.Assert(pTimerEvent != null);

            // Return a newly created Timer Event
            return pTimerEvent;
        }

        // Data
        private static TimerEvent poNodeToFind;
        private static TimerEventManager psInstance;
        private static TimerEventManager psActiveInstance = null;
        protected float mCurrentTime;
    }
}

// End of file