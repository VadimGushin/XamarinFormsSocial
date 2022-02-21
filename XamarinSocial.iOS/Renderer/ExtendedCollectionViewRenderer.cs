using CoreGraphics;
using Foundation;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.CustomElements;
using XamarinSocial.iOS.Renderer;

[assembly: ExportRenderer(typeof(ExtendedCollectionView), typeof(ExtendedCollectionViewRenderer))]
namespace XamarinSocial.iOS.Renderer
{
    //public class ExtendedCollectionViewRenderer : CollectionViewRenderer
    //{
    //    protected override void OnElementChanged(ElementChangedEventArgs<GroupableItemsView> e)
    //    {
    //        base.OnElementChanged(e);

    //        if (Control != null)
    //        {
    //            NSArray views = Control.ValueForKey(new NSString("_subviewCache")) as NSMutableArray;
    //            UICollectionView collectionView = views.GetItem<UICollectionView>(0);
    //            collectionView.Delegate = new MyCollectionViewDelegate((ExtendedCollectionView)Element);
    //        }
    //    }
    //}

    //public class MyCollectionViewDelegate : UICollectionViewDelegate
    //{
    //    private ExtendedCollectionView _customView;
    //    public MyCollectionViewDelegate(ExtendedCollectionView collectionView)
    //    {
    //        this._customView = collectionView;
    //    }

    //    public override void CellDisplayingEnded(UICollectionView collectionView, UICollectionViewCell cell, NSIndexPath indexPath)
    //    {
    //        _customView.RaiseDisappeared(indexPath.Row);
    //    }
    //}

    public class ExtendedCollectionViewRenderer : GroupableItemsViewRenderer<GroupableItemsView,
    CustomItemsViewController>
    {
        protected override CustomItemsViewController CreateController(
            GroupableItemsView itemsView,
            ItemsViewLayout itemsLayout)
        {
            return new CustomItemsViewController(itemsView, itemsLayout);
        }
    }

    public class CustomItemsViewController : GroupableItemsViewController<GroupableItemsView>
    {
        public CustomItemsViewController(GroupableItemsView itemsView, ItemsViewLayout itemsLayout)
            : base(itemsView, itemsLayout)
        {
        }

        protected override UICollectionViewDelegateFlowLayout CreateDelegator()
        {
            return new CustomItemsViewDelegator(ItemsViewLayout, this as CustomItemsViewController);
        }
    }

    public class CustomItemsViewDelegator : ItemsViewDelegator<GroupableItemsView, CustomItemsViewController>
    {
        private Dictionary<NSIndexPath, CGSize> _itemSizeDictionary;

        public CustomItemsViewDelegator(ItemsViewLayout itemsLayout, CustomItemsViewController itemsController)
            : base(itemsLayout, itemsController)
        {
            _itemSizeDictionary = new Dictionary<NSIndexPath, CGSize>();
        }

        /// <summary>
        /// Per default this method returns the Estimated size when its not overriden. This method is called before
        /// the rendering process and sizes the cell correctly before it is displayed in the CollectionView.
        /// Calling the base implementation of this method will throw an exception when overriding the method.
        /// </summary>
        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout,
            NSIndexPath indexPath)
        {
            UICollectionViewCell cell = collectionView.CellForItem(indexPath);

            if (cell is ItemsViewCell itemsViewCell)
            {
                var value = itemsViewCell.Measure();

                if (_itemSizeDictionary.ContainsKey(indexPath))
                {
                    _itemSizeDictionary[indexPath] = value;
                    return value;
                }
                _itemSizeDictionary.Add(indexPath, value);
                return value;
            }

            bool isSucced = _itemSizeDictionary.TryGetValue(indexPath, out var size);
            return isSucced ? size : ItemsViewLayout.EstimatedItemSize; 
        }
    }
}