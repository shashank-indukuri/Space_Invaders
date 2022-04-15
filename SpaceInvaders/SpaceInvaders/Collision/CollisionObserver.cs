using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionObserver : SingleLink
    {
        public abstract void Notify();

        public override void ClearValues()
        {
            Debug.Assert(false);
        }

        // State Pattern
        public virtual void Execute()
        {
            // Nothing
        }

        public virtual void RemoveSplat()
        {
            // Nothing
        }

        public CollisionSubject pSubject;
    }
}

// End of file
