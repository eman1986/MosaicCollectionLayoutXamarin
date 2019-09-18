using System;
using System.Collections.Generic;
using CoreGraphics;

namespace MosaicCollectionLayoutXamarin.Helpers
{
    public static class UiExtensions
    {
        public static Dictionary<string, CGRect> DividedIntegral(this CGRect rect, nfloat fraction, CGRectEdge fromEdge)
        {
            nfloat dimension = 0.0f;

            switch (fromEdge)
            {
                case CGRectEdge.MinXEdge:
                case CGRectEdge.MaxXEdge:
                    dimension = rect.Size.Width;
                    break;
                case CGRectEdge.MinYEdge:
                case CGRectEdge.MaxYEdge:
                    dimension = rect.Size.Height;
                    break;
            }

            var distance = Math.Round(dimension * fraction);
            rect.Divide((nfloat) distance, fromEdge, out var slice, out var remainder);

            var remainderSize = remainder.Size;

            switch (fromEdge)
            {
                case CGRectEdge.MinXEdge:
                case CGRectEdge.MaxXEdge:
                    remainder.X += 1;
                    remainderSize.Width -= 1;
                    remainder.Size = remainderSize;
                    break;
                case CGRectEdge.MinYEdge:
                case CGRectEdge.MaxYEdge:
                    remainder.Y += 1;
                    remainderSize.Height -= 1;
                    remainder.Size = remainderSize;
                    break;
            }

            return new Dictionary<string, CGRect>
            {
                { "First", slice },
                { "Second", remainder }
            };
        }
    }
}