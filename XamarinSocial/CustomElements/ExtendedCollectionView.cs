using System;
using Xamarin.Forms;

namespace XamarinSocial.CustomElements
{
    public class ExtendedCollectionView : CollectionView
    {
        public event EventHandler Disappeared;

        public void RaiseDisappeared(object element)
        {
            Disappeared?.Invoke(element, null);
        }
    }
}
