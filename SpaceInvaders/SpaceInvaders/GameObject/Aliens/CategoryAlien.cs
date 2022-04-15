using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryAlien : Leaf
    {
        // Enum
        public enum Kind
        {
            OctopusExtend,
            Octopus,
            CrabExtend,
            Crab,
            SquidExtend,
            Squid,
            Column,
            Grid,
            Group
        }

        // Constructor
        public CategoryAlien(Name gameName, Sprite.Name spriteName, float x, float y)
            : base(gameName, spriteName, x, y)
        {

        }

        public override void Resurrect()
        {
            base.Resurrect();
        }

        abstract public int GetScore();
    }
}

// End of file