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
        const float STROKE_WIDTH = 15;
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
                    _endAngle = (float)(d * 100);
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
                StrokeWidth = STROKE_WIDTH + 5,
                Color = SKColors.Black
            };

            SKPaint arcPaintGreen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = STROKE_WIDTH,
                Color = new SKColor(148, 220, 77)
            };

            SKPaint arcPaintYellow = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = STROKE_WIDTH,
                Color = new SKColor(255, 241, 91)
            };

            SKPaint arcPaintRed = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = STROKE_WIDTH,
                Color = new SKColor(255, 91, 91)
            };


            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            //SKRect rect = new SKRect(CANVAS_SIZE, (info.Height - (info.Width - CANVAS_SIZE)) / 2, info.Width - CANVAS_SIZE, info.Width - CANVAS_SIZE + ((info.Height - (info.Width - CANVAS_SIZE)) / 2));

            SKRect rect = new SKRect(CANVAS_SIZE / 2, CANVAS_SIZE, info.Width - CANVAS_SIZE / 2, info.Height - CANVAS_SIZE);

            SKPaint arcPaint;

            if (_endAngle <= 75)
                arcPaint = arcPaintGreen;
            else if (_endAngle >= 95)
                arcPaint = arcPaintRed;
            else
                arcPaint = arcPaintYellow;


            //outlinePaint.StrokeCap = SKStrokeCap.Round;
            //arcPaint.StrokeCap = SKStrokeCap.Round;


            canvas.DrawLine(rect.Left, rect.Bottom, rect.Right, rect.Bottom, outlinePaint);

            rect.Left += 1;
            rect.Right -= 1;
            canvas.DrawLine(rect.Left, rect.Bottom, rect.Left + (_endAngle * rect.Width) / 100, rect.Bottom, arcPaint);

            //canvas.DrawRect(rect, outlinePaint);

            //if (_endAngle <= size)
            //{
            //    canvas.DrawLine(rect.Left, rect.Top, (rect.Right * _endAngle) / size, rect.Top, arcPaint);

            //}
            //else if (_endAngle <= size * 2 && _endAngle > size)
            //{
            //    canvas.DrawLine(rect.Left, rect.Top, rect.Right, rect.Top, arcPaint);
            //    canvas.DrawLine(rect.Right, rect.Top, rect.Right, (rect.Bottom * (_endAngle % size)) / size, arcPaint);
            //}
            //else if (_endAngle <= size * 3 && _endAngle > size * 2)
            //{
            //    canvas.DrawLine(rect.Left, rect.Top, rect.Right, rect.Top, arcPaint);
            //    canvas.DrawLine(rect.Right, rect.Top, rect.Right, rect.Bottom, arcPaint);
            //    canvas.DrawLine(rect.Right, rect.Bottom, rect.Right - (rect.Right * (_endAngle % size)) / size, rect.Bottom, arcPaint);
            //}
            //else
            //{
            //    canvas.DrawLine(rect.Left, rect.Top, rect.Right, rect.Top, arcPaint);
            //    canvas.DrawLine(rect.Right, rect.Top, rect.Right, rect.Bottom, arcPaint);
            //    canvas.DrawLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom, arcPaint);
            //    canvas.DrawLine(rect.Left, rect.Bottom, rect.Left, rect.Bottom - (rect.Bottom * (_endAngle % size)) / size, arcPaint);
            //}


            //canvas.DrawOval(rect, outlinePaint);

            //using (SKPath path = new SKPath())
            //{
            //    path.AddRect(rect, SKPathDirection.Clockwise);
            //    path.AddArc(rect, START_ANGLE, _endAngle);
            //    SKPaint arcPaint;
            //    if (_endAngle <= 180)
            //        arcPaint = arcPaintGreen;
            //    else if (_endAngle >= 315)
            //        arcPaint = arcPaintRed;
            //    else
            //        arcPaint = arcPaintYellow;

            //    canvas.DrawPath(path, arcPaint);
            //}
        }
    }
}
