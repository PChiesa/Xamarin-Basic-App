using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace BasicApp.Voucher.Views
{
    public partial class VoucherTestPage : ContentPage
    {
        const float startAngle = -90f;
        float endAngle = 0f;

        public VoucherTestPage()
        {
            InitializeComponent();

            var animation = new Animation((d) =>
            {
                endAngle = (float)(d * 360);
                canvasView.InvalidateSurface();
            }, 0, 1);

            animation.Commit(this, "animation", 8, 30000, Easing.Linear, repeat: () => true);

        }

        SKPaint outlinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 32,
            Color = SKColors.Black
        };

        SKPaint arcPaintGreen = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 30,
            Color = SKColors.Green
        };

        SKPaint arcPaintYellow = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 30,
            Color = SKColors.Yellow
        };

        SKPaint arcPaintRed = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 30,
            Color = SKColors.Red
        };

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKRect rect = new SKRect(50, 50, info.Width - 50, info.Width - 50);

            canvas.DrawOval(rect, outlinePaint);

            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngle, endAngle);
                SKPaint arcPaint;
                if (endAngle <= 45)
                    arcPaint = arcPaintGreen;
                else if (endAngle >= 315)
                    arcPaint = arcPaintRed;
                else
                    arcPaint = arcPaintYellow;

                canvas.DrawPath(path, arcPaint);
            }

            //var v = new Models.Voucher() { Token = "abc" };
            //v.AtualizarQrCode(new System.TimeSpan());

            //var image = SKImage.FromBitmap(SKBitmap.Decode(v.QrCodeStream));
            //canvas.DrawImage(image, 50, 100);
        }
    }
}

