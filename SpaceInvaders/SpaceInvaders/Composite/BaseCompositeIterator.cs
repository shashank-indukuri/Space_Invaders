using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class BaseCompositeIterator
    {
        public abstract Component First();

        public abstract Component Current();

        public abstract Component Next();

        public abstract bool IsDone();
    }
}

// End of file