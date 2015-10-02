using Microsoft.Xna.Framework;

namespace TutorialRPG
{
    public class ImageEffect
    {
        protected Image Image { get; private set; }
        public bool IsActive { get; set; }

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            Image = image;
        }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime) { }
    }
}
