using System;

using Foundation;
using MosaicCollectionLayoutXamarin.CollectionView.Cells;
using MosaicCollectionLayoutXamarin.CollectionView.Layouts;
using MosaicCollectionLayoutXamarin.CollectionView.Sources;
using MosaicCollectionLayoutXamarin.Models;
using SDWebImage;
using UIKit;

namespace MosaicCollectionLayoutXamarin.TableView.Cells
{
    public partial class PostViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("PostViewCell");
        public static readonly UINib Nib;

        private PostImageCollectionSource _postImageCollectionSource;

        static PostViewCell()
        {
            Nib = UINib.FromName("PostViewCell", NSBundle.MainBundle);
        }

        protected PostViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(Post post)
        {
            _postImageCollectionSource = new PostImageCollectionSource();

            ivProfileImage.SetImage(new NSUrl(post.ProfileImageUrl));
            lblFullName.Text = post.Fullname;
            lblPostDate.Text = post.PostDate;
            lblHeader.Text = post.HeaderText;

            if (post.ImageUrls.Count > 0)
            {
                cvImages.Hidden = false;

                cvImages.RegisterNibForCell(ImagesCollectionViewCell.Nib, ImagesCollectionViewCell.Key);
                cvImages.RegisterNibForCell(ImagesMoreCollectionViewCell.Nib, ImagesMoreCollectionViewCell.Key);
                cvImages.Source = _postImageCollectionSource;
//                cvImages.SetCollectionViewLayout(new MosaicCollectionLayout(), false);

                _postImageCollectionSource.Update(post.ImageUrls.Count > 5 ? post.ImageUrls.GetRange(0, 5) : post.ImageUrls, post.ImageUrls);

                InvokeOnMainThread(cvImages.ReloadData);
            }
            else
            {
                cvImages.Hidden = true;
            }
        }
    }
}