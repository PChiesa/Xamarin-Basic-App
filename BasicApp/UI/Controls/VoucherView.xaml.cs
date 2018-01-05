using System;
using System.Collections.Generic;
using Autofac;
using BasicApp.UI.PubSubEvents;
using Prism.Autofac;
using Prism.Events;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace BasicApp.UI.Controls
{
    public partial class VoucherView : ContentView
    {
        const float START_ANGLE = -90f;
        const float CANVAS_SIZE = 20f;
        private float _endAngle;
        private Animation _circleProgress;
        private bool _repeatAnimation = true;
        private readonly IEventAggregator _eventAggregator;
        private Voucher.Models.Voucher _voucher;

        public VoucherView()
        {
            InitializeComponent();
            _eventAggregator = (Application.Current as PrismApplication).Container.Resolve<IEventAggregator>();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext is Voucher.Models.Voucher)
            {
                _voucher = this.BindingContext as Voucher.Models.Voucher;
                
                //if (!_voucher.IsVoucherActive)
                //    return;

                _eventAggregator
                        .GetEvent<QrCodeRefreshEvent>()
                    .Publish(new VoucherEventArgs(_voucher));

                BeginAnimation();
            }
        }

        private void BeginAnimation()
        {
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            _circleProgress = new Animation((d) =>
                {
                    _endAngle = (float)(d * 360);
                    canvasView.InvalidateSurface();
                }, 0, 1);

            _circleProgress
                .Commit(
                    this,
                    "cicleProgress",
                    8,
                    30000,
                    Easing.Linear,
                    repeat: () => _repeatAnimation,
                    finished: (d, b) => _eventAggregator
                        .GetEvent<QrCodeRefreshEvent>()
                        .Publish(new VoucherEventArgs(_voucher)));
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
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


            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKRect rect = new SKRect(CANVAS_SIZE, (info.Height - (info.Width - CANVAS_SIZE)) / 2, info.Width - CANVAS_SIZE, info.Width - CANVAS_SIZE + ((info.Height - (info.Width - CANVAS_SIZE)) / 2));

            canvas.DrawOval(rect, outlinePaint);

            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, START_ANGLE, _endAngle);
                SKPaint arcPaint;
                if (_endAngle <= 180)
                    arcPaint = arcPaintGreen;
                else if (_endAngle >= 315)
                    arcPaint = arcPaintRed;
                else
                    arcPaint = arcPaintYellow;

                canvas.DrawPath(path, arcPaint);
            }
        }
    }
}
