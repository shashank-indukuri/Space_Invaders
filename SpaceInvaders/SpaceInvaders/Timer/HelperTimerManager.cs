using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class HelperTimerManager : BaseManager
    {
        public HelperTimerManager(int InitialNumReserved = 3, int DeltaGrow = 1)
            : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - HelperTimerManager
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
                psInstance = new HelperTimerManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }

        public static void Destroy()
        {
            HelperTimerManager pTimerEventMan = psActiveInstance;

            Debug.Assert(pTimerEventMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        private static HelperTimerManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        public static void Dump()
        {
            HelperTimerManager pTimerEventMan = psActiveInstance;
            // Make sure the instance is not null
            Debug.Assert(pTimerEventMan != null);

            // Calling the Base manager Dump to print
            pTimerEventMan.BaseDump();

        }

        public static TimerEvent Add(TimerEvent.Name name, float deltaTime, BaseCommand pCommand)
        {
            HelperTimerManager pTimerEventMan = psActiveInstance;

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
            HelperTimerManager pTimerEventMan = psActiveInstance;
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

        public static void SetActiveTimer(HelperTimerManager pTimerMan)
        {
            HelperTimerManager pTimerEventMan = HelperTimerManager.PrivGetInstance();
            Debug.Assert(pTimerEventMan != null);

            Debug.Assert(pTimerMan != null);
            HelperTimerManager.psActiveInstance = pTimerMan;
        }

        public static TimerEvent Find(TimerEvent.Name name)
        {
            HelperTimerManager pTimerEventMan = psActiveInstance;
            HelperTimerManager.poNodeToFind.name = name;
            TimerEvent pTimerEvent = (TimerEvent)pTimerEventMan.BaseFind(HelperTimerManager.poNodeToFind);

            // Return the found node
            return pTimerEvent;
        }

        public static void Remove(TimerEvent pTimerEvent)
        {
            Debug.Assert(pTimerEvent != null);

            HelperTimerManager pTimerEventMan = psActiveInstance;
            Debug.Assert(pTimerEventMan != null);

            pTimerEventMan.BaseRemove(pTimerEvent);
        }

        public static void RemoveAll()
        {
            // current active instance
            HelperTimerManager pTimerEventMan = HelperTimerManager.psActiveInstance;


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

        public static void UpdateTimerEvents(float timeToUpdate)
        {
            HelperTimerManager pTimerEventMan = psActiveInstance;
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

        public static void UpdateEvents()
        {
            HelperTimerManager pTimerEventMan = psActiveInstance;
            Debug.Assert(pTimerEventMan != null);

            BaseIterator pIterator = pTimerEventMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            TimerEvent pTimerEvent = (TimerEvent)pIterator.First();
            TimerEvent pNextEvent = null;
            TimerEventManager.RemoveTimerEventsOnScreen();

            // Loop thorugh the nodes in the active list
            while (!pIterator.IsDone())
            {

                // Get the next Timer Event
                pNextEvent = (TimerEvent)pIterator.Next();

                TimerEventManager.Add(pTimerEvent.name, pTimerEvent.deltaTime, pTimerEvent.pCommand);

                pTimerEvent = pNextEvent;

            }
            bIsUpdateRequired = false;
        }

        public static float GetCurrentTime()
        {
            HelperTimerManager pTimerEventMan = psActiveInstance;

            // Check the current manager instance is not null
            Debug.Assert(pTimerEventMan != null);

            return pTimerEventMan.mCurrentTime;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - HelperTimerManager
            TimerEvent pTimerEvent = new TimerEvent();
            Debug.Assert(pTimerEvent != null);

            // Return a newly created Timer Event
            return pTimerEvent;
        }

        // Data
        private static TimerEvent poNodeToFind;
        private static HelperTimerManager psInstance;
        private static HelperTimerManager psActiveInstance = null;
        protected float mCurrentTime;
        public static bool bIsUpdateRequired = false;
    }
}

// End of file
