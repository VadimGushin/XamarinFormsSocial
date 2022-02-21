using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinSocial.CustomElements
{
    public class CustomEntry : Entry
    {
        public CustomEntry()
        {
            this.Unfocused += CustomEntryUnfocused;
        }

        #region Bindable Properties
        public static readonly BindableProperty CustomEntryUnfocusedProperty =
                                                BindableProperty.Create(
                                                propertyName: nameof(CustomEntryUnfocusedCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(CustomEntry),
                                                defaultValue: null);
        #endregion

        private void CustomEntryUnfocused(object sender, EventArgs e)
        {
            CustomEntryUnfocusedCommand?.Execute(null);
        }

        #region Commands
        /// <summary>
        /// Unfocused command Custom Entry
        /// </summary>
        /// 
        public ICommand CustomEntryUnfocusedCommand
        {
            get { return (ICommand)GetValue(CustomEntryUnfocusedProperty); }
            set { SetValue(CustomEntryUnfocusedProperty, value); }
        }
        #endregion
    }
}
