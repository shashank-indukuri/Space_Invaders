using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BombFallStrategy
    {
        abstract public void BombFall(Bomb pBomb);
        abstract public void Reset(float posY);
    }
}

// End of file
