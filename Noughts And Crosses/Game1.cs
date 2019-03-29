using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
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
        SpriteFont spriteFont;
        public static ContentManager Content;
        public static Point WindowMiddle { get; private set; }
        public static readonly Dictionary<Enum, Texture2D> Textures = new Dictionary<Enum, Texture2D>();
        public static bool AlreadyPressing { get; private set; } = false;
        private static RestClient RestClient = new RestClient(QuoteUrl);
        private static string Quote;
        private GameState gameState = GameState.Menu;
        private PlayField PlayField { get; set; }

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // BUGGAD PÅ HEMDATOR, BORTTAGEN FÖR TILLFÄLLET spriteFont = Content.Load<SpriteFont>("standard");
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

            MouseState mouseState = Mouse.GetState();
            PlayField.Update(mouseState);
            AlreadyPressing = mouseState.LeftButton == ButtonState.Pressed;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if(gameState == GameState.Menu && !string.IsNullOrEmpty(Quote))
            {
                spriteBatch.DrawString(spriteFont, Quote, new Vector2(0, 0), Color.White);
            }
            PlayField.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
