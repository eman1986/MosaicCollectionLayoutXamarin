using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using MosaicCollectionLayoutXamarin.Constants;
using MosaicCollectionLayoutXamarin.Helpers;
using UIKit;

namespace MosaicCollectionLayoutXamarin.CollectionView.Layouts
{
    public class MosaicCollectionLayout : UICollectionViewLayout
    {
        private CGRect _contentBounds = CGRect.Empty;
        private readonly List<UICollectionViewLayoutAttributes> _cachedAttributes = new List<UICollectionViewLayoutAttributes>();

        public MosaicCollectionLayout()
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public MosaicCollectionLayout(NSCoder coder) : base(coder)
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
            var itemStyle = MosaicStyle.FullWidth;
            var lastFrame = CGRect.Empty;
            var cvWidth = CollectionView.Bounds.Width;

            while (currentIndex < itemCount)
            {
                var segmentFrame = new CGRect(0, lastFrame.GetMaxY() + 1, cvWidth, 200);
                var segmentRects = new List<CGRect>();

                switch (itemStyle)
                {
                    case MosaicStyle.FullWidth:
                        segmentRects.Add(segmentFrame);
                        break;
                    case MosaicStyle.HalfWidth:
                        var slices = segmentFrame.DividedIntegral((nfloat) 0.5, CGRectEdge.MinXEdge);

                        segmentRects.Add(slices["First"]);
                        segmentRects.Add(slices["Second"]);

                        break;
                    case MosaicStyle.TwoThirdsByOneThird:
                        var twoThirdsHorizontalSlices = segmentFrame.DividedIntegral((nfloat) (2.0 / 3.0), CGRectEdge.MinXEdge);
                        var twoThirdsVerticalSlices = segmentFrame.DividedIntegral((nfloat) 0.5, CGRectEdge.MinYEdge);

                        segmentRects.Add(twoThirdsHorizontalSlices["First"]);
                        segmentRects.Add(twoThirdsVerticalSlices["First"]);
                        segmentRects.Add(twoThirdsVerticalSlices["Second"]);
                        break;
                    case MosaicStyle.OneThirdByTwoThirds:
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
                    var indexPath = NSIndexPath.FromRowSection(currentIndex, 1);
                    var attributes = UICollectionViewLayoutAttributes.CreateForCell(indexPath);

                    attributes.Frame = rect;
                    _cachedAttributes.Add(attributes);

                    var unionRect = _contentBounds.UnionWith(lastFrame);

                    _contentBounds = unionRect;

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

        public override CGSize CollectionViewContentSize => CollectionView.Bounds.Size;

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

            // Find any cell that sits within the query rect.
            var lastIndex = _cachedAttributes.Last().IndexPath;
            var firstMatchIndex = BinSearch(rect, 0, lastIndex.Row);

            if (firstMatchIndex == null)
            {
                return attributes.ToArray();
            }

            // Starting from the match, loop up and down through the array until all the attributes have been added
            // within the query rect.
            _cachedAttributes.Reverse(lastIndex.Row, firstMatchIndex.Value);

            foreach (var attribute in _cachedAttributes)
            {
                if (attribute.Frame.GetMaxY() >= rect.GetMinY())
                {
                    break;
                }

                attributes.Add(attribute);
            }

            _cachedAttributes.Reverse(firstMatchIndex.Value, _cachedAttributes.Count - firstMatchIndex.Value);

            foreach (var attribute in _cachedAttributes)
            {
                if (attribute.Frame.GetMinY() <= rect.GetMaxY())
                {
                    break;
                }

                attributes.Add(attribute);
            }

            return attributes.ToArray();
        }

        /// <summary>
        /// Perform a binary search on the cached attributes array.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>int?</returns>
        private int? BinSearch(CGRect rect, int start, int end)
        {
            if (end < start)
            {
                return null;
            }

            var mid = (start + end) / 2;
            var attribute = _cachedAttributes[mid];

            if (attribute.Frame.IntersectsWith(rect))
            {
                return mid;
            }

            return attribute.Frame.GetMaxY() < rect.GetMaxY() ?
                BinSearch(rect, (mid + 1), end) :
                BinSearch(rect, start, (end - 1));
        }
    }
}