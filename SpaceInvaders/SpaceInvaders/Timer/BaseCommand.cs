using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BaseCommand
    {
        // Making it contract to its sub classes
        abstract public void Execute(float deltaTime);
    }
}

// End of file
