using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameLoop : Page
    {
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
        };



        //Kolory i motyw
        string theme = PlayPage.P1.GetTheme(); //Dostępny też "Test"

        Color gridColor = ColorHelper.FromArgb(178, 118, 125, 146); //slategray
        Color backColor = ColorHelper.FromArgb(178, 0, 0, 0);
        const float gridStroke = 2;
        const float backgroundBlur = 5.0f;

        

        const int gridWidth = 10;
        const int gridHeight = 20;
        Vector2 gridSize = new Vector2(gridWidth, gridHeight);

        public CanvasBitmap backgroundImage;
        public CanvasBitmap gridBackgroundImage;
        public CanvasBitmap overlayImage;

        public CanvasBitmap IBlock;
        public CanvasBitmap OBlock;
        public CanvasBitmap TBlock;
        public CanvasBitmap SBlock;
        public CanvasBitmap ZBlock;
        public CanvasBitmap JBlock;
        public CanvasBitmap LBlock;

        public GameLoop()
        {
            this.InitializeComponent();
            
        }


        private void GameCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {

            using (var canvasActiveLayer = args.DrawingSession.CreateLayer(1.0f))
            {
                CanvasCommandList cl = new CanvasCommandList(sender);
                using (CanvasDrawingSession clds = cl.CreateDrawingSession())
                {
                    clds.DrawImage(backgroundImage, 0, 0);
                }
                GaussianBlurEffect blur = new GaussianBlurEffect();
                blur.Source = cl;
                blur.BlurAmount = backgroundBlur;
                args.DrawingSession.DrawImage(blur);
            }
            using (var canvasActiveLayer = args.DrawingSession.CreateLayer(0.5f))
            {
                args.DrawingSession.FillRectangle(740, 100, 440, 880, backColor);


                for (int i = 0; i <= gridHeight; i++)
                {
                    args.DrawingSession.DrawLine(740, 100 + 44 * i, 1180, 100 + 44 * i, gridColor, gridStroke);

                }
                for (int i = 0; i <= gridWidth; i++)
                {
                    args.DrawingSession.DrawLine(740 + 44 * i, 100, 740 + 44 * i, 980, gridColor, gridStroke);
                }
            }
            using (var canvasActiveLayer = args.DrawingSession.CreateLayer(1.0f))
            {
                DrawPlacedBlocks(sender, args, board);
            }
            using (var canvasActiveLayer = args.DrawingSession.CreateLayer(1.0f))
            {
                
            }
            using (var canvasActiveLayer = args.DrawingSession.CreateLayer(1.0f))
            {
                args.DrawingSession.DrawImage(overlayImage, 480, 0);
            }

        }
        public void DrawPlacedBlocks(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, char[,] array)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    Rect kloc = new Rect(740 + j * 44, 100 + i * 44, 44, 44);

                    if (array[i + 20, j] == 'i') args.DrawingSession.DrawImage(IBlock, kloc);
                    if (array[i + 20, j] == 'o') args.DrawingSession.DrawImage(OBlock, kloc);
                    if (array[i + 20, j] == 't') args.DrawingSession.DrawImage(TBlock, kloc);
                    if (array[i + 20, j] == 's') args.DrawingSession.DrawImage(SBlock, kloc);
                    if (array[i + 20, j] == 'z') args.DrawingSession.DrawImage(ZBlock, kloc);
                    if (array[i + 20, j] == 'j') args.DrawingSession.DrawImage(JBlock, kloc);
                    if (array[i + 20, j] == 'l') args.DrawingSession.DrawImage(LBlock, kloc);
                }
            }


        }

        public void DrawFallingBlocks(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, char[,] array)
        {

        }

        private void GameCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            backgroundImage = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/BCG.jpg"));
            overlayImage = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/overlay.png"));

            IBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/I.png"));
            OBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/O.png"));
            TBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/T.png"));
            SBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/S.png"));
            ZBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/Z.png"));
            JBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/J.png"));
            LBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/L.png"));

        }

        private void GameCanvas_GameLoopStarting(ICanvasAnimatedControl sender, object args)
        {

        }

        private void GameCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {

        }
    }
}
