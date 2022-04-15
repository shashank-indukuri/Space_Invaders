using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class BaseNode
    {
        abstract public void ClearValues();
        abstract public void Dump();

        // Optional method that can be overided in the child classes
        virtual public object GetName()
        {
            return null;
        }

        virtual public bool Compare(BaseNode pNodeToCompare)
        {
            return false;
        }
    }
}
