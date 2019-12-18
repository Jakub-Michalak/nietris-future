
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using Windows.Foundation;

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
        Texture2D BackgroundTex;
        Texture2D OverlayTex;


        string theme = PlayPage.P1.GetTheme();

        char[,] board = new char[40, 10] {
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 's', 'n', 'n', 'n', 'n', 'n', 'n', 'n' },
        { 'n', 'n', 's', 's', 'n', 'n', 'n', 'n', 'n', 'i' },
        { 'l', 'o', 'o', 's', 'j', 'j', 'j', 'n', 'n', 'i' },
        { 'l', 'o', 'o', 't', 'z', 'z', 'j', 'n', 'n', 'i' },
        { 'l', 'l', 't', 't', 't', 'z', 'z', 'n', 'n', 'i' },
        }; //TEST

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

            Board board = new Board();
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

            // TODO: use this.Content to load your game content here <- nie działało
            FileStream fileStream;
            fileStream = new FileStream($@"Content/Themes/{theme}/I.png", FileMode.Open, FileAccess.Read);
            IBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/J.png", FileMode.Open, FileAccess.Read);
            JBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/L.png", FileMode.Open, FileAccess.Read);
            LBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/O.png", FileMode.Open, FileAccess.Read);
            OBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/S.png", FileMode.Open, FileAccess.Read);
            SBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/T.png", FileMode.Open, FileAccess.Read);
            TBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/Z.png", FileMode.Open, FileAccess.Read);
            ZBlockTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/BCG.jpg", FileMode.Open, FileAccess.Read);
            BackgroundTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream($@"Content/Themes/{theme}/Overlay.png", FileMode.Open, FileAccess.Read);
            OverlayTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Dispose();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            IBlockTex.Dispose();
            JBlockTex.Dispose();
            LBlockTex.Dispose();
            OBlockTex.Dispose();
            SBlockTex.Dispose();
            TBlockTex.Dispose();
            ZBlockTex.Dispose();
            BackgroundTex.Dispose();
            OverlayTex.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
                //TODO: Wróć do MainPage/PlayPage
            }

            


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Magenta);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(BackgroundTex, GraphicsDevice.Viewport.Bounds, Color.White);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    Rectangle kloc = new Rectangle(j * 44, i * 44, 44, 44);


                    if (board[i + 20, j] == 'i') spriteBatch.Draw(IBlockTex, kloc , Color.White);
                    if (board[i + 20, j] == 'o') spriteBatch.Draw(OBlockTex, kloc , Color.White);
                    if (board[i + 20, j] == 't') spriteBatch.Draw(TBlockTex, kloc , Color.White);
                    if (board[i + 20, j] == 's') spriteBatch.Draw(SBlockTex, kloc , Color.White);
                    if (board[i + 20, j] == 'z') spriteBatch.Draw(ZBlockTex, kloc , Color.White);
                    if (board[i + 20, j] == 'j') spriteBatch.Draw(JBlockTex, kloc , Color.White);
                    if (board[i + 20, j] == 'l') spriteBatch.Draw(LBlockTex, kloc , Color.White);



                }
            }
            spriteBatch.Draw(OverlayTex, new Rectangle(GraphicsDevice.Viewport.Width/4,0, GraphicsDevice.Viewport.Width /2, GraphicsDevice.Viewport.Height),Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
