using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class BaseType
    {
        abstract public void AddNodeToFront(BaseNode pNode);
        abstract public void AddNodeToEnd(BaseNode pNode);

        virtual public void AddNodeOnPriority(BaseNode pNode, float priority)
        {
            // Nothing
        }
        abstract public void RemoveNode(BaseNode pNode);
        abstract public BaseNode RemoveNodeFromFront();
        abstract public BaseIterator FetchIterator();
    }
}
