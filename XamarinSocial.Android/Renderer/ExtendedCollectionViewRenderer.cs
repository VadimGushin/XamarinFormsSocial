using Android.Content;
using Android.Support.V7.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinSocial.CustomElements;
using XamarinSocial.Droid.Renderer;

[assembly: ExportRenderer(typeof(ExtendedCollectionView), typeof(ExtendedCollectionViewRenderer))]
namespace XamarinSocial.Droid.Renderer
{
    public class ExtendedCollectionViewRenderer : CollectionViewRenderer
    {
        public ExtendedCollectionViewRenderer(Context context) : base(context)
        {

        }

        private ExtendedCollectionView _extendedCollectionView;

        protected override void OnElementChanged(ElementChangedEventArgs<ItemsView> elementChangedEvent)
        {
            base.OnElementChanged(elementChangedEvent);

            if (elementChangedEvent.NewElement is ExtendedCollectionView enhancedCollectionView)
            {
                _extendedCollectionView = enhancedCollectionView;

                ChildViewRemoved -= ExtendedCollectionViewRenderer_ChildViewRemoved;
                ChildViewRemoved += ExtendedCollectionViewRenderer_ChildViewRemoved;
            }
        }

        private void ExtendedCollectionViewRenderer_ChildViewRemoved(object sender, ChildViewRemovedEventArgs e)
        {
            var position = ((RecyclerView)e.Parent).GetChildViewHolder(e.Child).AdapterPosition;
            _extendedCollectionView.RaiseDisappeared(position);
        }
    }
}