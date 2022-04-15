using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameSimulation
    {
        // Enum
        public enum State
        {
            Realtime,
            FixedStep,
            SingleStep,
            Pause
        };

        // Singleton Constructor
        private GameSimulation()
        {
            this.state = State.SingleStep;

            this.timeStep = 0.0f;
            this.totalWatch = 0.0f;
            this.stopWatch_tic = 0.0f;
            this.stopWatch_toc = 0.0f;
        }

        // Singleton
        private static GameSimulation PrivGetInstance()
        {
            // Check for the pInstance
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // Static Methods
        public static void Create()
        {
            // Create the singleton instance
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GameSimulation();
            }
        }

        public static void Update(float systemTime)
        {
            GameSimulation pSim = GameSimulation.PrivGetInstance();
            Debug.Assert(pSim != null);

            // Update the input
            pSim.privProcessInput();

            // Time update.
            //      Get the time that has passed.
            //      Feels backwards, but its not, need to see how much time has passed
            pSim.stopWatch_toc = systemTime - pSim.stopWatch_tic;
            pSim.stopWatch_tic = systemTime;

            if (pSim.privGetState() == State.FixedStep)
            {
                pSim.timeStep = SIM_SINGLE_TIME_STEP;
            }
            else if (pSim.privGetState() == State.Realtime)
            {
                pSim.timeStep = pSim.stopWatch_toc;
            }
            else if (pSim.privGetState() == State.SingleStep)
            {
                pSim.timeStep = SIM_SINGLE_TIME_STEP;
                pSim.privSetState(State.Pause);
            }
            // Pause state
            else 
            {
                pSim.timeStep = 0.0f;
            }

            pSim.totalWatch += pSim.timeStep;

        }


        // --- GameSimulation controls ------------
        //   S - single step
        //   D - repeat step while holding
        //   G - start GameSimulation fixed step
        //   H - start GameSimulation realtime stepping
        private void privProcessInput()
        {
            // Controls

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_G) == true)
            {
                this.privSetState(State.FixedStep);
            }
            else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_H) == true)
            {
                this.privSetState(State.Realtime);
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_S) && (oldKey == false))
            {
                // One step
                this.privSetState(State.SingleStep);
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_D) == true)
            {
                // Mutiple single steps
                this.privSetState(State.SingleStep);
            }

            oldKey = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_S);

        }

        public static void SetState(State simState)
        {
            GameSimulation pSim = GameSimulation.PrivGetInstance();
            Debug.Assert(pSim != null);

            pSim.privSetState(simState);
        }
        public static State GetState()
        {
            GameSimulation pSim = GameSimulation.PrivGetInstance();
            Debug.Assert(pSim != null);

            return pSim.privGetState();
        }
        public static float GetTimeStep()
        {
            GameSimulation pSim = GameSimulation.PrivGetInstance();
            Debug.Assert(pSim != null);
            return pSim.timeStep;
        }
        public static float GetTotalTime()
        {
            GameSimulation pSim = GameSimulation.PrivGetInstance();
            Debug.Assert(pSim != null);
            return pSim.totalWatch;
        }

        // Private Methods

        private void privSetState(State simState)
        {
            this.state = simState;
        }
        private State privGetState()
        {
            return this.state;
        }

        // Data

        private static GameSimulation pInstance;

        private State state;

        private float stopWatch_tic;
        private float stopWatch_toc;
        private float totalWatch;
        private float timeStep;

        private const float SIM_SINGLE_TIME_STEP = 0.016666f;

        private static bool oldKey = false;
    }
}

// End of file