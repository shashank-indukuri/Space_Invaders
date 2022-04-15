using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Leaf : GameObject
    {

        // Constructor
        public Leaf(Name gameObjName,Sprite.Name name, float x, float y)
            : base(Type.Leaf, gameObjName, name, x, y)
        {

        }

        // Overriding methods
        public override void Print()
        {
            Dump();
        }
        public override void Add(Component pComp)
        {
            Debug.Assert(false);
        }

        public override void Remove(Component pComp)
        {
            Debug.Assert(false);
        }

        public override void DumpComponent()
        {
            Debug.WriteLine(" Game Object Name: {0} ({1}) parent:{2}", this.GetName(), this.GetHashCode(), ForwardCompositeIterator.GetParentNode(this).GetHashCode());
        }

        public override void ClearValues()
        {
            /// Not allowed to clear the values
            Debug.Assert(false);
        }

        public override void Resurrect()
        {
            base.Resurrect();
        }
    }
}

// End of file