using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BannerTextFactory
    {
        // Constructor
        private BannerTextFactory()
        {
            
        }

        public static void LoadTexts(string pMessage, float deltaTimeToTrigger, float delayTime, float x, float y, float red, float green, float blue)
        {
            BannerTextFactory pInstance = BannerTextFactory.PrivInstance();
            
            // For storing the old command
            BannerTextCommand pOldCmd = null;

            for (int i = 0; i < pMessage.Length; i++)
            {
                string pCharacter = pMessage.Substring(0, i + 1);

                BannerTextCommand pCmd = new BannerTextCommand(pOldCmd, pCharacter, x, y, red, green, blue);

                float time = deltaTimeToTrigger + i * delayTime;
                TimerEventManager.Add(TimerEvent.Name.BannerText, time, pCmd);

                pOldCmd = pCmd;
            }
        }


        private static BannerTextFactory PrivInstance()
        {
            // Make sure the Manager instance is created first
            if (psInstance == null)
            {
                // Create the instance
                BannerTextFactory.psInstance = new BannerTextFactory();
            }

            Debug.Assert(psInstance != null);

            return psInstance;
        }

        // Data
        private static BannerTextFactory psInstance = null;
    }
}

// End of file
