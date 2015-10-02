using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TutorialRPG
{
    public class FadeEffect : ImageEffect
    {
        public float FadeSpeed { get; set; }
        public bool Increase { get; set; }

        public FadeEffect()
        {
            FadeSpeed = 1;
            Increase = false;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Image.IsActive)
            {
                if (!Increase)
                    Image.Alpha -= FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    Image.Alpha += FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Image.Alpha < 0)
                {
                    Increase = true;
                    Image.Alpha = 0;
                }
                else if(Image.Alpha > 1f)
                {
                    Increase = false;
                    Image.Alpha = 1f;
                }
            }
            else
            {
                Image.Alpha = 1f;
            }
        }
    }
}
