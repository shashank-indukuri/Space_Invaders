using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ReStartGameCommand : BaseCommand
    {
        // Constructor
        public ReStartGameCommand()
            : base()
        {

        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            GameObject pShieldGroup = GameObjectNodeManager.Find(GameObject.Name.ShieldGroup);
            ShieldFactory.RemoveShields(pShieldGroup);

            GameObject pAlienGroup = GameObjectNodeManager.Find(GameObject.Name.AlienGroup);
            AlienFactory.RemoveAliens(pAlienGroup);

            GameObject pNewShieldGroup = ShieldFactory.CreateSingleShield(100.0f, 200.0f);
            GameObject pShieldGroup1 = ShieldFactory.CreateSingleShield(300.0f, 200.0f);
            GameObject pShieldGroup2 = ShieldFactory.CreateSingleShield(500.0f, 200.0f);
            GameObject pShieldGroup3 = ShieldFactory.CreateSingleShield(700.0f, 200.0f);

            AlienFactory.CreateAlienGrid();

            ShipManager.ActivateShip();

            AlienGroup pAlineGroup1 = (AlienGroup)pAlienGroup;
            pAlineGroup1.ResetDelta(5.0f);
            float timeToUpdate = pAlineGroup1.GetInitialGridSpeed();

            TimerEvent pSquidEvent = TimerEventManager.Find(TimerEvent.Name.Squid);
            TimerEvent pCrabEvent = TimerEventManager.Find(TimerEvent.Name.Crab);
            TimerEvent pOctopusEvent = TimerEventManager.Find(TimerEvent.Name.Octopus);
            TimerEvent pMoveEvent = TimerEventManager.Find(TimerEvent.Name.Move);
            TimerEvent pMarchEvent = TimerEventManager.Find(TimerEvent.Name.March);
            //TimerEvent pUFOEvent = TimerEventManager.Find(TimerEvent.Name.LaunchUFO);

            pSquidEvent.UpdateDeltaTime(timeToUpdate);
            pCrabEvent.UpdateDeltaTime(timeToUpdate);
            pOctopusEvent.UpdateDeltaTime(timeToUpdate);
            pMoveEvent.UpdateDeltaTime(timeToUpdate);
            pMarchEvent.UpdateDeltaTime(timeToUpdate);
        }
    }
}

// End of file
