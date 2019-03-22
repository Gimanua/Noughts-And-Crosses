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
        private static RestClient RestClient = new RestClient(QuoteUrl);
        private static string Quote;
        private GameState gameState = GameState.Menu;

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
            // TODO: Add your initialization logic here
            WindowMiddle = new Point(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
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
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("standard");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // TODO: Add your update logic here

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


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
