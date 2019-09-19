using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MosaicCollectionLayoutXamarin.CollectionView.Cells;
using UIKit;

namespace MosaicCollectionLayoutXamarin.CollectionView.Sources
{
    public class PostImageCollectionSource : UICollectionViewSource
    {
        public event EventHandler<List<string>> OnItemSelect;

        private List<string> _presentationImageUrlsList;

        private List<string> _fullImageUrlsList;

        public override nint NumberOfSections(UICollectionView collectionView) => 1;

        public override nint GetItemsCount(UICollectionView collectionView, nint section) => _presentationImageUrlsList?.Count ?? 0;

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var imageUrl = _presentationImageUrlsList.ElementAtOrDefault(indexPath.Row);

            if (indexPath.Row == 4 && _fullImageUrlsList.Count > 5)
            {
                var cellImagesMore = collectionView.DequeueReusableCell(ImagesMoreCollectionViewCell.Key, indexPath) as ImagesMoreCollectionViewCell;
                var additionalImagesCount = _fullImageUrlsList.Count - 5;

                cellImagesMore?.SetData(imageUrl, additionalImagesCount);

                return cellImagesMore;
            }

            var cell = collectionView.DequeueReusableCell(ImagesCollectionViewCell.Key, indexPath) as ImagesCollectionViewCell;

            cell?.SetData(imageUrl);

            return cell;
        }

        public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        public void Update(List<string> presentationImageUrls, List<string> imageUrls)
        {
            _presentationImageUrlsList = presentationImageUrls;
            _fullImageUrlsList = imageUrls;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            OnItemSelect?.Invoke(collectionView, _fullImageUrlsList);
        }
    }
}