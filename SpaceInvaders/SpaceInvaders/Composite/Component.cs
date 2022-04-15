using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Component : CollisionVistor
    {
        // Enum
        public enum Type
        {
            Leaf,
            Composite,

            Uninitialized
        }

        // Constructor
        public Component(Type type)
        {
            pParent = null;
            pReverse = null;
            this.type = type;
        }

        abstract public void Print();

        public abstract void Add(Component pComp);
        public abstract void Remove(Component pComp);
        public abstract void DumpComponent();

        public virtual int GetNumOfChildren()
        {
            return 0;
        }

        public virtual GameObject GetChild(int location)
        {
            return null;
        }

        public virtual void Resurrect()
        {
            this.pParent = null;
            this.pReverse = null;
        }

        // Data
        public Component pParent;
        public Component pReverse;
        public Type type;
    }
}

// End of file