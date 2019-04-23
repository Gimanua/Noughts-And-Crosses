using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Noughts_And_Crosses.Actions.Spells;
using Noughts_And_Crosses.API;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Noughts_And_Crosses
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private const string QuoteUrl = "https://quotes.rest/qod.json";
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState MouseState;
        public static Random Random { get; } = new Random();
        public new static ContentManager Content { get; private set; }
        public static Point WindowMiddle { get; private set; }
        public static SpriteFont SpriteFont { get; private set; }
        public static readonly Dictionary<Enum, Texture2D> Textures = new Dictionary<Enum, Texture2D>();
        public static bool AlreadyPressing { get; private set; } = false;
        private static RestClient RestClient = new RestClient(QuoteUrl);
        private static string Quote;
        private GameState gameState = GameState.Menu;
        private PlayField PlayField { get; set; }
        private Vector2 CameraLocation { get; set; }
        private Matrix TransformMatrix { get { return Matrix.CreateTranslation(new Vector3(-CameraLocation, 0)); } }

        private enum GameState
        {
            Ingame,
            Menu
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content = base.Content;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            WindowMiddle = new Point(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            IsMouseVisible = true;
            /*
            try
            {
                var request = new RestRequest("/", Method.GET);
                IRestResponse response = RestClient.Execute(request);
                string content = response.Content;
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(content);
                Quote = rootObject.contents.quotes[0].quote;
            }
            catch
            {

            }
            */
            
            PlayField = new PlayField(PlayField.GameMode.Normal);
            Explosion.Grids = PlayField.Grids;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Content.Load<SpriteFont>("standard");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState = Mouse.GetState();
            KeyboardState keyBoardState = Keyboard.GetState();
            if (keyBoardState.IsKeyDown(Keys.Down))
                CameraLocation = new Vector2(CameraLocation.X, CameraLocation.Y + 5);
            if(keyBoardState.IsKeyDown(Keys.Up))
                CameraLocation = new Vector2(CameraLocation.X, CameraLocation.Y - 5);
            if (keyBoardState.IsKeyDown(Keys.Right))
                CameraLocation = new Vector2(CameraLocation.X + 5, CameraLocation.Y);
            if (keyBoardState.IsKeyDown(Keys.Left))
                CameraLocation = new Vector2(CameraLocation.X - 5, CameraLocation.Y);

            PlayField.Update(MouseState, CameraLocation, gameTime);
            AlreadyPressing = MouseState.LeftButton == ButtonState.Pressed;
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: TransformMatrix);

            if(gameState == GameState.Menu && !string.IsNullOrEmpty(Quote))
            {
                spriteBatch.DrawString(SpriteFont, Quote, new Vector2(0, 0), Color.White);
            }
            PlayField.Draw(spriteBatch);
            spriteBatch.End();

            #region DEBUG
            //spriteBatch.Begin();

            //spriteBatch.DrawString(SpriteFont, $"MousePoint (X:{MouseState.Position.X},Y:{MouseState.Position.Y})", new Vector2(10, 10), Color.Red);
            //spriteBatch.DrawString(SpriteFont, $"CameraPoint (X:{CameraLocation.ToPoint().X},Y:{CameraLocation.ToPoint().Y})", new Vector2(10, 30), Color.Red);
            //spriteBatch.DrawString(SpriteFont, $"Combined (X:{(MouseState.Position + CameraLocation.ToPoint()).X},Y:{(MouseState.Position + CameraLocation.ToPoint()).Y})", new Vector2(10, 50), Color.Red);
            //var _temp = GameObject.LogicalPosition.GetLogicalPosition(MouseState.Position, CameraLocation.ToPoint());
            //spriteBatch.DrawString(SpriteFont, $"Function position (X:{_temp.X},Y:{_temp.Y})", new Vector2(10, 70), Color.Red);

            //spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }
    }
}
