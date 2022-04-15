using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GlobalClock
    {
        // Constructor
        private GlobalClock()
        {
            this.mCurrentTime = 0.0f;
        }

        // Static Methods
        public static void Update(float time)
        {
            GlobalClock pTimer = GlobalClock.PrivGetInstance();
            pTimer.mCurrentTime = time;
        }

        public static float GetCurrentTime()
        {
            GlobalClock pTimer = GlobalClock.PrivGetInstance();
            return pTimer.mCurrentTime;
        }

        // Private Method
        private static GlobalClock PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            if (psInstance == null)
            {
                // Create the instance
                psInstance = new GlobalClock();
            }

            return psInstance;
        }

        // Data
        private static GlobalClock psInstance = null;
        protected float mCurrentTime;
    }
}

// End of file