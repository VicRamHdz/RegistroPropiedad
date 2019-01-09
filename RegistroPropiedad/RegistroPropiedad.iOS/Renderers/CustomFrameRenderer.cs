using System;
using System.Drawing;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using RegistroPropiedad.CustomControls;
using RegistroPropiedad.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace RegistroPropiedad.iOS.Renderers
{
    public class CustomFrameRenderer : VisualElementRenderer<CustomFrame>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomFrame> e)
        {
            base.OnElementChanged(e);
            //ApplyStyle();
            SetGradientColor();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            SetGradientColor();
        }

        private void SetGradientColor()
        {
            if (this.Element == null)
                return;
            var element = (CustomFrame)this.Element;
            var gradientLayer = new CAGradientLayer();
            gradientLayer.CornerRadius = element.CornerRadius;
            gradientLayer.Frame = Bounds;
            gradientLayer.Colors = new CGColor[] { element.StartColor.MultiplyAlpha(element.AlphaMultiplier).ToCGColor(), element.EndColor.MultiplyAlpha(element.AlphaMultiplier).ToCGColor() };
            if (element.HasBorder)
            {
                gradientLayer.BorderWidth = (nfloat)element.StrokeThickness;
                gradientLayer.BorderColor = element.OutlineColor.ToCGColor();
            }
            // Shadow
            if (element.HasShadow)
            {
                gradientLayer.ShadowColor = element.ShadowColor.ToCGColor();
                gradientLayer.ShadowOpacity = 0.1f;
                gradientLayer.ShadowRadius = 2.0f;
                gradientLayer.ShadowOffset = new SizeF(3, 3);
            }
            var layer = this?.Layer.Sublayers.LastOrDefault();
            this?.Layer.InsertSublayerBelow(gradientLayer, layer);
        }

        public override void LayoutSubviews()
        {
            var newBounds = Element.Bounds.ToRectangleF();
            foreach (var layer in this?.Layer.Sublayers.Where(layer => layer is CAGradientLayer))
            {
                layer.Frame = new CGRect(0, 0, newBounds.Width, newBounds.Height);
            }
            base.LayoutSubviews();
        }

        private void ApplyStyle()
        {
            var elem = (CustomFrame)this.Element;
            if (elem != null)
            {
                // Border
                this.Layer.CornerRadius = (float)elem.CornerRadius;
                this.Layer.Bounds.Inset((int)elem.StrokeThickness, (int)elem.StrokeThickness);
                Layer.BorderColor = elem.OutlineColor.ToCGColor();
                Layer.BorderWidth = (float)elem.StrokeThickness;
                Layer.BackgroundColor = elem.BackgroundColor.ToCGColor();

                // Shadow
                if (elem.HasShadow)
                {
                    this.Layer.ShadowColor = UIColor.DarkGray.CGColor;
                    this.Layer.ShadowOpacity = 0.6f;
                    this.Layer.ShadowRadius = 2.0f;
                    this.Layer.ShadowOffset = new SizeF(0, 0);
                    this.Layer.MasksToBounds = true;
                }
            }
        }
    }
}
