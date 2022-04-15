using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGrid : Composite
    {

        // Constructor
        public AlienGrid(GameObject.Name name, Sprite.Name spriteName)
            : base(name, spriteName)
        {

        }

        // Methods
        public float GetDelta()
        {
            AlienGroup pAlienGroup = (AlienGroup)ForwardCompositeIterator.GetParentNode(this);
            return pAlienGroup.GetDelta();
        }

        public void SetDelta(float delta)
        {
            AlienGroup pAlienGroup = (AlienGroup)ForwardCompositeIterator.GetParentNode(this);
            pAlienGroup.SetDelta(delta);
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            // Create a new box with white color
            base.Resurrect();

            this.SetCollisionBoxColor(0.0f, 0.0f, 1.0f);
        }

        // Overriding methods
        public override void Accept(CollisionVistor other)
        {
            // Call the VisitGrid method           
            other.VisitGrid(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs AlienGrid
            GameObject pGameObj = (GameObject)ForwardCompositeIterator.GetChildNode(this);
            CollisionPair.CollidePair(m, pGameObj);
        }

        public override void Update()
        {
            BaseBoundingBoxUpdate(this);
            base.Update();
        }
    }
}

// End of file