using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ReStartNextLevelCommand : BaseCommand
    {
        // Constructor
        public ReStartNextLevelCommand()
            : base()
        {

        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            GameObject pShieldGroup = GameObjectNodeManager.Find(GameObject.Name.ShieldGroup);
            ShieldFactory.RemoveShields(pShieldGroup);

            GameObject pNewShieldGroup = ShieldFactory.CreateSingleShield(100.0f, 200.0f);
            GameObject pShieldGroup1 = ShieldFactory.CreateSingleShield(300.0f, 200.0f);
            GameObject pShieldGroup2 = ShieldFactory.CreateSingleShield(500.0f, 200.0f);
            GameObject pShieldGroup3 = ShieldFactory.CreateSingleShield(700.0f, 200.0f);

            // Add the timer event
            AlienGroup pAlienGroup = (AlienGroup)GameObjectNodeManager.Find(GameObject.Name.AlienGroup);
            float positionY = pAlienGroup.GetGridLocation() - 50.0f;
            pAlienGroup.SetGridLocation(positionY);

            AlienFactory.CreateAlienGrid(positionY);
            HelperTimerManager.RemoveAll();

            pAlienGroup.ResetDelta(5.0f);
            float timeToUpdate = pAlienGroup.GetGridSpeed() - 0.4f;

            HelperTimerManager.bIsUpdateRequired = true;
            HelperTimerManager.Add(TimerEvent.Name.Squid, timeToUpdate, SpaceInvaders.pSquidAnimation);
            HelperTimerManager.Add(TimerEvent.Name.Crab, timeToUpdate, SpaceInvaders.pCrabAnimation);
            HelperTimerManager.Add(TimerEvent.Name.Octopus, timeToUpdate, SpaceInvaders.pOctopusAnimation);
            HelperTimerManager.Add(TimerEvent.Name.Move, timeToUpdate, SpaceInvaders.pMoveCommand);
            HelperTimerManager.Add(TimerEvent.Name.March, timeToUpdate, SpaceInvaders.pMarchCommand);
            HelperTimerManager.Add(TimerEvent.Name.Bomb, 2.0f, SpaceInvaders.pRandomCommand);
            HelperTimerManager.Add(TimerEvent.Name.LaunchUFO, SpaceInvaders.pRandom.Next(5, 10), SpaceInvaders.pLaunchUFO);
        }
    }
}

// End of file