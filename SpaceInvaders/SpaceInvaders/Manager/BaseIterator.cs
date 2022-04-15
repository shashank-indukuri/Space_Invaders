using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BaseIterator
    {
        abstract public BaseNode Next();
        abstract public bool IsDone();
        abstract public BaseNode First();

        abstract public BaseNode Current();

    }
}
