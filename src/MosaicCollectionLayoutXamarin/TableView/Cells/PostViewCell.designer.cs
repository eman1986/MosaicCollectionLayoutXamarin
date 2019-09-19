// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MosaicCollectionLayoutXamarin.TableView.Cells
{
    [Register ("PostViewCell")]
    partial class PostViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnComments { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnLike { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnShare { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView cvImages { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ivProfileImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblFullName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblHeader { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPostDate { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnComments != null) {
                btnComments.Dispose ();
                btnComments = null;
            }

            if (btnLike != null) {
                btnLike.Dispose ();
                btnLike = null;
            }

            if (btnShare != null) {
                btnShare.Dispose ();
                btnShare = null;
            }

            if (cvImages != null) {
                cvImages.Dispose ();
                cvImages = null;
            }

            if (ivProfileImage != null) {
                ivProfileImage.Dispose ();
                ivProfileImage = null;
            }

            if (lblFullName != null) {
                lblFullName.Dispose ();
                lblFullName = null;
            }

            if (lblHeader != null) {
                lblHeader.Dispose ();
                lblHeader = null;
            }

            if (lblPostDate != null) {
                lblPostDate.Dispose ();
                lblPostDate = null;
            }
        }
    }
}