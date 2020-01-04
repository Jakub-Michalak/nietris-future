
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;

namespace Tetris
{
    public class Game1 : Game
{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        RenderTarget2D themeRenderer;
        RenderTarget2D boardRenderer;
        RenderTarget2D nextRenderer;
        RenderTarget2D holdRenderer;

        Texture2D IBlockTex;
        Texture2D JBlockTex;
        Texture2D LBlockTex;
        Texture2D OBlockTex;
        Texture2D SBlockTex;
        Texture2D TBlockTex;
        Texture2D ZBlockTex;
        Texture2D BackgroundTex;
        Texture2D OverlayTex;
        Texture2D GridTex;

        public double timepassed;
        public double timer;

        List<Tetromino> nextTetromino = BagRandomizer.GetNewBag();
        Tetromino currentTetromino;
        Tetromino heldTetromino;
        Tetromino tempTetromino;

        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;

        int level = 1;
        int score = 0;
        int linesCleared = 0;
        int singleCleared = 0;
        int doublesCleared = 0;
        int tripleCleared = 0;
        int tetrisCleared = 0;
        int linesClearedSimultaneously;

        bool isBlockPlaced = false;
        bool blockHeld = false;
        bool canMove = false;

        static List<Vector2> CurrentBlockPositions;

        Tetromino.rotations currentRotation;




        public enum Directions
        {
            Down,
            Left,
            Right
        }

        string theme = PlayPage.P1.GetTheme();
        string audioPack = PlayPage.P1.GetAudioPack();

        char?[,] board = new char?[10, 40];


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 144.0f);
            score = 0;

            IsFixedTimeStep = true;
            currentTetromino = nextTetromino[0];
            nextTetromino.RemoveAt(0);
            CurrentBlockPositions = currentTetromino.StartingPosition();


            base.Initialize();
        }

       

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Song BGM;

            themeRenderer = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            boardRenderer = new RenderTarget2D(GraphicsDevice, 440, 880);
            nextRenderer = new RenderTarget2D(GraphicsDevice, 264, 880 );
            holdRenderer = new RenderTarget2D(GraphicsDevice, 264, 264);

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
            fileStream = new FileStream($@"Content/Themes/{theme}/Grid.png", FileMode.Open, FileAccess.Read);
            GridTex = Texture2D.FromStream(GraphicsDevice, fileStream);
            BGM = Content.Load<Song>($"Audio/{audioPack}/BGM");
            MediaPlayer.Play(BGM);
            MediaPlayer.IsRepeating = true;
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

        protected void DeleteLines()
        {
            linesClearedSimultaneously = 0;
            for (int i = 0; i < 40; i++)
            {
                if (board[0, i] != null && board[1, i] != null && board[2, i] != null && board[3, i] != null && board[4, i] != null && board[5, i] != null && board[6, i] != null && board[7, i] != null && board[8, i] != null && board[9, i] != null)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        board[j, i] = null;  
                    }

                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            board[j, k] = board[j, k - 1];
                        }
                    }
                    linesClearedSimultaneously++;
                }
            }
            if (linesClearedSimultaneously == 1) singleCleared++;
            if (linesClearedSimultaneously == 2) doublesCleared++;
            if (linesClearedSimultaneously == 3) tripleCleared++;
            if (linesClearedSimultaneously == 4) tetrisCleared++;
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            timepassed = gameTime.ElapsedGameTime.TotalSeconds;
            currentKeyboardState = Keyboard.GetState();


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
                //TODO: Wróć do MainPage/PlayPage
            }

            if (nextTetromino.Count < 7) nextTetromino.AddRange(BagRandomizer.GetNewBag());

            if (isBlockPlaced)
            {
                currentTetromino = nextTetromino[0];
                CurrentBlockPositions = currentTetromino.StartingPosition();
                nextTetromino.RemoveAt(0);
                currentRotation = Tetromino.rotations.rotation1;
                //UWPConsole.Console.WriteLine($"current: {currentTetromino.PieceSymbol()}");
                //UWPConsole.Console.WriteLine($"next: {nextTetromino[0].PieceSymbol()}");
                //UWPConsole.Console.WriteLine($"queued: {nextTetromino.Count}");
                isBlockPlaced = false;
                blockHeld = false;
            }

            timer += timepassed;
            if(timer >= Math.Pow((0.8-((level-1)*0.007)),(level-1)))
            {
                Move(Directions.Down);
                timer = 0;
            }

            if (blockHeld == false && (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.LeftShift) && lastKeyboardState.IsKeyUp(Keys.LeftShift)))
            {
                if (heldTetromino == null)
                {
                    heldTetromino = currentTetromino;
                    currentTetromino = nextTetromino[0];
                    CurrentBlockPositions = currentTetromino.StartingPosition();
                    nextTetromino.RemoveAt(0);
                }
                else
                {
                    tempTetromino = currentTetromino;
                    currentTetromino = heldTetromino;
                    heldTetromino = tempTetromino;
                    CurrentBlockPositions = currentTetromino.StartingPosition();
                }
                blockHeld = true;
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Up) && lastKeyboardState.IsKeyUp(Keys.Up))
            {
                //HARD DROP
                isBlockPlaced = true;
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed  || currentKeyboardState.IsKeyDown(Keys.Left) && lastKeyboardState.IsKeyUp(Keys.Left))
            {
                Move(Directions.Left);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Right) && lastKeyboardState.IsKeyUp(Keys.Right))
            {
                Move(Directions.Right);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Down) && lastKeyboardState.IsKeyUp(Keys.Down))
            {
                Move(Directions.Down);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space))
            {
                foreach(Vector2 v in CurrentBlockPositions)
                {
                    board[(int)v.X, 20 + (int)v.Y ] = currentTetromino.PieceSymbol();
                }
                isBlockPlaced = true;
            }

            DeleteLines();




            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.X) && lastKeyboardState.IsKeyUp(Keys.X))
            {
                CurrentBlockPositions = currentTetromino.Rotate(CurrentBlockPositions, currentRotation, Tetromino.rotationDirection.clockwise,ref board);
                if (currentRotation == Tetromino.rotations.rotation1) currentRotation = Tetromino.rotations.rotation2;
                else if (currentRotation == Tetromino.rotations.rotation2) currentRotation = Tetromino.rotations.rotation3;
                else if (currentRotation == Tetromino.rotations.rotation3) currentRotation = Tetromino.rotations.rotation4;
                else if (currentRotation == Tetromino.rotations.rotation4) currentRotation = Tetromino.rotations.rotation1;
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Z) && lastKeyboardState.IsKeyUp(Keys.Z))
            {
                CurrentBlockPositions = currentTetromino.Rotate(CurrentBlockPositions, currentRotation, Tetromino.rotationDirection.counterclockwise, ref board);
                if (currentRotation == Tetromino.rotations.rotation1) currentRotation = Tetromino.rotations.rotation4;
                else if (currentRotation == Tetromino.rotations.rotation2) currentRotation = Tetromino.rotations.rotation1;
                else if (currentRotation == Tetromino.rotations.rotation3) currentRotation = Tetromino.rotations.rotation2;
                else if (currentRotation == Tetromino.rotations.rotation4) currentRotation = Tetromino.rotations.rotation3;
            }


            lastKeyboardState = currentKeyboardState;

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        public int MaxPositionX()
        {
            int max = 0;
            foreach(Vector2 v in CurrentBlockPositions)
            {
                if (v.X > max) max = (int)v.X;
            }
            return max;
        }

        public int MinPositionX()
        {
            int min = 10;
            foreach (Vector2 v in CurrentBlockPositions)
            {
                if (v.X < min) min = (int)v.X;
            }
            return min;
        }

        public int MaxPositionY()
        {
            int max = 0;
            foreach (Vector2 v in CurrentBlockPositions)
            {
                if (v.Y > max) max = (int)v.Y;
            }
            return max;
        }

        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.Left:
                    if (MinPositionX() - 1 >= 0)
                    {
                        foreach (Vector2 v in CurrentBlockPositions)
                        {
                            if (board[(int)v.X - 1, 20 + (int)v.Y] == null)
                            {
                                canMove = true;
                            }
                            else
                            {
                                canMove = false;
                                break;
                            }
                        }
                        if (canMove == true)
                        {
                            for (int i = 0; i < CurrentBlockPositions.Count; i++)
                            {
                                CurrentBlockPositions[i] = new Vector2(CurrentBlockPositions[i].X - 1, CurrentBlockPositions[i].Y);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
                case Directions.Right:
                    if (MaxPositionX() + 1 < 10)
                    {
                        foreach (Vector2 v in CurrentBlockPositions)
                        {
                            if (board[(int)v.X + 1, 20 + (int)v.Y ] == null)
                            {
                                canMove = true;
                            }
                            else
                            {
                                canMove = false;
                                break;
                            }
                        }
                        if (canMove == true)
                        {
                            for (int i = 0; i < CurrentBlockPositions.Count; i++)
                            {
                                CurrentBlockPositions[i] = new Vector2(CurrentBlockPositions[i].X + 1, CurrentBlockPositions[i].Y);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
                case Directions.Down:
                    if (MaxPositionY() + 21 < 40)
                    {
                        foreach (Vector2 v in CurrentBlockPositions)
                        {
                            if (board[(int)v.X ,21 + (int)v.Y ] == null)
                            {
                                canMove = true;
                            }
                            else
                            {
                                canMove = false;
                                break;
                            }
                        }
                        if (canMove == true)
                        {
                            for (int i = 0; i < CurrentBlockPositions.Count; i++)
                            {
                                CurrentBlockPositions[i] = new Vector2(CurrentBlockPositions[i].X , CurrentBlockPositions[i].Y + 1);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
            }
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>


        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(nextRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            int starting = 1;
            for (int i = 0; i < 5; i++)
            {
                
                foreach (Vector2 v in nextTetromino[i].StartingPosition())
                {
                    Rectangle kloc = new Rectangle(((int)v.X-3) * 44,((int)v.Y+2) * 44 + ( 44 * starting), 44, 44);

                    if (nextTetromino[i].PieceSymbol() == 'i') spriteBatch.Draw(IBlockTex, kloc, Color.White);
                    if (nextTetromino[i].PieceSymbol() == 'o') spriteBatch.Draw(OBlockTex, kloc, Color.White);
                    if (nextTetromino[i].PieceSymbol() == 't') spriteBatch.Draw(TBlockTex, kloc, Color.White);
                    if (nextTetromino[i].PieceSymbol() == 's') spriteBatch.Draw(SBlockTex, kloc, Color.White);
                    if (nextTetromino[i].PieceSymbol() == 'z') spriteBatch.Draw(ZBlockTex, kloc, Color.White);
                    if (nextTetromino[i].PieceSymbol() == 'j') spriteBatch.Draw(JBlockTex, kloc, Color.White);
                    if (nextTetromino[i].PieceSymbol() == 'l') spriteBatch.Draw(LBlockTex, kloc, Color.White);

                }
                starting += 4;
            }
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(holdRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);


            if (heldTetromino != null)
            {
                foreach (Vector2 v in heldTetromino.StartingPosition())
                {
                    Rectangle kloc = new Rectangle(((int)v.X - 3) * 44, ((int)v.Y + 2) * 44, 44, 44);

                    if (heldTetromino.PieceSymbol() == 'i') spriteBatch.Draw(IBlockTex, kloc, Color.White);
                    if (heldTetromino.PieceSymbol() == 'o') spriteBatch.Draw(OBlockTex, kloc, Color.White);
                    if (heldTetromino.PieceSymbol() == 't') spriteBatch.Draw(TBlockTex, kloc, Color.White);
                    if (heldTetromino.PieceSymbol() == 's') spriteBatch.Draw(SBlockTex, kloc, Color.White);
                    if (heldTetromino.PieceSymbol() == 'z') spriteBatch.Draw(ZBlockTex, kloc, Color.White);
                    if (heldTetromino.PieceSymbol() == 'j') spriteBatch.Draw(JBlockTex, kloc, Color.White);
                    if (heldTetromino.PieceSymbol() == 'l') spriteBatch.Draw(LBlockTex, kloc, Color.White);

                }
            }


            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            


            GraphicsDevice.SetRenderTarget(boardRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Rectangle kloc = new Rectangle(j * 44, i * 44, 44, 44);


                    if (board[j,i+20] == 'i') spriteBatch.Draw(IBlockTex, kloc, Color.White);
                    if (board[j,i+20] == 'o') spriteBatch.Draw(OBlockTex, kloc, Color.White);
                    if (board[j,i+20] == 't') spriteBatch.Draw(TBlockTex, kloc, Color.White);
                    if (board[j,i+20] == 's') spriteBatch.Draw(SBlockTex, kloc, Color.White);
                    if (board[j,i+20] == 'z') spriteBatch.Draw(ZBlockTex, kloc, Color.White);
                    if (board[j,i+20] == 'j') spriteBatch.Draw(JBlockTex, kloc, Color.White);
                    if (board[j,i+20] == 'l') spriteBatch.Draw(LBlockTex, kloc, Color.White);
                }
            }
            foreach(Vector2 v in CurrentBlockPositions)
            {
                if (currentTetromino.PieceSymbol() == 'i') spriteBatch.Draw(IBlockTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
                if (currentTetromino.PieceSymbol() == 'o') spriteBatch.Draw(OBlockTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
                if (currentTetromino.PieceSymbol() == 't') spriteBatch.Draw(TBlockTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
                if (currentTetromino.PieceSymbol() == 's') spriteBatch.Draw(SBlockTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
                if (currentTetromino.PieceSymbol() == 'z') spriteBatch.Draw(ZBlockTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
                if (currentTetromino.PieceSymbol() == 'j') spriteBatch.Draw(JBlockTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
                if (currentTetromino.PieceSymbol() == 'l') spriteBatch.Draw(LBlockTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
                                                
            }
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(themeRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            spriteBatch.Draw(BackgroundTex, new Rectangle(0, 0, 1920, 1080), Color.White);
            spriteBatch.Draw(GridTex, new Rectangle(740, 100, 440, 880), Color.White);
            spriteBatch.Draw(boardRenderer, new Rectangle(740, 100, 440, 880), Color.White);
            spriteBatch.Draw(nextRenderer, new Rectangle(1220, 119, 140, 462), Color.White);
            spriteBatch.Draw(holdRenderer, new Rectangle(600, 124, 140, 140), Color.White);
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
