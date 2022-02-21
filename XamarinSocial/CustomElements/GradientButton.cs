using System;
using Xamarin.Forms;

namespace XamarinSocial.CustomElements
{
    public class GradientButton : Button
    {
        #region enum
        // specifies the orientation of the gradient color
        public enum GradientOrientationStates
        {
            Vertical,
            Horizontal
        }
        #endregion

        #region bindable properties
        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(
                nameof(StartColor),
                typeof(Color),
                typeof(GradientButton),
                default(Color),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: HandleStartColorValuePropertyChanged);

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(
                nameof(EndColor),
                typeof(Color),
                typeof(GradientButton),
                default(Color),
                 defaultBindingMode: BindingMode.TwoWay,
                 propertyChanged: HandleEndColorValuePropertyChanged);

        public static readonly BindableProperty GradientOrientationProperty =
            BindableProperty.Create(
                nameof(GradientOrientation),
                typeof(GradientOrientationStates),
                typeof(GradientButton),
                default(GradientOrientationStates));
        #endregion

        #region properties
        /// <summary>
        /// The start color of the gradient background
        /// </summary>
        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set 
            {
                SetValue(StartColorProperty, value);
                OnPropertyChanged();
            } 
        }

        /// <summary>
        ///  The end color of the gradient background
        /// </summary>
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set
            {
                SetValue(EndColorProperty, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///  The gradient color orientation
        /// </summary>
        public GradientOrientationStates GradientOrientation
        {
            get => (GradientOrientationStates)GetValue(GradientOrientationProperty);
            set => SetValue(GradientOrientationProperty, value);
        }
        #endregion

        private static void HandleStartColorValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((GradientButton)bindable).StartColor = (Color)newValue;
        }

        private static void HandleEndColorValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((GradientButton)bindable).EndColor = (Color)newValue;
        }
    }
}
