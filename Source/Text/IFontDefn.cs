﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
#if !NO_SDC
using System.Drawing.Drawing2D;
#endif

namespace Svg
{
    public interface IFontDefn : IDisposable
    {
#if !NO_SDC
        float Size { get; }
        float SizeInPoints { get; }
        void AddStringToPath(ISvgRenderer renderer, GraphicsPath path, string text, PointF location);
        float Ascent(ISvgRenderer renderer);
        IList<RectangleF> MeasureCharacters(ISvgRenderer renderer, string text);
        SizeF MeasureString(ISvgRenderer renderer, string text);
#endif
    }
}
