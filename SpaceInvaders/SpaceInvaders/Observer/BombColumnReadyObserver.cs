using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombColumnReadyObserver : CollisionObserver
    {
        // Overriding Methods
        public override void Notify()
        {
            GameObject pBomb;
            if (pSubject.pGameObjA.name == GameObject.Name.Bomb || pSubject.pGameObjA.name == GameObject.Name.UFOBomb)
            {
                pBomb = (Bomb)pSubject.pGameObjA;
            }
            else
            {
                pBomb = (Bomb)pSubject.pGameObjB;
            }

            GameObject.Name pColName = ((Bomb)pBomb).GetColName();

            if (pColName != GameObject.Name.UFO)
            {
                GameObject pAlienGroup = GameObjectNodeManager.Find((GameObject.Name.AlienGroup));
                ForwardCompositeIterator pIterator = new ForwardCompositeIterator(pAlienGroup);

                Component pNode = pIterator.First();

                // Walk through the nodes
                while (!pIterator.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;

                    if (pGameObj.name == pColName)
                    {
                        break;
                    }

                    pNode = pIterator.Next();
                }

                ((AlienColumn)pNode).SetState(AlienColumn.BombState.BombReady);
            }
        }
    }
}

// End of file
