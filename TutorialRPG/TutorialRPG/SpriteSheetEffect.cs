using Microsoft.Xna.Framework;

namespace TutorialRPG
{
    public class SpriteSheetEffect : ImageEffect
    {
        public int FrameWidth
        {
            get
            {
                if (Image.Texture != null)
                    return Image.Texture.Width / (int)amountOfFrames.X;
                return 0;
            }
        }

        public int FrameHeight
        {
            get
            {
                if (Image.Texture != null)
                    return Image.Texture.Height / (int)amountOfFrames.Y;
                return 0;
            }
        }

        public int FrameCounter { get; set; }
        public int SwitchFrame { get; set; }
        public Vector2 currentFrame;
        public Vector2 amountOfFrames;

        public SpriteSheetEffect()
        {
            amountOfFrames = new Vector2(3, 4);
            currentFrame = Vector2.UnitX;
            SwitchFrame = 100;
            FrameCounter = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Image.IsActive)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter > SwitchFrame)
                {
                    FrameCounter = 0;
                    currentFrame.X++;

                    if (currentFrame.X >= amountOfFrames.X)
                        currentFrame.X = 0;
                }
            }
            else
                currentFrame.X = 1;
            
            Image.SourceRect = 
                new Rectangle((int)currentFrame.X * FrameWidth,
                              (int)currentFrame.Y * FrameHeight,
                              FrameWidth, FrameHeight);
        }
    }
}
