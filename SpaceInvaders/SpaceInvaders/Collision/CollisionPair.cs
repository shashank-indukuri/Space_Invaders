using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionPair : DoubleLink
    {
        // Enum
        public enum Name
        {
            Alien_Wall,
            Wall_UFO,
            Missile_Wall,
            Missile_Alien,
            Missile_UFO,
            Misslie_Shield,
            Missile_Bomb,
            Bomb_Wall,
            Bomb_Shield,
            Bomb_Ship,
            WallBumper_Ship,
            NullObject,
            Uninitialized
        }

        // Constructor
        public CollisionPair()
            : base()
        {
            PrivClearValues();

            poSubject = new CollisionSubject();
            Debug.Assert(poSubject != null);
        }

        ~CollisionPair()
        {

        }

        // Methods
        public void SetValues(Name name, GameObject pCompositeA, GameObject pCompositeB)
        {
            // GameObjects shouldn't be null
            Debug.Assert(pCompositeA != null);
            Debug.Assert(pCompositeB != null);

            // Initializing the values
            this.name = name;
            this.pCompositeA = pCompositeA;
            this.pCompositeB = pCompositeB;
        }

        public void Attach(CollisionObserver pObserver)
        {
            poSubject.Attach(pObserver);
        }

        public void NotifyListeners()
        {
            // Invoke the Notify in CollisionSubject
            poSubject.Notify();
        }

        public void SetCollision(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);

            // Set the values of current subject with game objects
            poSubject.pGameObjA = pObjA;
            poSubject.pGameObjB = pObjB;
        }

        public void Process()
        {
            // Test the current pair
            CollidePair(pCompositeA, pCompositeB);
        }

        public static void CollidePair(GameObject pTreeA, GameObject pTreeB)
        {
            // GameObject A vs GameObject B
            GameObject pGameNodeA = pTreeA;
            GameObject pGameNodeB = pTreeB;

            while (pGameNodeA != null)
            {
                // Compare pGameNodeA with pTreeB (full tree)
                pGameNodeB = pTreeB;

                while (pGameNodeB != null)
                {
                    // Testing Pair
                    //Debug.WriteLine("CollisionPair: test:  {0}, {1}", pGameNodeA.name, pGameNodeB.name);

                    // Getting the rectangles for two objects
                    CollisionRect pRectA = pGameNodeA.poCollisionObj.poCollisionRect;
                    CollisionRect pRectB = pGameNodeB.poCollisionObj.poCollisionRect;

                    // Intersect and test for collision
                    if (CollisionRect.Intersect(pRectA, pRectB))
                    {
                        // If Collide, do Visit (Visitor Pattern)
                        pGameNodeA.Accept(pGameNodeB);
                        break;
                    }

                    pGameNodeB = (GameObject)ForwardCompositeIterator.GetSiblingNode(pGameNodeB);
                }

                pGameNodeA = (GameObject)ForwardCompositeIterator.GetSiblingNode(pGameNodeA);
            }
        }
        private void PrivClearValues()
        {
            name = Name.Uninitialized;
            pCompositeA = null;
            pCompositeB = null;
        }

        public override void ClearValues()
        {
            PrivClearValues();
        }

        public override object GetName()
        {
            return name;
        }

        override public bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            CollisionPair pColPair = (CollisionPair)pNodeToCompare;

            if (name == pColPair.name)
            {
                return true;
            }
            return false;
        }
        public override void Dump()
        {
            // Hash code is used here to uniquely identify
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", name, GetHashCode());

            base.Dump();
        }

        // Data
        public Name name;
        public GameObject pCompositeA;
        public GameObject pCompositeB;
        public CollisionSubject poSubject;
    }
}

// End of file