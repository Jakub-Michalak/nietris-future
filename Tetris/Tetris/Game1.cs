
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using Windows.Foundation;

namespace Tetris
{
    public class Game1 : Game
{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        RenderTarget2D themeRenderer;
        RenderTarget2D boardRenderer;

        Texture2D IBlockTex;
        Texture2D JBlockTex;
        Texture2D LBlockTex;
        Texture2D OBlockTex;
        Texture2D SBlockTex;
        Texture2D TBlockTex;
        Texture2D ZBlockTex;
        Texture2D BackgroundTex;
        Texture2D OverlayTex;

        public float timepassed;



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
        char[,] temp = new char[40, 10];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 144.0f);

            IsFixedTimeStep = true;

            base.Initialize();
        }

       

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            themeRenderer = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            boardRenderer = new RenderTarget2D(GraphicsDevice, 440, 880);

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


        protected override void UnloadContent()
        {
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

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            timepassed = gameTime.ElapsedGameTime.Milliseconds;
            


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
                //TODO: Wróć do MainPage/PlayPage
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed  || Keyboard.GetState().IsKeyDown(Keys.Left))
            {

            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right))
            {

            }





            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>


        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(boardRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Rectangle kloc = new Rectangle(j * 44, i * 44, 44, 44);

                    if (board[i + 20, j] == 'i') spriteBatch.Draw(IBlockTex, kloc, Color.White);
                    if (board[i + 20, j] == 'o') spriteBatch.Draw(OBlockTex, kloc, Color.White);
                    if (board[i + 20, j] == 't') spriteBatch.Draw(TBlockTex, kloc, Color.White);
                    if (board[i + 20, j] == 's') spriteBatch.Draw(SBlockTex, kloc, Color.White);
                    if (board[i + 20, j] == 'z') spriteBatch.Draw(ZBlockTex, kloc, Color.White);
                    if (board[i + 20, j] == 'j') spriteBatch.Draw(JBlockTex, kloc, Color.White);
                    if (board[i + 20, j] == 'l') spriteBatch.Draw(LBlockTex, kloc, Color.White);
                }
            }
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(themeRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            spriteBatch.Draw(BackgroundTex, new Rectangle(0, 0, 1920, 1080), Color.White);
            spriteBatch.Draw(boardRenderer, new Rectangle(740, 100, 440, 880), Color.White);
            spriteBatch.Draw(OverlayTex, new Rectangle(480, 0, 960, 1080), Color.White);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);



            spriteBatch.Begin();

            spriteBatch.Draw(themeRenderer, GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
