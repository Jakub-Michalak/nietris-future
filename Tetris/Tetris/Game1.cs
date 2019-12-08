
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Tetris
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D IBlockTex;
    Texture2D JBlockTex;
    Texture2D LBlockTex;
    Texture2D OBlockTex;
    Texture2D SBlockTex;
    Texture2D TBlockTex;
    Texture2D ZBlockTex;

    string theme = PlayPage.P1.GetTheme();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            // TODO: use this.Content to load your game content here

            FileStream fileStream = new FileStream($@"Content/Themes/{theme}/I.png", FileMode.Open);
            IBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/J.png", FileMode.Open);
            JBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/L.png", FileMode.Open);
            LBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/O.png", FileMode.Open);
            OBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/S.png", FileMode.Open);
            SBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/T.png", FileMode.Open);
            TBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/Z.png", FileMode.Open);
            ZBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Dispose();

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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
