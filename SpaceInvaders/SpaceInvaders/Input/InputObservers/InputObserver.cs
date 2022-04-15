using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class InputObserver : SingleLink
    {
        public abstract void Notify();

        public override void ClearValues()
        {
            Debug.Assert(false);
        }

        public InputSubject pSubject;
    }
}

// End of file