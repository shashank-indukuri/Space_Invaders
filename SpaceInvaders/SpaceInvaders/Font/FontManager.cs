using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FontManager : BaseManager
    {
        // Constructor
        // kicking the Can to the Base classes
        public FontManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - FontManager
            poNodeToFind = new Font();

            psActiveInstance = null;
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(psInstance == null);

            // Creating the new Font Manager
            if (psInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                psInstance = new FontManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(psInstance != null);
        }
        public static void Destroy()
        {
            FontManager pFontMan = psActiveInstance;

            Debug.Assert(pFontMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            psInstance = null;
        }

        public static Font Add(Font.Name name, SpriteBatch.Name SBName, string pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            FontManager pFontMan = psActiveInstance;

            Font pNode = (Font)pFontMan.BaseAddToFront();
            Debug.Assert(pNode != null);

            pNode.SetValues(name, pMessage, glyphName, xStart, yStart);

            // Add to sprite batch
            SpriteBatch pSBName = SpriteBatchManager.Find(SBName);
            Debug.Assert(pSBName != null);
            Debug.Assert(pNode.poSpriteFont != null);
            pSBName.Link(pNode.poSpriteFont);

            return pNode;
        }

        public static void AddXml(Glyph.Name glyphName, string assetName, Texture.Name textName)
        {
            GlyphManager.AddXml(glyphName, assetName, textName);
        }

        public static void SetActiveFont(FontManager pFontManager)
        {
            FontManager pFontMan = FontManager.PrivGetInstance();
            Debug.Assert(pFontMan != null);

            Debug.Assert(pFontManager != null);
            FontManager.psActiveInstance = pFontManager;
        }

        public static Font Find(Font.Name name)
        {
            FontManager pFontMan = psActiveInstance;
            Debug.Assert(pFontMan != null);

            // Compare two nodes
            FontManager.poNodeToFind.name = name;

            Font pFont = (Font)pFontMan.BaseFind(FontManager.poNodeToFind);

            return pFont;
        }

        public static void RemoveAll()
        {
            // current active instance
            FontManager pFontMan = FontManager.psActiveInstance;


            BaseIterator pIterator = pFontMan.BaseFetchIterator();
            Debug.Assert(pIterator != null);

            Font pNode = (Font)pIterator.First();
            Font pNextNode = null;

            // Walk through the nodes
            while (!pIterator.IsDone())
            {
                pNextNode = (Font)pIterator.Next();

                // Remove from the current list
                pFontMan.BaseRemove(pNode);

                // Next node
                pNode = pNextNode;
            }
        }


        public static void Remove(Font pCurrentNode)
        {
            FontManager pFontMan = FontManager.psActiveInstance;

            // Remove the current node from the manager
            SpriteNode pSpriteNode = pCurrentNode.poSpriteFont.GetSpriteNode();
            Debug.Assert(pSpriteNode != null);
            SpriteBatchManager.Remove(pSpriteNode);

            pFontMan.BaseRemove(pCurrentNode);
        }

        public static void Dump()
        {
            FontManager pFontMan = FontManager.psActiveInstance;
            Debug.Assert(pFontMan != null);

            pFontMan.BaseDump();
        }

        public static void UpdateScore()
        {
            Font pScore1 = FontManager.Find(Font.Name.Score1);
            Debug.Assert(pScore1 != null);

            Font pScore2 = FontManager.Find(Font.Name.Score2);
            Debug.Assert(pScore1 != null);

            Font pHighScore = FontManager.Find(Font.Name.HiScore);
            Debug.Assert(pHighScore != null);

            Font pLifes = FontManager.Find(Font.Name.Lifes);

            Player pPlayer1 = PlayerManager.Find(Player.Name.Player1);
            int score = pPlayer1.GetScore();

            Player pPlayer2 = PlayerManager.Find(Player.Name.Player2);
            int score2 = pPlayer2.GetScore();

            int highScore = PlayerManager.GetHighScore();

            Player pActivePlayer = PlayerManager.GetActivePlayer();

            int lifes = 0;

            if (pActivePlayer != null)
            {
                lifes = pActivePlayer.GetNumOfLifes();
            }

            pScore1.UpdateText(score.ToString("D4"));
            pScore2.UpdateText(score2.ToString("D4"));

            pHighScore.UpdateText(highScore.ToString("D4"));

            if (pLifes != null)
            {
                pLifes.UpdateText(lifes.ToString());
            }


        }

        // Private Methods
        private static FontManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - FontManager
            BaseNode pFont = new Font();
            Debug.Assert(pFont != null);

            // Return a newly created Font
            return pFont;
        }

        // Data
        private static Font poNodeToFind;
        private static FontManager psInstance = null;
        private static FontManager psActiveInstance = null;

    }
}

// End of file