using System;

using Foundation;
using SDWebImage;
using UIKit;

namespace MosaicCollectionLayoutXamarin.CollectionView.Cells
{
    public partial class ImagesMoreCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("ImagesMoreCollectionViewCell");
        public static readonly UINib Nib;

        static ImagesMoreCollectionViewCell()
        {
            Nib = UINib.FromName("ImagesMoreCollectionViewCell", NSBundle.MainBundle);
        }

        protected ImagesMoreCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(string imageUrl, int additionalImageCount)
        {
            ivMain.SetImage(new NSUrl(imageUrl), UIImage.FromBundle("image_placeholder"));
            lblAdditionalImageCount.Text = $"+{additionalImageCount}";
        }
    }
}
