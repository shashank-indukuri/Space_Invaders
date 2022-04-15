using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AnimationCommand : BaseCommand
    {

        // Constructor
        public AnimationCommand(Sprite.Name name)
            : base()
        {
            // Find the sprite
            pSprite = SpriteManager.Find(name);
            Debug.Assert(pSprite != null);

            // Initialize the DoubleLink Manager
            // LTN - TimerEventManager
            poDoubleLinkMan = new DoubleLinkManager();
            Debug.Assert(poDoubleLinkMan != null);

            // Fecth the Iterator
            pIterator = poDoubleLinkMan.FetchIterator();
            Debug.Assert(pIterator != null);
        }

        // Method
        public void Link(Image.Name name)
        {
            // Find the Image
            Image pImage = ImageManager.Find(name);
            Debug.Assert(pImage != null);

            // Create a new link
            // LTN - TimerEventManager
            ImageNode pImageNode = new ImageNode(pImage);
            Debug.Assert(pImageNode != null);

            // Add the new Image Node to the front of the Sprite Animation
            poDoubleLinkMan.AddNodeToFront(pImageNode);

            // Update the current iterator
            pIterator = poDoubleLinkMan.FetchIterator();
        }

        // Overriding method
        public override void Execute(float deltaTime)
        {
            // Go to next image
            ImageNode pImgNode = (ImageNode)pIterator.Current();
            Debug.Assert(pImgNode != null);

            if (pIterator.Next() == null)
            {
                // Loop it 
                pIterator.First();
            }

            // Swap the image
            this.pSprite.SwapImage(pImgNode.pImage);

            // Add the sprite back to the timer
            TimerEventManager.Add((TimerEvent.Name)pSprite.GetName(), deltaTime, this);
        }

        // Data
        public Sprite pSprite;
        public DoubleLinkManager poDoubleLinkMan;
        public BaseIterator pIterator;
    }
}

// End of file