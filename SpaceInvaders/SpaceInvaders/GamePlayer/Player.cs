using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class Player : SingleLink
    {
        // Public Enum
        public enum Name
        {
            Player1,
            Player2,

            Uninitialized
        }

        // Constrcutor
        public Player()
            : base()
        {
            ClearValues();
        }

        // Methods
        public void SetValues(Name name)
        {
            this.name = name;
            lifes = 3;
            score = 0;
        }

        public int GetNumOfLifes()
        {
            return lifes;
        }

        public int GetScore()
        {
            return score;
        }

        public void AddLife()
        {
            lifes += 1;
        }

        public void AddScore(int score)
        {
            this.score = score;
        }

        public void RemoveLife()
        {
            lifes -= 1;
        }

        public void Reset()
        {
            lifes = 3;
            score = 0;
        }

        public bool GetGameStatus()
        {
            return bInitialGame;
        }

        public void SetGameStatus(bool status)
        {
            bInitialGame = status;
        }

        // Overriding Methods
        public override void ClearValues()
        {
            name = Name.Uninitialized;
            lifes = 3;
            score = 0;
        }

        public override bool Compare(BaseNode pNodeToCompare)
        {
            Debug.Assert(pNodeToCompare != null);

            // Used to compare two nodes
            Player pPlayer = (Player)pNodeToCompare;

            if (name == pPlayer.name)
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
            Debug.WriteLine("             (Score, Lifes): {0},{1}", score, lifes);

            base.Dump();
        }


        // Data
        public Name name;
        private int lifes;
        private int score;
        private bool bInitialGame = true;
    }
}

// End of file