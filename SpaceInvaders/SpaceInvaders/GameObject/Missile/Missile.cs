using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : CategoryMissile
    {
        // Constructor
        public Missile(Sprite.Name spriteName, float posX, float posY)
            : base(Name.Missile, spriteName, posX, posY)
        {
            x = posX;
            y = posY;
            delta = 7.0f;
            this.poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }

        ~Missile()
        {

        }

        // Methods

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 7.0f;
            base.Resurrect();
            this.poCollisionObj.pCollisionSBoxProxy.SetColor(1, 1, 0);
        }
        public void SetPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public void SetDelta(float delta)
        {
            this.delta = delta;
        }


        // Overriding methods
        public override void Update()
        {
            base.Update();
            this.y += delta;
        }

        public override void Remove()
        {
            // Update the size to zero as it has parent group
            poCollisionObj.poCollisionRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent missile group
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // remove it
            base.Remove();
        }

        public override void Accept(CollisionVistor other)
        {
            // Call the VisitMissile method            
            other.VisitMissile(this);
        }

        // Data
        public float delta;
    }
}

// End of file