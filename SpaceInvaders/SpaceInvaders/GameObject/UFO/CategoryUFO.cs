using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CategoryUFO : Leaf
    {
        // Enum
        public enum UFOType
        {
            UFO,
            UFOGroup,
            Unitialized
        }

        // Contructor
        protected CategoryUFO(GameObject.Name name, Sprite.Name spriteName, float posX, float posY, CategoryUFO.UFOType ufoType)
            : base(name, spriteName, posX, posY)
        {
            this.ufoType = ufoType;
        }

        ~CategoryUFO()
        {
        }

        abstract public int GetScore();


        // Data
        protected CategoryUFO.UFOType ufoType;
    }
}

// End of file