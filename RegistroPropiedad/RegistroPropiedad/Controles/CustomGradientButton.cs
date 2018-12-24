using System;
using Xamarin.Forms;

namespace RegistroPropiedad.CustomControls
{
    public class CustomGradientButton : Button
    {
        public static readonly BindableProperty StartColorProperty =
        BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(CustomGradientButton),
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
        BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(CustomGradientButton),
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
                                 typeof(CustomGradientButton), 1.0D, BindingMode.OneWay);

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
                                    typeof(CustomGradientButton), false, BindingMode.OneWay);


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

        public CustomGradientButton()
        {
        }
    }
}
