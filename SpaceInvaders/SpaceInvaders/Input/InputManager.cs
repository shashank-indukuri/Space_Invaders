using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputManager
    {
        // Constructor
        private InputManager()
        {
            privPrevSpaceKey = false;
            // Initializing the InputSubjects for three inputs
            poInSubjectArrowLeft = new InputSubject();
            poInSubjectArrowRight = new InputSubject();
            poInSubjectSpace = new InputSubject();
        }

        // Static methods
        private static InputManager PrivGetInstance()
        {
            // Singleton
            if (poInstance == null)
            {
                poInstance = new InputManager();
            }
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputManager pInputMan = PrivGetInstance();

            // return the right arrow subject
            return pInputMan.poInSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputManager pInputMan = PrivGetInstance();

            // return the left arrow subject
            return pInputMan.poInSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputManager pInputMan = PrivGetInstance();

            // return the space subject
            return pInputMan.poInSubjectSpace;
        }

        public static void Update()
        {
            InputManager pInputMan = PrivGetInstance();

            // Right arrow key
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                pInputMan.poInSubjectArrowRight.Notify();
            }

            // Left arrow key
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                pInputMan.poInSubjectArrowLeft.Notify();
            }

            // Track the previous space key
            bool tempSpaceKey = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);

            if (tempSpaceKey == true && pInputMan.privPrevSpaceKey == false)
            {
                pInputMan.poInSubjectSpace.Notify();
            }

            pInputMan.privPrevSpaceKey = tempSpaceKey;

        }

        // Data
        private InputSubject poInSubjectArrowRight;
        private InputSubject poInSubjectArrowLeft;
        private InputSubject poInSubjectSpace;
        private static InputManager poInstance = null;
        private bool privPrevSpaceKey;
    }
}

// End of file