using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BombShootState
    {
        public abstract void ShootBomb(GameObject pAlienCol);
    }
}
