main = """
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RepoScroller
{
    class Child
    {
        Rectangle rect, backRect;
        string source, current = "";
        int currentPosition = 0, currentWidth, currentHeight, maxWidth, maxHeight;
        public bool alive = true;

        public Child(Rectangle r, string s)
        {
            rect = r;
            rect.X = Math.Clamp(rect.X - Main.fontSize.X, 0, Main.screenSize.X);
            rect.Y = Math.Clamp(rect.Y - Main.fontSize.Y, 0, Main.screenSize.Y);
            source = s;

            if (rect.Right >= Main.screenSize.X)
            {
                rect.X = Main.screenSize.X - rect.Width - Main.fontSize.X;
            }

            if (rect.Bottom >= Main.screenSize.Y)
            {
                rect.Y = Main.screenSize.Y - rect.Height - Main.fontSize.Y;
            }

            maxWidth = rect.Width / Main.fontSize.X;
            maxHeight = rect.Height / Main.fontSize.Y;
            currentWidth = 0;
            currentHeight = 0;
        }

        public void Update()
        {
            current += source[currentPosition];
            currentPosition += 1;
            currentWidth += 1;
            if (currentWidth >= maxWidth)
            {
                current += "\n";
                currentWidth = 0;

                currentHeight++;
                if (currentHeight >= maxHeight)
                {
                    currentHeight--;
                    current = current.Substring(maxWidth + 1);
                }
            }

            if (currentPosition >= source.Length - 1)
            {
                alive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, float l)
        {
            spriteBatch.DrawString(Main.font, current, rect.Location.ToVector2(), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, l);
        }
    }
}
"""

print('\\n'.join(main.replace('"',"'").strip().split('\n')))
