using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveSplatCommand : BaseCommand
    {
        // Constructor
        public RemoveSplatCommand(CollisionObserver pObserver)
            : base()
        {
            this.pObserver = pObserver;
        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            pObserver.RemoveSplat();
        }

        //Data
        public CollisionObserver pObserver;
    }
}

// End of file