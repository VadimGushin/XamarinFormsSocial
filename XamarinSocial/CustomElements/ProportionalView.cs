using Xamarin.Forms;

namespace XamarinSocial.CustomElements
{
    public class ProportionalView : ContentView
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                base.OnSizeAllocated(width, height);
                HeightRequest = HeightToWidthPropotional * Width;
            });
        }

        #region Properties

        /// <summary>
        /// Value double for Aspect Fit 
        /// </summary>
        public double HeightToWidthPropotional
        {
            get { return (double)base.GetValue(ValueProperty); }
            set { base.SetValue(ValueProperty, value); }
        }
        #endregion Properties

        #region Bindable Properties

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
                                                propertyName: nameof(HeightToWidthPropotional),
                                                returnType: typeof(double),
                                                declaringType: typeof(ProportionalView),
                                                defaultValue: default);
        #endregion
    }
}
