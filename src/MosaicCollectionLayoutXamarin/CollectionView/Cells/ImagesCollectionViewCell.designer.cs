﻿// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MosaicCollectionLayoutXamarin.CollectionView.Cells
{
    [Register ("ImagesCollectionViewCell")]
    partial class ImagesCollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ivMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ivMain != null) {
                ivMain.Dispose ();
                ivMain = null;
            }
        }
    }
}