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
        int currentPosition = 0, maxWidth, maxHeight;
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

            backRect.X = rect.X - Main.fontSize.X;
            backRect.Y = rect.Y - Main.fontSize.Y;
            backRect.Width = rect.Width + (Main.fontSize.X * 2);
            backRect.Height = rect.Height + (Main.fontSize.Y * 2);

            maxWidth = rect.Width / Main.fontSize.X;
            maxHeight = rect.Height / Main.fontSize.Y;
            /*currentWidth = 0;
            currentHeight = 0;*/
        }

        public void Update()
        {
            current += source[currentPosition];
            currentPosition += 1;

            Point size = Main.font.MeasureString(current).ToPoint();
            if (size.X >= (Main.fontSize.X * maxWidth))
            {
                current = current.Insert(current.Length - 2, "\n");
            }
            size = Main.font.MeasureString(current).ToPoint();
            if (size.Y >= (Main.fontSize.Y * maxHeight))
            {
                int _index = 0;
                for (int i = 0; i < current.Length; i++)
                {
                    if (current[i] == '\n')
                    {
                        _index = i;
                        break;
                    }
                }
                current = current.Substring(_index + 1);
            }

            if (currentPosition >= source.Length - 1)
            {
                alive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, float l)
        {
            GeneralDependencies.NineSliceDraw(spriteBatch, Main.windowSprites, backRect, Main.fontSize.X, Color.White, l);
            spriteBatch.DrawString(Main.font, current, rect.Location.ToVector2(), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, l);
        }
    }
}
