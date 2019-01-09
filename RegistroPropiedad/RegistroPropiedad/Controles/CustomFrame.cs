using System;
using Xamarin.Forms;

namespace RegistroPropiedad.CustomControls
{
    public class CustomFrame : Frame
    {
        #region Bindable Properties

        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(CustomFrame), default(double), BindingMode.TwoWay);

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly BindableProperty ShadowColorProperty =
            BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(CustomFrame),
                                    Color.Transparent, BindingMode.OneWay);

        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(CustomFrame),
                                 Color.Transparent, BindingMode.OneWay);

        public Color StartColor
        {
            get
            {
                return (Color)GetValue(StartColorProperty);
            }
            set
            {
                SetValue(StartColorProperty, value);
            }
        }

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(CustomFrame),
                                Color.Transparent, BindingMode.OneWay);

        public Color EndColor
        {
            get
            {
                return (Color)GetValue(EndColorProperty);
            }
            set
            {
                SetValue(EndColorProperty, value);
            }
        }

        public static readonly BindableProperty AlphaMultiplierProperty =
         BindableProperty.Create(nameof(AlphaMultiplier), typeof(double),
                                 typeof(CustomFrame), 1.0D, BindingMode.OneWay);

        public double AlphaMultiplier
        {
            get
            {
                return (double)GetValue(AlphaMultiplierProperty);
            }
            set
            {
                SetValue(AlphaMultiplierProperty, value);
            }
        }

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool),
                                    typeof(CustomFrame), false, BindingMode.OneWay);


        public bool HasBorder
        {
            get
            {
                return (bool)GetValue(HasBorderProperty);
            }
            set
            {
                SetValue(HasBorderProperty, value);
            }
        }

        public CustomFrame()
        {
            HasShadow = false;
        }
        #endregion
    }
}
