using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionVistor : DoubleLink
    {
        abstract public void Accept(CollisionVistor other);

        // If not implemented in subclass, run the default statements
        public virtual void VisitGroup(AlienGroup aRoot)
        {
            Debug.WriteLine("Visit by AlienRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitGrid(AlienGrid aGrid)
        {
            Debug.WriteLine("Visit by AlienGrid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitColumn(AlienColumn aColumn)
        {
            Debug.WriteLine("Visit by AlienColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitSquid(Squid aSquid)
        {
            Debug.WriteLine("Visit by Squid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitCrab(Crab aCrab)
        {
            Debug.WriteLine("Visit by Crab not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitOctopus(Octopus aOctopus)
        {
            Debug.WriteLine("Visit by Octopus not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFOGroup(UFOGroup uGroup)
        {
            Debug.WriteLine("Visit by UFOGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFO(UFO ufo)
        {
            Debug.WriteLine("Visit by UFO not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldGroup(ShieldGroup sGroup)
        {
            Debug.WriteLine("Visit by ShieldGroup not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShieldGrid(ShieldGrid sGrid)
        {
            Debug.WriteLine("Visit by ShieldGrid not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShieldColumn(ShieldColumn sColumn)
        {
            Debug.WriteLine("Visit by ShieldColumn not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShieldBrick(ShieldBrick sBrick)
        {
            Debug.WriteLine("Visit by ShieldBrick not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile missile)
        {
            Debug.WriteLine("Visit by Missile not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup mGroup)
        {
            Debug.WriteLine("Visit by MissileGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitNullGameObject(NullGameObject nullObject)
        {
            Debug.WriteLine("Visit by NullGameObject not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallGroup(WallGroup wallGroup)
        {
            Debug.WriteLine("Visit by WallGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallRight(WallRight wRight)
        {
            Debug.WriteLine("Visit by WallRight not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallLeft(WallLeft wLeft)
        {
            Debug.WriteLine("Visit by WallLeft not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallTop(WallTop wTop)
        {
            Debug.WriteLine("Visit by WallTop not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallBottom(WallBottom wBottom)
        {
            Debug.WriteLine("Visit by WallBottom not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship s)
        {
            Debug.WriteLine("Visit by Ship not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShipGroup(ShipGroup s)
        {
            Debug.WriteLine("Visit by ShipGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBumperGroup(WallBumperGroup wbGroup)
        {
            Debug.WriteLine("Visit by Bumper Group not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBumperRight(WallBumperRight wbRight)
        {
            Debug.WriteLine("Visit by Bumper Right not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBumperLeft(WallBumperLeft wbLeft)
        {
            Debug.WriteLine("Visit by Bumper Left not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBomb(Bomb bomb)
        {
            Debug.WriteLine("Visit by Bomb not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBombGroup(BombGroup bombGroup)
        {
            Debug.WriteLine("Visit by Bomb Group not implemented");
            Debug.Assert(false);
        }
    }
}

// End of file
