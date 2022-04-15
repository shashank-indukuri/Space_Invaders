using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public PlayerManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new SingleLinkManager(), new SingleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - PlayerManager
            poNodeToFind = new Player();
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(psInstance == null);

            // Creating the new Player Manager
            if (psInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                psInstance = new PlayerManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }

        public static void Destroy()
        {
            PlayerManager pPlayerMan = PrivGetInstance();

            Debug.Assert(pPlayerMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        private static PlayerManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        public static void Dump()
        {
            PlayerManager pPlayerMan = PrivGetInstance();
            // Make sure the instance is not null
            Debug.Assert(pPlayerMan != null);

            // Calling the Base manager Dump to print
            pPlayerMan.BaseDump();
        }

        public static void Add(Player.Name name)
        {
            Player pPlayer = (Player)psInstance.BaseAddToFront();
            // Check the Player is not null
            Debug.Assert(pPlayer != null);

            // Set the data to Player
            pPlayer.SetValues(name);
        }

        public static Player Find(Player.Name name)
        {
            psInstance.poNodeToFind.name = name;
            Player pPlayer = (Player)psInstance.BaseFind(psInstance.poNodeToFind);

            // Return the found node
            return pPlayer;
        }

        public static void UpdateHighScore(int score)
        {
            if (score > highScore)
            {
                highScore = score;
            }
        }

        public static int GetHighScore()
        {
            return highScore;
        }

        public static void SetGameMode(bool mode)
        {
            bTwoPlayer = mode;
        }

        public static bool GetGameMode()
        {
            return bTwoPlayer;
        }

        public static void UpdatePlayerState(bool state)
        {
            bIsPlayerStateUpdated = state;
        }

        public static bool GetPlayerState()
        {
            return bIsPlayerStateUpdated;
        }

        public static void SetActivePlayer(Player.Name name)
        {
            Player pPlayer = PlayerManager.Find(name);
            psActivePlayer = pPlayer;
        }

        public static Player GetActivePlayer()
        {
            return psActivePlayer;
        }


        // Overriding methods
        protected override BaseNode derivedConstructNode()
        {
            // LTN - PlayerManager
            Player pPlayer = new Player();
            Debug.Assert(pPlayer != null);

            // Return a newly created Player
            return pPlayer;
        }

        // Data
        private readonly Player poNodeToFind;
        private static PlayerManager psInstance = null;
        private static int highScore = 0;
        private static bool bTwoPlayer = false;
        public static bool bIsPlayerStateUpdated = false;
        private static Player psActivePlayer;
    }
}

// End of file