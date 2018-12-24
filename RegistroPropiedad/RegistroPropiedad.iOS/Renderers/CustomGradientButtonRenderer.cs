using System;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using RegistroPropiedad.CustomControls;
using RegistroPropiedad.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomGradientButton), typeof(CustomGradientButtonRenderer))]
namespace RegistroPropiedad.iOS.Renderers
{
    public class CustomGradientButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var formsButton = Element as CustomGradientButton;
                if (formsButton != null)
                {
                    SetGradientColor(formsButton);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null) return;

            if (e.PropertyName == "StartColor" || e.PropertyName == "EndColor" || e.PropertyName == "AlphaMultiplier"
                || e.PropertyName == "HasBorder")
            {
                var formsButton = Element as CustomGradientButton;
                if (formsButton != null)
                {
                    SetGradientColor(formsButton);
                }
            }
        }

        private void SetGradientColor(CustomGradientButton element)
        {
            var gradientLayer = new CAGradientLayer();
            gradientLayer.CornerRadius = element.BorderRadius;
            gradientLayer.Frame = Bounds;
            gradientLayer.Colors = new CGColor[] { element.StartColor.MultiplyAlpha(element.AlphaMultiplier).ToCGColor(), element.EndColor.MultiplyAlpha(element.AlphaMultiplier).ToCGColor() };
            if (element.HasBorder)
            {
                gradientLayer.BorderWidth = (nfloat)element.BorderWidth;
                gradientLayer.BorderColor = element.BorderColor.ToCGColor();//element.EndColor.ToCGColor();
            }
            var layer = Control?.Layer.Sublayers.LastOrDefault();
            Control?.Layer.InsertSublayerBelow(gradientLayer, layer);
            Control?.SetTitleColor(element.TextColor.ToUIColor(), UIControlState.Disabled);
        }

        public override void LayoutSubviews()
        {
            var newBounds = Element.Bounds.ToRectangleF();
            foreach (var layer in Control?.Layer.Sublayers.Where(layer => layer is CAGradientLayer))
            {
                layer.Frame = new CGRect(0, 0, newBounds.Width, newBounds.Height);
            }
            base.LayoutSubviews();
        }
    }
}
