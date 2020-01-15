
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

namespace nieTRIS_future
{
    public class Game1 : Game
    {
        Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
        double ARR = 50;
        double DAS = 300;

        double GameTotal = 0;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public SpriteFont neuro;

        RenderTarget2D themeRenderer;
        RenderTarget2D boardRenderer;
        RenderTarget2D nextRenderer;
        RenderTarget2D holdRenderer;
        RenderTarget2D pauseRenderer;

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
        Texture2D GameOverScreen;
        public Texture2D WinScreen;

        Song BGM;
        SoundEffect RotateSFX;
        SoundEffect HarddropSFX;
        SoundEffect LineclearSFX;
        SoundEffect TetrisSFX;

        public double timepassed;
        public double timer;
        public double lockTimer;
        public double ARRtimer;
        public double DAStimer;

        List<Tetromino> nextTetromino = BagRandomizer.GetNewBag();
        Tetromino currentTetromino;
        Tetromino heldTetromino;
        Tetromino tempTetromino;

        Gamemode currentGamemode;

        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;

        private GamePadState currentGamepadState;
        private GamePadState lastGamepadState;

        Color halfOpacityWhite = Color.White * 0.5f;

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
        bool blockCanMoveDown = true;

        static List<Vector2> CurrentBlockPositions;
        static List<Vector2> CurrentGhostPositions;

        Tetromino.rotations currentRotation;

        public enum Directions
        {
            Down,
            Left,
            Right
        }

        public enum GameStates
        {
            Game,
            Pause,
            End,
            Win
        }

        public GameStates currentGameState = GameStates.Game;

        string theme = MainPage.P1.GetTheme();
        string audioPack = MainPage.P1.GetAudioPack();
        int level = MainPage.P1.GetStartingLevel();

        char?[,] board = new char?[10, 40];

        private double DAStimerLeft;

        private double DAStimerRight;

        private bool DASclickedRight = true;

        private bool DASclickedLeft = true;
        private double TempGameTotal;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 144.0f);

            if (App.IsXbox())
            {
                graphics.HardwareModeSwitch = true;
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.ApplyChanges();
            }

            Reset();

            base.Initialize();
        }

        public void Load()
        {
            theme = MainPage.P1.GetTheme();
            audioPack = MainPage.P1.GetAudioPack();

            currentGamemode = MainPage.P1.GetGamemode();


            spriteBatch = new SpriteBatch(GraphicsDevice);


            themeRenderer = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            boardRenderer = new RenderTarget2D(GraphicsDevice, 440, 880);
            nextRenderer = new RenderTarget2D(GraphicsDevice, 264, 880);
            holdRenderer = new RenderTarget2D(GraphicsDevice, 264, 264);
            pauseRenderer = new RenderTarget2D(GraphicsDevice, 1920, 1080);


            IBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/I");
            JBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/J");
            LBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/L");
            OBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/O");
            SBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/S");
            TBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/T");
            ZBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/Z");
            GhostTex = Content.Load<Texture2D>($@"Themes/{theme}/Ghost");
            BackgroundTex = Content.Load<Texture2D>($@"Themes/{theme}/BCG");
            OverlayTex = Content.Load<Texture2D>($@"Themes/{theme}/Overlay");
            GridTex = Content.Load<Texture2D>($@"Themes/{theme}/Grid");
            GameOverScreen = Content.Load<Texture2D>($@"Themes/GAMEOVER");
            WinScreen = Content.Load<Texture2D>($@"Themes/WIN");


            BGM = Content.Load<Song>($"Audio/{audioPack}/BGM");
            RotateSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/rotate");
            HarddropSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/harddrop");
            LineclearSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/lineclear");
            TetrisSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/tetris");

            neuro = Content.Load<SpriteFont>("Fonts/font");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = (float)MainPage.musicVolume;
            SoundEffect.MasterVolume = (float)MainPage.sfxVolume;
            MediaPlayer.Play(BGM);

            GameTotal = 0;


        }

        public void Reset()
        {
            nextTetromino.Clear();
            currentTetromino = null;
            heldTetromino = null;

            nextTetromino = BagRandomizer.GetNewBag();

            level = MainPage.P1.GetStartingLevel();
            score = 0;
            linesCleared = 0;
            singleCleared = 0;
            doublesCleared = 0;
            tripleCleared = 0;
            tetrisCleared = 0;

            Array.Clear(board, 0, 400);

            isBlockPlaced = false;
            blockHeld = false;
            blockCanMoveDown = true;

            currentTetromino = nextTetromino[0];
            nextTetromino.RemoveAt(0);
            CurrentBlockPositions = currentTetromino.StartingPosition();

            TempGameTotal = 0;

            



        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            themeRenderer = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            boardRenderer = new RenderTarget2D(GraphicsDevice, 440, 880);
            nextRenderer = new RenderTarget2D(GraphicsDevice, 264, 880);
            holdRenderer = new RenderTarget2D(GraphicsDevice, 264, 264);
            pauseRenderer = new RenderTarget2D(GraphicsDevice, 1920, 1080);


            IBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/I");
            JBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/J");
            LBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/L");
            OBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/O");
            SBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/S");
            TBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/T");
            ZBlockTex = Content.Load<Texture2D>($@"Themes/{theme}/Z");
            GhostTex = Content.Load<Texture2D>($@"Themes/{theme}/Ghost");
            BackgroundTex = Content.Load<Texture2D>($@"Themes/{theme}/BCG");
            OverlayTex = Content.Load<Texture2D>($@"Themes/{theme}/Overlay");
            GridTex = Content.Load<Texture2D>($@"Themes/{theme}/Grid");

            BGM = Content.Load<Song>($"Audio/{audioPack}/BGM");
            RotateSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/rotate");
            HarddropSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/harddrop");
            LineclearSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/lineclear");
            TetrisSFX = Content.Load<SoundEffect>($"Audio/{audioPack}/tetris");

            neuro = Content.Load<SpriteFont>("Fonts/font");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = (float)MainPage.musicVolume;
            SoundEffect.MasterVolume = (float)MainPage.sfxVolume;
            MediaPlayer.Play(BGM);

        }

        public void MiuzikuStoppo()
        {
            MediaPlayer.Stop();
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
            RotateSFX.Dispose();
            HarddropSFX.Dispose();
            LineclearSFX.Dispose();
            TetrisSFX.Dispose();

            
        }

        protected override void Update(GameTime gameTime)
        {

            timepassed = gameTime.ElapsedGameTime.TotalSeconds;
            currentKeyboardState = Keyboard.GetState();
            currentGamepadState = GamePad.GetState(PlayerIndex.One);

            switch (currentGameState)
            {
                case GameStates.Game:
                    GameUpdate(gameTime);
                    break;
                case GameStates.Pause:
                    PauseUpdate();
                    break;
                case GameStates.End:
                    EndUpdate();
                    break;
                default:
                    break;
            }
            lastKeyboardState = currentKeyboardState;
            lastGamepadState = currentGamepadState;
            base.Update(gameTime);
        }

        private void GameUpdate(GameTime gameTime)
        {
            ///MISC
            //PAUSE
            if (currentGamepadState.IsButtonDown(Buttons.Start) && lastGamepadState.IsButtonUp(Buttons.Start) || currentKeyboardState.IsKeyDown(Keys.P) && lastKeyboardState.IsKeyUp(Keys.P))
            {
                currentGameState = GameStates.Pause;
            }

            //RESETING
            if (isBlockPlaced)
            {
                currentTetromino = nextTetromino[0];
                CurrentBlockPositions = currentTetromino.StartingPosition();
                nextTetromino.RemoveAt(0);
                currentRotation = Tetromino.rotations.rotation1;
                blockCanMoveDown = true;
                isBlockPlaced = false;
                blockHeld = false;
            }

            timer += timepassed;


            ///GENERATION PHASE
            //NEW BAG
            if (nextTetromino.Count < 7) nextTetromino.AddRange(BagRandomizer.GetNewBag());


            ///FALLING PHASE
            //AUTO MOVE DOWN
            if (timer >= Math.Pow((0.8 - ((level - 1) * 0.007)), (level - 1)))
            {
                Move(Directions.Down);
                timer = 0;
            }

            //HARD DROP?
            if (currentGamepadState.IsButtonDown(Buttons.DPadUp) && lastGamepadState.IsButtonUp(Buttons.DPadUp) || currentKeyboardState.IsKeyDown(Keys.Up) && lastKeyboardState.IsKeyUp(Keys.Up))
            {
                dropDistance = (int)CurrentGhostPositions[1].Y - (int)CurrentBlockPositions[1].Y;
                CurrentBlockPositions = new List<Vector2>(CurrentGhostPositions);
                PlaceBlock();
                HarddropSFX.CreateInstance().Play();
                AddScore("hardDrop", dropDistance);
                isBlockPlaced = true;
            }


            ///LOCK PHASE
            //AUTO LOCK
            if (blockCanMoveDown == false)
            {
                lockTimer += timepassed;
                if (lockTimer >= 0.5)
                {
                    PlaceBlock();
                    isBlockPlaced = true;
                    lockTimer = 0;
                }
            }

            //MOVING LEFT
            if (DASclickedLeft && !(currentGamepadState.IsButtonDown(Buttons.DPadLeft) || currentKeyboardState.IsKeyDown(Keys.Left)))
            {
                Debug.WriteLine("czyszczenie DASclickedLeft");
                DASclickedLeft = false;

            }

            if (currentGamepadState.IsButtonDown(Buttons.DPadLeft) || currentKeyboardState.IsKeyDown(Keys.Left))
            {
                if (DASclickedLeft == false)
                {
                    Debug.WriteLine("pierwszy klik");
                    DAStimerLeft = gameTime.TotalGameTime.TotalMilliseconds;

                    Move(Directions.Left);
                    DASclickedLeft = true;
                }
                if (gameTime.TotalGameTime.TotalMilliseconds > DAStimerLeft + DAS)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds - ARRtimer > ARR)
                    {
                        Debug.WriteLine("przesowanie ARR");
                        lockTimer = 0;
                        Move(Directions.Left);
                        ARRtimer = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
            }

            //MOVING RIGHT
            if (DASclickedRight && !(currentGamepadState.IsButtonDown(Buttons.DPadRight) || currentKeyboardState.IsKeyDown(Keys.Right)))
            {
                Debug.WriteLine("czyszczenie DASclickedRight");
                DASclickedRight = false;

            }

            if (currentGamepadState.IsButtonDown(Buttons.DPadRight) || currentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (DASclickedRight == false)
                {
                    Debug.WriteLine("pierwszy klik");
                    DAStimerRight = gameTime.TotalGameTime.TotalMilliseconds;

                    Move(Directions.Right);
                    DASclickedRight = true;
                }
                if (gameTime.TotalGameTime.TotalMilliseconds > DAStimerRight + DAS)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds - ARRtimer > ARR)
                    {
                        Debug.WriteLine("przesowanie ARR");
                        lockTimer = 0;
                        Move(Directions.Right);
                        ARRtimer = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
            }

            //SOFT DROP
            if (currentGamepadState.IsButtonDown(Buttons.DPadDown) || currentKeyboardState.IsKeyDown(Keys.Down))
            {
                if (timer >= Math.Pow((0.8 - ((level - 1) * 0.007)), level - 1) / 20)
                {
                    lockTimer = 0;
                    timer = 0;
                    Move(Directions.Down);

                }
            }


            //ROTATE
            if (currentGamepadState.IsButtonDown(Buttons.B) && lastGamepadState.IsButtonUp(Buttons.B) || currentKeyboardState.IsKeyDown(Keys.X) && lastKeyboardState.IsKeyUp(Keys.X))
            {
                lockTimer = 0;
                RotateSFX.CreateInstance().Play();
                CurrentBlockPositions = currentTetromino.Rotate(CurrentBlockPositions, currentRotation, Tetromino.rotationDirection.clockwise, ref board);
                if (currentRotation == Tetromino.rotations.rotation1) currentRotation = Tetromino.rotations.rotation2;
                else if (currentRotation == Tetromino.rotations.rotation2) currentRotation = Tetromino.rotations.rotation3;
                else if (currentRotation == Tetromino.rotations.rotation3) currentRotation = Tetromino.rotations.rotation4;
                else if (currentRotation == Tetromino.rotations.rotation4) currentRotation = Tetromino.rotations.rotation1;
            }


            if (currentGamepadState.IsButtonDown(Buttons.A) && lastGamepadState.IsButtonUp(Buttons.A) || currentKeyboardState.IsKeyDown(Keys.Z) && lastKeyboardState.IsKeyUp(Keys.Z))
            {
                lockTimer = 0;
                RotateSFX.CreateInstance().Play();
                CurrentBlockPositions = currentTetromino.Rotate(CurrentBlockPositions, currentRotation, Tetromino.rotationDirection.counterclockwise, ref board);
                if (currentRotation == Tetromino.rotations.rotation1) currentRotation = Tetromino.rotations.rotation4;
                else if (currentRotation == Tetromino.rotations.rotation2) currentRotation = Tetromino.rotations.rotation1;
                else if (currentRotation == Tetromino.rotations.rotation3) currentRotation = Tetromino.rotations.rotation2;
                else if (currentRotation == Tetromino.rotations.rotation4) currentRotation = Tetromino.rotations.rotation3;
            }

            //HOLD
            if ((currentGamepadState.IsButtonDown(Buttons.LeftShoulder) && lastGamepadState.IsButtonUp(Buttons.LeftShoulder) || currentKeyboardState.IsKeyDown(Keys.LeftShift) && lastKeyboardState.IsKeyUp(Keys.LeftShift)) && blockHeld == false)
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

            ///ELIMINATE PHASE
            DeleteLines();

            //CHECK LEVEL UP
            if (linesCleared >= level * 10 + 10)
            {
                level++;
            }

            ///GHOST BLOCK
            ghostBlock();

            TempGameTotal += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (currentGamemode.CheckWinCondition(linesCleared))
            {

                GameTotal = TempGameTotal;
                currentGameState = GameStates.Win;
                
                SubmitStats(true);
            }


        }

        private void PauseUpdate()
        {
            if(currentGamepadState.IsButtonDown(Buttons.Start) && lastGamepadState.IsButtonUp(Buttons.Start) || currentKeyboardState.IsKeyDown(Keys.P) && lastKeyboardState.IsKeyUp(Keys.P))
            {
                currentGameState = GameStates.Game;
            }

        }

        private void EndUpdate()
        {

        }

        private void WinUpdate()
        {

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
            if (linesClearedSimultaneously == 1) { singleCleared++; AddScore("single"); LineclearSFX.CreateInstance().Play(); }
            if (linesClearedSimultaneously == 2) { doublesCleared++; AddScore("double"); LineclearSFX.CreateInstance().Play(); }
            if (linesClearedSimultaneously == 3) { tripleCleared++; AddScore("triple"); LineclearSFX.CreateInstance().Play(); }
            if (linesClearedSimultaneously == 4) { tetrisCleared++; AddScore("tetris"); TetrisSFX.CreateInstance().Play(); }
        }

        protected void AddScore(string source, int dropDistance = 0)
        {
            if (source == "single") score = score + 100 * level;
            else if (source == "double") score = score + 300 * level;
            else if (source == "triple") score = score + 500 * level;
            else if (source == "tetris") score = score + 800 * level;
            else if (source == "hardDrop") score = score + 2 * dropDistance;

        }

        protected void SubmitStats(bool win)
        {
            if(currentGamemode.getName() == "Endless")MainPage.P1.endlessSubmitScore(score);
            if(currentGamemode.getName() == "Sprint" && win)MainPage.P1.sprintSubmitTime(GameTotal);
            if(currentGamemode.getName() == "Marathon" && win) MainPage.P1.marathonSubmitScore(score);
            MainPage.P1.addLineClears(linesCleared);
            MainPage.P1.addTetrisClears(tetrisCleared);

            roamingSettings.Values["endlessHighScore"] = MainPage.P1.getEndlessScore();
            roamingSettings.Values["sprintBestTime"] = MainPage.P1.getSprintTime();
            roamingSettings.Values["marathonHighScore"] = MainPage.P1.getMarathonScore();
            roamingSettings.Values["linesCleared"] = MainPage.P1.getLineClears();
            roamingSettings.Values["tetrisCleared"] = MainPage.P1.getTetrisClears();
        }

        public int MaxPositionX(List<Vector2> list)
        {
            int max = 0;
            foreach (Vector2 v in list)
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

        public void PlaceBlock()
        {
            foreach (Vector2 v in CurrentBlockPositions)
            {
                if ((int)v.Y < 0 && !CanMove(Directions.Down,CurrentBlockPositions))
                {
                    currentGameState = GameStates.End;
                    SubmitStats(false); 
                    break;
                }
                else board[(int)v.X, 20 + (int)v.Y] = currentTetromino.PieceSymbol();
            }
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
                            lockTimer = 0;
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
                            lockTimer = 0;
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
                            lockTimer = 0;
                        }
                    }
                    else
                    {
                        blockCanMoveDown = false;
                        break;
                    }
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {

            switch (currentGameState)
            {
                case GameStates.Game:
                    GameDraw();
                    break;
                case GameStates.Pause:
                    PauseDraw();
                    break;
                case GameStates.End:
                    EndedDraw();
                    break;
                case GameStates.Win:
                    WinDraw();
                    break;
                default:
                    break;
            }



            base.Draw(gameTime);
        }

        private void GameDraw()
        {

            GraphicsDevice.SetRenderTarget(nextRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            int starting = 1;
            for (int i = 0; i < 5; i++)
            {

                foreach (Vector2 v in nextTetromino[i].StartingPosition())
                {
                    Rectangle kloc = new Rectangle(((int)v.X - 3) * 44, ((int)v.Y + 2) * 44 + (44 * starting), 44, 44);

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


                    if (board[j, i + 20] == 'i') spriteBatch.Draw(IBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'o') spriteBatch.Draw(OBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 't') spriteBatch.Draw(TBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 's') spriteBatch.Draw(SBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'z') spriteBatch.Draw(ZBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'j') spriteBatch.Draw(JBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'l') spriteBatch.Draw(LBlockTex, kloc, Color.White);
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

            spriteBatch.DrawString(neuro, $"SCORE:", new Vector2(1220, 650), Color.White);
            spriteBatch.DrawString(neuro, $"{score}", new Vector2(1220, 700), Color.White);
            spriteBatch.DrawString(neuro, $"CLEARED LINES:", new Vector2(1220, 775), Color.White);
            spriteBatch.DrawString(neuro, $"{linesCleared}", new Vector2(1220, 825), Color.White);
            spriteBatch.DrawString(neuro, $"LEVEL:", new Vector2(1220, 900), Color.White);
            spriteBatch.DrawString(neuro, $"{level}", new Vector2(1220, 950), Color.White);



            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);



            spriteBatch.Begin();

            spriteBatch.Draw(themeRenderer, GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();

        }

        private void PauseDraw()
        {
            GraphicsDevice.SetRenderTarget(nextRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            int starting = 1;
            for (int i = 0; i < 5; i++)
            {

                foreach (Vector2 v in nextTetromino[i].StartingPosition())
                {
                    Rectangle kloc = new Rectangle(((int)v.X - 3) * 44, ((int)v.Y + 2) * 44 + (44 * starting), 44, 44);

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


                    if (board[j, i + 20] == 'i') spriteBatch.Draw(IBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'o') spriteBatch.Draw(OBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 't') spriteBatch.Draw(TBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 's') spriteBatch.Draw(SBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'z') spriteBatch.Draw(ZBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'j') spriteBatch.Draw(JBlockTex, kloc, Color.White);
                    if (board[j, i + 20] == 'l') spriteBatch.Draw(LBlockTex, kloc, Color.White);
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

            GraphicsDevice.SetRenderTarget(pauseRenderer);
            GraphicsDevice.Clear(Color.Black * 0.5f);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            spriteBatch.DrawString(neuro, "GAME PAUSED", new Vector2((1920 - neuro.MeasureString("GAME PAUSED").X) / 2, (1080 - neuro.MeasureString("GAME PAUSED").Y) / 2), Color.White);


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

            spriteBatch.DrawString(neuro, $"SCORE:", new Vector2(1220, 650), Color.White);
            spriteBatch.DrawString(neuro, $"{score}", new Vector2(1220, 700), Color.White);
            spriteBatch.DrawString(neuro, $"CLEARED LINES:", new Vector2(1220, 775), Color.White);
            spriteBatch.DrawString(neuro, $"{linesCleared}", new Vector2(1220, 825), Color.White);
            spriteBatch.DrawString(neuro, $"LEVEL:", new Vector2(1220, 900), Color.White);
            spriteBatch.DrawString(neuro, $"{level}", new Vector2(1220, 950), Color.White);

            spriteBatch.Draw(pauseRenderer, new Rectangle(0, 0, 1920, 1080), Color.White);





            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);



            spriteBatch.Begin();

            spriteBatch.Draw(themeRenderer, GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();


        }

        private void EndedDraw()
        {
            GraphicsDevice.SetRenderTarget(themeRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            spriteBatch.Draw(GameOverScreen, new Rectangle(0,0,1920, 1080), Color.White);

            spriteBatch.End();


            GraphicsDevice.SetRenderTarget(null);


            spriteBatch.Begin();

            spriteBatch.Draw(themeRenderer, GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();
        }

        private void WinDraw()
        {
            GraphicsDevice.SetRenderTarget(themeRenderer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            spriteBatch.Draw(WinScreen, new Rectangle(0, 0, 1920, 1080), Color.White);

            spriteBatch.End();

            


            GraphicsDevice.SetRenderTarget(null);


            spriteBatch.Begin();

            spriteBatch.Draw(themeRenderer, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(neuro, "SCORE", new Vector2((1920 - neuro.MeasureString("SCORE").X) / 2, 400), Color.White);
            spriteBatch.DrawString(neuro, $"{score}", new Vector2((1920 - neuro.MeasureString($"{score}").X) / 2, 465 ), Color.White);
            spriteBatch.DrawString(neuro, "LINES CLEARED", new Vector2((1920 - neuro.MeasureString("LINES CLEARED").X) / 2, 530) , Color.White);
            spriteBatch.DrawString(neuro, $"{linesCleared}", new Vector2((1920 - neuro.MeasureString($"{linesCleared}").X) / 2, 595), Color.White);
            spriteBatch.DrawString(neuro, "TETRIS CLEARED", new Vector2((1920 - neuro.MeasureString("TETRIS CLEARED").X) / 2,660), Color.White);
            spriteBatch.DrawString(neuro, $"{tetrisCleared}", new Vector2((1920 - neuro.MeasureString($"{tetrisCleared}").X) / 2,725), Color.White);
            spriteBatch.DrawString(neuro, "TIME", new Vector2((1920 - neuro.MeasureString("TIME").X) / 2, 790), Color.White);
            spriteBatch.DrawString(neuro, $"{TimeSpan.FromMilliseconds(GameTotal).Minutes}:{TimeSpan.FromMilliseconds(GameTotal).Seconds}:{TimeSpan.FromMilliseconds(GameTotal).Milliseconds}", new Vector2((1920 - neuro.MeasureString($"{TimeSpan.FromMilliseconds(GameTotal).Minutes}:{TimeSpan.FromMilliseconds(GameTotal).Seconds}:{TimeSpan.FromMilliseconds(GameTotal).Milliseconds}").X) / 2, 855), Color.White);

            spriteBatch.End();
        }
    }
}
