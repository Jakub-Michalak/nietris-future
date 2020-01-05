
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;

namespace nieTRIS_future
{
    public class Game1 : Game
{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont neuro;

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
        Texture2D GhostTex;
        Song BGM;

        public double timepassed;
        public double timer;

        List<Tetromino> nextTetromino = BagRandomizer.GetNewBag();
        Tetromino currentTetromino;
        Tetromino heldTetromino;
        Tetromino tempTetromino;

        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;

        Color halfOpacityWhite = Color.White * 0.5f;

        int level = 1;
        int score = 0;
        int linesCleared = 0;
        int singleCleared = 0;
        int doublesCleared = 0;
        int tripleCleared = 0;
        int tetrisCleared = 0;
        int linesClearedSimultaneously;
        int dropDistance = 0;

        int countToFour = 0;

        bool isBlockPlaced = false;
        bool blockHeld = false;


        static List<Vector2> CurrentBlockPositions;
        static List<Vector2> CurrentGhostPositions;

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
            

            themeRenderer = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            boardRenderer = new RenderTarget2D(GraphicsDevice, 440, 880);
            nextRenderer = new RenderTarget2D(GraphicsDevice, 264, 880 );
            holdRenderer = new RenderTarget2D(GraphicsDevice, 264, 264);


            IBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/I");
            JBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/J");
            LBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/L");
            OBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/O");
            SBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/S");
            TBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/T");
            ZBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/Z");
            GhostTex = Content.Load<Texture2D> ($@"Themes/{theme}/Ghost");
            BackgroundTex = Content.Load<Texture2D>($@"Themes/{theme}/BCG");
            OverlayTex = Content.Load<Texture2D>($@"Themes/{theme}/Overlay");
            GridTex = Content.Load<Texture2D>($@"Themes/{theme}/Grid");
            BGM = Content.Load<Song>($"Audio/{audioPack}/BGM");
            neuro = Content.Load<SpriteFont>("Fonts/font");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BGM);

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
            GhostTex.Dispose();
            BackgroundTex.Dispose();
            OverlayTex.Dispose();
            GridTex.Dispose();
            BGM.Dispose();
        }

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
                dropDistance = (int)CurrentGhostPositions[1].Y - (int)CurrentBlockPositions[1].Y;
                CurrentBlockPositions = new List<Vector2>(CurrentGhostPositions);

                foreach (Vector2 v in CurrentBlockPositions)
                {
                    board[(int)v.X, 20 + (int)v.Y] = currentTetromino.PieceSymbol();
                }

                AddScore("hardDrop",dropDistance);
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

            ghostBlock();

            lastKeyboardState = currentKeyboardState;

            base.Update(gameTime);
        }

        public void ghostBlock()
        {
            CurrentGhostPositions = new List<Vector2>(CurrentBlockPositions);
            while (CanMove(Directions.Down, CurrentGhostPositions) == true)
            {
                for (int i = 0; i < CurrentGhostPositions.Count; i++)
                {
                    CurrentGhostPositions[i] = new Vector2(CurrentGhostPositions[i].X, CurrentGhostPositions[i].Y + 1);
                }
            }

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
                    linesCleared++;
                }
            }
            if (linesClearedSimultaneously == 1) { singleCleared++; AddScore("single"); }
            if (linesClearedSimultaneously == 2) { doublesCleared++; AddScore("double"); }
            if (linesClearedSimultaneously == 3) { tripleCleared++; AddScore("triple"); }
            if (linesClearedSimultaneously == 4) { tetrisCleared++; AddScore("tetris"); }
        }

        protected void AddScore(string source, int dropDistance = 0)
        {
            if (source == "single") score = score + 100 * level;
            else if (source == "double") score = score + 300 * level;
            else if (source == "triple") score = score + 500 * level;
            else if (source == "tetris") score = score + 800 * level;
            else if (source == "hardDrop") score = score + 2 * dropDistance;

        }

        public int MaxPositionX(List<Vector2> list)
        {
            int max = 0;
            foreach(Vector2 v in list)
            {
                if (v.X > max) max = (int)v.X;
            }
            return max;
        }

        public int MinPositionX(List<Vector2> list)
        {
            int min = 10;
            foreach (Vector2 v in list)
            {
                if (v.X < min) min = (int)v.X;
            }
            return min;
        }

        public int MaxPositionY(List<Vector2> list)
        {
            int max = 0;
            foreach (Vector2 v in list)
            {
                if (v.Y > max) max = (int)v.Y;
            }
            return max;
        }

        public bool CanMove(Directions direction, List<Vector2> list)
        {
            switch (direction)
            {
                case Directions.Left:
                    countToFour = 0;
                    if (MinPositionX(list) - 1 >= 0)
                    {
                        foreach (Vector2 v in list)
                        {
                            if (board[(int)v.X - 1, 20 + (int)v.Y] == null)
                            {
                                countToFour++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    if (countToFour == 4) return true;
                    break;
                case Directions.Right:
                    countToFour = 0;
                    if (MaxPositionX(list) + 1 < 10)
                    {
                        foreach (Vector2 v in list)
                        {
                            if (board[(int)v.X + 1, 20 + (int)v.Y] == null)
                            {
                                countToFour++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    if (countToFour == 4) return true;
                    break;
                case Directions.Down:
                    countToFour = 0;
                    if (MaxPositionY(list) + 21 < 40)
                    {
                        foreach (Vector2 v in list)
                        {
                            if (board[(int)v.X, 21 + (int)v.Y] == null)
                            {
                                countToFour++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    if (countToFour == 4) return true;
                    break;
            }
            return false;
        }

        public void Move(Directions direction)
        {

            switch (direction)
            {
                case Directions.Left:
                    if (CanMove(Directions.Left, CurrentBlockPositions) == true)
                    {
                        for (int i = 0; i < CurrentBlockPositions.Count; i++)
                        {
                            CurrentBlockPositions[i] = new Vector2(CurrentBlockPositions[i].X - 1, CurrentBlockPositions[i].Y);
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
                case Directions.Right:
                    if (CanMove(Directions.Right, CurrentBlockPositions) == true)
                    {
                        for (int i = 0; i < CurrentBlockPositions.Count; i++)
                        {
                            CurrentBlockPositions[i] = new Vector2(CurrentBlockPositions[i].X + 1, CurrentBlockPositions[i].Y);
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
                case Directions.Down:
                   
                    if (CanMove(Directions.Down, CurrentBlockPositions) == true)
                    {
                        for (int i = 0; i < CurrentBlockPositions.Count; i++)
                        {
                            CurrentBlockPositions[i] = new Vector2(CurrentBlockPositions[i].X, CurrentBlockPositions[i].Y + 1);
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
            }
        }

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
            foreach (Vector2 v in CurrentGhostPositions)
            {
                if (currentTetromino.PieceSymbol() != null) spriteBatch.Draw(GhostTex, new Rectangle((int)v.X * 44, (int)v.Y * 44, 44, 44), Color.White);
            }
            foreach (Vector2 v in CurrentBlockPositions)
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
            spriteBatch.Draw(GridTex, new Rectangle(480, 0, 960, 1080), Color.White);
            spriteBatch.Draw(boardRenderer, new Rectangle(740, 100, 440, 880), Color.White);
            spriteBatch.Draw(nextRenderer, new Rectangle(1220, 119, 140, 462), Color.White);
            spriteBatch.Draw(holdRenderer, new Rectangle(600, 124, 140, 140), Color.White);
            spriteBatch.Draw(OverlayTex, new Rectangle(480, 0, 960, 1080), Color.White);

            spriteBatch.DrawString(neuro, $"SCORE:", new Vector2(1220,650),Color.White );
            spriteBatch.DrawString(neuro, $"{score}", new Vector2(1220, 700), Color.White);
            spriteBatch.DrawString(neuro, $"CLEARED LINES:", new Vector2(1220, 775), Color.White);
            spriteBatch.DrawString(neuro, $"{linesCleared}", new Vector2(1220, 825), Color.White);


            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);



            spriteBatch.Begin();

            spriteBatch.Draw(themeRenderer, GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
