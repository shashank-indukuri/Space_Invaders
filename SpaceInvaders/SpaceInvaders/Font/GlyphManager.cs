using System;
using System.Diagnostics;
using System.Xml;

namespace SpaceInvaders
{
    public class GlyphManager : BaseManager
    {        
        
        // Constructor
        // kicking the Can to the Base classes
        public GlyphManager(int InitialNumReserved = 3, int DeltaGrow = 1)
                : base(new DoubleLinkManager(), new DoubleLinkManager(), InitialNumReserved, DeltaGrow)
        {
            // LTN - GlyphManager
            poNodeToFind = new Glyph();
        }

        // Static Methods
        public static void Create(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // The values given should be atleast 1
            Debug.Assert(InitialNumReserved > 0);
            Debug.Assert(DeltaGrow > 0);

            // Only create a the instance if only is null
            Debug.Assert(poInstance == null);

            // Creating the new Glyph Manager
            if (poInstance == null)
            {
                // LTN - It's a singleton and owned by the application.exe
                poInstance = new GlyphManager(InitialNumReserved, DeltaGrow);
            }

            Debug.Assert(poInstance != null);
        }
        public static void Destroy()
        {
            GlyphManager pGlyphMan = PrivGetInstance();

            Debug.Assert(pGlyphMan != null);

            // Printing the states
            Dump();

            // Invalidating the instance of Manager
            poInstance = null;
        }

        public static Glyph Add(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();

            Glyph pNode = (Glyph)pMan.BaseAddToFront();
            Debug.Assert(pNode != null);

            pNode.SetValues(name, key, textName, x, y, width, height);
            return pNode;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            // STN - It's used to read the xml
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            // Simple hack
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // Reading each element
                    case XmlNodeType.Element:
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element 
                        if (reader.Name == "character")
                        {
                            // Once all the data is read, store in the glyphManager
                            // Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            GlyphManager.Add(glyphName, key, textName, x, y, width, height);
                        }
                        break;
                }
            }

            // Debug.Write("\n");
        }

        public static Glyph Find(Glyph.Name name, int key)
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare two nodes
            pMan.poNodeToFind.name = name;
            pMan.poNodeToFind.key = key;

            Glyph pData = (Glyph)pMan.BaseFind(pMan.poNodeToFind);
            return pData;
        }

        public static void Remove(Glyph pImage)
        {
            Debug.Assert(pImage != null);

            GlyphManager pGlyphMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pGlyphMan != null);

            pGlyphMan.BaseRemove(pImage);
        }

        public static void Dump()
        {
            GlyphManager pGlyphMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pGlyphMan != null);

            pGlyphMan.BaseDump();
        }

        // Private Methods
        private static GlyphManager PrivGetInstance()
        {
            // Make sure the Manager instance is created first
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        // Overriding method
        protected override BaseNode derivedConstructNode()
        {
            // LTN - GlyphManager
            BaseNode pGlyph = new Glyph();
            Debug.Assert(pGlyph != null);

            // Return a newly created Glyph
            return pGlyph;
        }

        // Data
        private readonly Glyph poNodeToFind;
        private static GlyphManager poInstance = null;
    }
}

// End of file
