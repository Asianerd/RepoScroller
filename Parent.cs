using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace RepoScroller
{
    class Parent
    {
        public static List<string> sources;
        public static List<Child> children;

        static float i = 0, target = 100f;

        public static void Initialize()
        {
            sources = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("source.json"));
            children = new List<Child>();
        }

        public static void Update()
        {
            i += Main.compensation;
            if (i >= target)
            {
                i = 0;

                children.Add(new Child(new Rectangle(
                    Main.random.Next(0, Main.screenSize.X),
                    Main.random.Next(0, Main.screenSize.Y),
                    Main.random.Next(40, 50) * Main.fontSize.X,
                    Main.random.Next(20, 30) * Main.fontSize.Y
                    ), sources[Main.random.Next(0, sources.Count)]));
            }

            foreach (Child x in children)
            {
                x.Update();
            }

            children = children.Where(n => n.alive).ToList();
        }

        public static void Draw()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Draw(Main.spriteBatch, i / children.Count);
            }
        }
    }
}
