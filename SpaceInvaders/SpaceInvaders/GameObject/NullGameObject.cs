using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class NullGameObject : Leaf
    {

        // Constructor
        public NullGameObject()
            : base(Name.NullObject, Sprite.Name.NullObject, 0, 0)
        {

        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitNullGameObject method           
            other.VisitNullGameObject(this);
        }

        public override void Update()
        {
            // Just nothing
        }

        // Static field for Null Sprite Proxy
        // LTN - GameObjectNodeManager
        private static NullSpriteProxy pNullSpriteProxy = new NullSpriteProxy();
    }
}

// End of file