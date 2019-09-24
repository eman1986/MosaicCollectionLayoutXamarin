using System;

using Foundation;
using SDWebImage;
using UIKit;

namespace MosaicCollectionLayoutXamarin.CollectionView.Cells
{
    public partial class ImagesCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("ImagesCollectionViewCell");
        public static readonly UINib Nib;

        static ImagesCollectionViewCell()
        {
            Nib = UINib.FromName("ImagesCollectionViewCell", NSBundle.MainBundle);
        }

        protected ImagesCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(string imageUrl)
        {
            ivMain.SetImage(new NSUrl(imageUrl), UIImage.FromBundle("image_placeholder"));
        }
    }
}