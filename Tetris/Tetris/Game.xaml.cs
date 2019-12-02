using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.Brushes;
using Windows.UI;
using System.Threading.Tasks;
using Windows.UI.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
  

    public sealed partial class Game : Page
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
        


        public Game()
        {
            
            this.InitializeComponent();
        }
        
        private async void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {

            BagRandomizer.Test();
            BagRandomizer.Test();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                float w = (float)AnimatedCanvas.Width;
            float h = (float)AnimatedCanvas.Height;
            float gridSize = w / 10;
            


            //Tło za gridem
            //args.DrawingSession.DrawImage(gridBackgroundImage,-700,-200);

            Rect boardOutline = new Rect(0, 0, AnimatedCanvas.Width, AnimatedCanvas.Height);
            args.DrawingSession.FillRectangle(boardOutline, backColor);

            //Grid
            for (int i = 0; i <= gridHeight; i++)
            {
                args.DrawingSession.DrawLine(0, gridSize * i, w, gridSize * i, gridColor, gridStroke);

            }
            for (int i = 0; i <= gridWidth; i++)
            {
                args.DrawingSession.DrawLine(gridSize * i, 0, gridSize * i, h, gridColor, gridStroke);
            }

            });
            DrawPlacedBlocks(sender, args, board);
        }
        

        public void DrawPlacedBlocks(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, char[,] array)
        {
                var Ibrush = new CanvasImageBrush(sender, IBlock);
                var Obrush = new CanvasImageBrush(sender, OBlock);
                var Tbrush = new CanvasImageBrush(sender, TBlock);
                var Sbrush = new CanvasImageBrush(sender, SBlock);
                var Zbrush = new CanvasImageBrush(sender, ZBlock);
                var Jbrush = new CanvasImageBrush(sender, JBlock);
                var Lbrush = new CanvasImageBrush(sender, LBlock);
                Ibrush.ExtendX = Ibrush.ExtendY = CanvasEdgeBehavior.Wrap;
                Obrush.ExtendX = Obrush.ExtendY = CanvasEdgeBehavior.Wrap;
                Tbrush.ExtendX = Tbrush.ExtendY = CanvasEdgeBehavior.Wrap;
                Sbrush.ExtendX = Sbrush.ExtendY = CanvasEdgeBehavior.Wrap;
                Zbrush.ExtendX = Zbrush.ExtendY = CanvasEdgeBehavior.Wrap;
                Jbrush.ExtendX = Jbrush.ExtendY = CanvasEdgeBehavior.Wrap;
                Lbrush.ExtendX = Lbrush.ExtendY = CanvasEdgeBehavior.Wrap;

                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {

                        Rect kloc = new Rect(j * 44, i * 44, 44, 44);

                        if (array[i + 20, j] == 'i') args.DrawingSession.FillRectangle(kloc, Ibrush);
                        if (array[i + 20, j] == 'o') args.DrawingSession.FillRectangle(kloc, Obrush);
                        if (array[i + 20, j] == 't') args.DrawingSession.FillRectangle(kloc, Tbrush);
                        if (array[i + 20, j] == 's') args.DrawingSession.FillRectangle(kloc, Sbrush);
                        if (array[i + 20, j] == 'z') args.DrawingSession.FillRectangle(kloc, Zbrush);
                        if (array[i + 20, j] == 'j') args.DrawingSession.FillRectangle(kloc, Jbrush);
                        if (array[i + 20, j] == 'l') args.DrawingSession.FillRectangle(kloc, Lbrush);
                    }
                }


        }
          

        

        private void Overlay_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(overlayImage, 0, 0);

        }



        private void Background_Draw(CanvasControl sender, CanvasDrawEventArgs args)
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
        private void Canvas_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private void Canvas_CreateResources_1(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateAnimatedResourcesAsync(sender).AsAsyncAction());
        }
        private void Background_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }
        private async Task CreateResourcesAsync(CanvasControl sender) //TU LADOWAC ZASOBY
        {
            backgroundImage = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/BCG.jpg"));
            overlayImage = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/overlay.png"));


            //gridBackgroundImage = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/GBGC.jpg"));
        }
        private async Task CreateAnimatedResourcesAsync(CanvasAnimatedControl sender) //TU LADOWAC ZASOBY
        {

            IBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/I.png"));
            OBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/O.png"));
            TBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/T.png"));
            SBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/S.png"));
            ZBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/Z.png"));
            JBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/J.png"));
            LBlock = await CanvasBitmap.LoadAsync(sender, new Uri($"ms-appx:///Assets/Themes/{theme}/L.png"));


        }


        private void Overlay_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }






    }
}
