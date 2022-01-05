#if !NO_SDC
using System.Drawing.Drawing2D;
#endif
using System.Globalization;

namespace Svg.Transforms
{
    public sealed class SvgTranslate : SvgTransform
    {
        public float X { get; set; }

        public float Y { get; set; }

#if !NO_SDC
        public override Matrix Matrix
        {
            get
            {
                var matrix = new Matrix();
                matrix.Translate(X, Y);
                return matrix;
            }
        }
#endif

        public override string WriteToString()
        {
            return $"translate({X.ToSvgString()}, {Y.ToSvgString()})";
        }

        public SvgTranslate(float x, float y)
        {
            X = x;
            Y = y;
        }

        public SvgTranslate(float x)
            : this(x, 0f)
        {
        }

        public override object Clone()
        {
            return new SvgTranslate(X, Y);
        }
    }
}
