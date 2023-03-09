using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RepoScroller
{
    public class Main : Game
    {
        public delegate void ScreenEvents();
        public static event ScreenEvents OnScreenResize;

        private GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        //public static Point screenSize = new Point(1920, 1080);
        public static Point screenSize = new Point(1200, 700);

        public static SpriteFont font;
        public static Point fontSize;
        public static Texture2D blank, twoXBlank;
        public static List<Texture2D> windowSprites;
        public static Random random = new Random();

        public static float compensation;
        public static int updateFrequency = 60;

        static bool showDebug = false;
        static float fps = 0, fpsTimer = 0;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = screenSize.X;
            graphics.PreferredBackBufferHeight = screenSize.Y;

            if (screenSize == new Point(1920, 1080))
            {
                graphics.IsFullScreen = true;
            }

            OnScreenResize += () =>
            {
                screenSize = new Point(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            };

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (s, e) =>
            {
                OnScreenResize(); // might be null
            };

            IsFixedTimeStep = false;

            graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Input.Initialize(new List<Keys>()
            {
                Keys.F11,
                Keys.F3
            });
            Parent.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("mainFont");
            blank = Content.Load<Texture2D>("blank");
            twoXBlank = Content.Load<Texture2D>("blankX2");
            windowSprites = GeneralDependencies.Split(Content.Load<Texture2D>("window"), (Content.Load<Texture2D>("window").Width / 3), (Content.Load<Texture2D>("window").Height / 3));

            fontSize = font.MeasureString("0").ToPoint();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            compensation = (float)gameTime.ElapsedGameTime.TotalSeconds * updateFrequency;

            Input.UpdateAll(Keyboard.GetState());
            Parent.Update();

            if (Input.collection[Keys.F3].active)
            {
                showDebug = !showDebug;
            }

            if (Input.collection[Keys.F11].active)
            {
                graphics.ToggleFullScreen();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(samplerState:SamplerState.PointClamp);
            Parent.Draw();
            if (showDebug)
            {
                fpsTimer += compensation;
                if (fpsTimer >= 60)
                {
                    fpsTimer = 0;
                    fps = (float)(1f / gameTime.ElapsedGameTime.TotalSeconds);
                }
                spriteBatch.DrawString(font, $"FPS : {MathF.Round(fps, 3)}\n" +
                    $"Children : {Parent.children.Count}", Vector2.Zero, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
