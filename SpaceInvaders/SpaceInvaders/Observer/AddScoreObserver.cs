using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AddScoreObserver : CollisionObserver
    {
        // Overriding Methods
        public override void Notify()
        {
            Font pScore;
            Player pPlayer = PlayerManager.GetActivePlayer();
            
            // Get the active player score
            int score = pPlayer.GetScore();

            if (pPlayer.name == Player.Name.Player1)
            {
                pScore = FontManager.Find(Font.Name.Score1);
                Debug.Assert(pScore != null);
            }
            else
            {
                pScore = FontManager.Find(Font.Name.Score2);
                Debug.Assert(pScore != null);
            }

            GameObject pGameObject = (GameObject)pSubject.pGameObjB;

            CategoryAlien pAlien = null;

            // Update the scores
            switch (pGameObject.spriteName)
            {
                case Sprite.Name.Octopus:
                    pAlien = (CategoryAlien)pGameObject;
                    score += pAlien.GetScore();
                    pPlayer.AddScore(score);
                    pScore.UpdateText(score.ToString("D4"));
                    break;

                case Sprite.Name.Crab:
                    pAlien = (CategoryAlien)pGameObject;
                    score += pAlien.GetScore();
                    pPlayer.AddScore(score);
                    pScore.UpdateText(score.ToString("D4"));
                    break;

                case Sprite.Name.Squid:
                    pAlien = (CategoryAlien)pGameObject;
                    score += pAlien.GetScore();
                    pPlayer.AddScore(score);
                    pScore.UpdateText(score.ToString("D4"));
                    break;

                case Sprite.Name.UFO:
                    CategoryUFO pUFO = (CategoryUFO)pGameObject;
                    score += pUFO.GetScore();
                    pPlayer.AddScore(score);
                    pScore.UpdateText(score.ToString("D4"));
                    break;

                default:
                    break;
            }
        }
    }
}

// End of file
