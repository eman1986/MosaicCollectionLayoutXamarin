using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using CoreGraphics;
using Foundation;
using MosaicCollectionLayoutXamarin.Constants;
using MosaicCollectionLayoutXamarin.Helpers;
using UIKit;

namespace MosaicCollectionLayoutXamarin.CollectionView.Layouts
{
    [Register("MosaicCollectionLayout")]
    public class MosaicCollectionLayout : UICollectionViewLayout
    {
        private CGRect _contentBounds = CGRect.Empty;
        private readonly List<UICollectionViewLayoutAttributes> _cachedAttributes = new List<UICollectionViewLayoutAttributes>();

        public MosaicCollectionLayout(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void PrepareLayout()
        {
            base.PrepareLayout();

            var sectionCount = CollectionView.NumberOfSections();

            if (sectionCount == 0)
            {
                return;
            }

            // Reset Cache
            _cachedAttributes.Clear();
            _contentBounds = new CGRect(CGPoint.Empty, CollectionView.Bounds.Size);

            var itemCount = CollectionView.NumberOfItemsInSection(0);
            var currentIndex = 0;
            var itemStyle = DetermineItemStyle(itemCount);
            var lastFrame = CGRect.Empty;
            var cvWidth = CollectionView.Bounds.Width;
            var cvHeight = CollectionView.Bounds.Height;

            while (currentIndex < itemCount)
            {
                CGRect segmentFrame;
                var segmentRects = new List<CGRect>();

                switch (itemStyle)
                {
                    case MosaicStyle.FullWidth:
                        segmentFrame = new CGRect(0, lastFrame.GetMaxY() + 1, cvWidth, cvHeight);

                        segmentRects.Add(segmentFrame);
                        break;
                    case MosaicStyle.HalfWidth:
                        segmentFrame = new CGRect(0, lastFrame.GetMaxY() + 1, cvWidth, cvHeight / 2);

                        var slices = segmentFrame.DividedIntegral((nfloat) 0.5, CGRectEdge.MinXEdge);

                        segmentRects.Add(slices["First"]);
                        segmentRects.Add(slices["Second"]);

                        break;
                    case MosaicStyle.TwoThirdsByOneThird:
                        segmentFrame = new CGRect(0, lastFrame.GetMaxY() + 1, cvWidth, cvHeight / 3);

                        var twoThirdsHorizontalSlices = segmentFrame.DividedIntegral((nfloat) (2.0 / 3.0), CGRectEdge.MinXEdge);
                        var twoThirdsVerticalSlices = segmentFrame.DividedIntegral((nfloat) 0.5, CGRectEdge.MinYEdge);

                        segmentRects.Add(twoThirdsHorizontalSlices["First"]);
                        segmentRects.Add(twoThirdsVerticalSlices["First"]);
                        segmentRects.Add(twoThirdsVerticalSlices["Second"]);
                        break;
                    case MosaicStyle.OneThirdByTwoThirds:
                        segmentFrame = new CGRect(0, lastFrame.GetMaxY() + 1, cvWidth, cvHeight / 3);

                        var oneThirdHorizontalSlices = segmentFrame.DividedIntegral((nfloat) (1.0 / 3.0), CGRectEdge.MinXEdge);
                        var oneThirdVerticalSlices = segmentFrame.DividedIntegral((nfloat) 0.5, CGRectEdge.MinYEdge);

                        segmentRects.Add(oneThirdVerticalSlices["First"]);
                        segmentRects.Add(oneThirdVerticalSlices["Second"]);
                        segmentRects.Add(oneThirdHorizontalSlices["Second"]);
                        break;
                }

                // cache attributes.
                foreach (var rect in segmentRects)
                {
                    var indexPath = NSIndexPath.FromRowSection(currentIndex, 0);
                    var attributes = UICollectionViewLayoutAttributes.CreateForCell(indexPath);

                    attributes.Frame = rect;

                    _cachedAttributes.Add(attributes);
                    _contentBounds = _contentBounds.UnionWith(lastFrame);

                    currentIndex++;
                    lastFrame = rect;
                }

                // determine style for next segment
                switch (itemCount - currentIndex)
                {
                    case 1:
                        itemStyle = MosaicStyle.FullWidth;
                        break;
                    case 2:
                        itemStyle = MosaicStyle.HalfWidth;
                        break;
                    default:
                        switch (itemStyle)
                        {
                            case MosaicStyle.FullWidth:
                                itemStyle = MosaicStyle.HalfWidth;
                                break;
                            case MosaicStyle.HalfWidth:
                                itemStyle = MosaicStyle.TwoThirdsByOneThird;
                                break;
                            case MosaicStyle.TwoThirdsByOneThird:
                                itemStyle = MosaicStyle.OneThirdByTwoThirds;
                                break;
                            case MosaicStyle.OneThirdByTwoThirds:
                                itemStyle = MosaicStyle.HalfWidth;
                                break;
                        }
                        break;
                }
            }
        }

        public override CGSize CollectionViewContentSize => _contentBounds.Size;

        public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds)
        {
            return newBounds.Size != CollectionView.Bounds.Size;
        }

        public override UICollectionViewLayoutAttributes LayoutAttributesForItem(NSIndexPath indexPath)
        {
            return _cachedAttributes[indexPath.Row];
        }

        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
        {
            var attributes = new List<UICollectionViewLayoutAttributes>();

            foreach (var attribute in _cachedAttributes)
            {
                if (rect.IntersectsWith(attribute.Frame))
                {
                    attributes.Add(attribute);
                }
            }

            return attributes.ToArray();
        }

        private static MosaicStyle DetermineItemStyle(nint count)
        {
            switch (count)
            {
                case 1:
                    return MosaicStyle.FullWidth;
                case 2:
                    return MosaicStyle.HalfWidth;
                case 3:
                case 5:
                    return MosaicStyle.OneThirdByTwoThirds;
                default:
                    return MosaicStyle.TwoThirdsByOneThird;
            }
        }
    }
}