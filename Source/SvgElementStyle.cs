﻿using System;
using System.ComponentModel;
using Svg.DataTypes;

namespace Svg
{
    public partial class SvgElement
    {
        private bool _dirty;

        /// <summary>
        /// Gets or sets a value indicating whether this element's 'Path' is dirty.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the path is dirty; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool IsPathDirty
        {
            get { return this._dirty; }
            set { this._dirty = value; }
        }

        /// <summary>
        /// Force recreation of the paths for the element and it's children.
        /// </summary>
        public void InvalidateChildPaths()
        {
            this.IsPathDirty = true;
            foreach (SvgElement element in this.Children)
            {
                element.InvalidateChildPaths();
            }
        }

        protected static float FixOpacityValue(float value)
        {
            const float max = 1.0f;
            const float min = 0.0f;
            return Math.Min(Math.Max(value, min), max);
        }

        /// <summary>
        /// Gets or sets the fill <see cref="SvgPaintServer"/> of this element.
        /// </summary>
        [SvgAttribute("fill")]
        public virtual SvgPaintServer Fill
        {
            get { return GetAttribute("fill", true, SvgPaintServer.NotSet); }
            set { Attributes["fill"] = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="SvgPaintServer"/> to be used when rendering a stroke around this element.
        /// </summary>
        [SvgAttribute("stroke")]
        public virtual SvgPaintServer Stroke
        {
            get { return GetAttribute<SvgPaintServer>("stroke", true); }
            set { Attributes["stroke"] = value; }
        }

        [SvgAttribute("fill-rule")]
        public virtual SvgFillRule FillRule
        {
            get { return GetAttribute("fill-rule", true, SvgFillRule.NonZero); }
            set { Attributes["fill-rule"] = value; }
        }

        /// <summary>
        /// Gets or sets the opacity of this element's <see cref="Fill"/>.
        /// </summary>
        [SvgAttribute("fill-opacity")]
        public virtual float FillOpacity
        {
            get { return GetAttribute("fill-opacity", true, 1f); }
            set { Attributes["fill-opacity"] = FixOpacityValue(value); }
        }

        /// <summary>
        /// Gets or sets the width of the stroke (if the <see cref="Stroke"/> property has a valid value specified.
        /// </summary>
        [SvgAttribute("stroke-width")]
        public virtual SvgUnit StrokeWidth
        {
            get { return GetAttribute<SvgUnit>("stroke-width", true, 1f); }
            set { Attributes["stroke-width"] = value; }
        }

        [SvgAttribute("stroke-linecap")]
        public virtual SvgStrokeLineCap StrokeLineCap
        {
            get { return GetAttribute("stroke-linecap", true, SvgStrokeLineCap.Butt); }
            set { Attributes["stroke-linecap"] = value; }
        }

        [SvgAttribute("stroke-linejoin")]
        public virtual SvgStrokeLineJoin StrokeLineJoin
        {
            get { return GetAttribute("stroke-linejoin", true, SvgStrokeLineJoin.Miter); }
            set { Attributes["stroke-linejoin"] = value; }
        }

        [SvgAttribute("stroke-miterlimit")]
        public virtual float StrokeMiterLimit
        {
            get { return GetAttribute("stroke-miterlimit", true, 4f); }
            set { Attributes["stroke-miterlimit"] = value; }
        }

        [TypeConverter(typeof(SvgStrokeDashArrayConverter))]
        [SvgAttribute("stroke-dasharray")]
        public virtual SvgUnitCollection StrokeDashArray
        {
            get { return GetAttribute<SvgUnitCollection>("stroke-dasharray", true); }
            set { Attributes["stroke-dasharray"] = value; }
        }

        [SvgAttribute("stroke-dashoffset")]
        public virtual SvgUnit StrokeDashOffset
        {
            get { return GetAttribute("stroke-dashoffset", true, SvgUnit.Empty); }
            set { Attributes["stroke-dashoffset"] = value; }
        }

        /// <summary>
        /// Gets or sets the opacity of the stroke, if the <see cref="Stroke"/> property has been specified. 1.0 is fully opaque; 0.0 is transparent.
        /// </summary>
        [SvgAttribute("stroke-opacity")]
        public virtual float StrokeOpacity
        {
            get { return GetAttribute("stroke-opacity", true, 1f); }
            set { Attributes["stroke-opacity"] = FixOpacityValue(value); }
        }

        /// <summary>
        /// Gets or sets the opacity of the element. 1.0 is fully opaque; 0.0 is transparent.
        /// </summary>
        [SvgAttribute("opacity")]
        public virtual float Opacity
        {
            get { return GetAttribute("opacity", false, 1f); }
            set { Attributes["opacity"] = FixOpacityValue(value); }
        }

        /// <summary>
        /// Refers to the AnitAlias rendering of shapes.
        /// </summary>
        [SvgAttribute("shape-rendering")]
        public virtual SvgShapeRendering ShapeRendering
        {
            get { return GetAttribute("shape-rendering", true, SvgShapeRendering.Auto); }
            set { Attributes["shape-rendering"] = value; }
        }

        /// <summary>
        /// Gets or sets the color space for gradient interpolations, color animations and alpha compositing.
        /// </summary>
        [SvgAttribute("color-interpolation")]
        public SvgColourInterpolation ColorInterpolation
        {
            get { return GetAttribute("color-interpolation", true, SvgColourInterpolation.SRGB); }
            set { Attributes["color-interpolation"] = value; }
        }

        /// <summary>
        /// Gets or sets the color space for imaging operations performed via filter effects.
        /// NOT currently mapped through to bitmap
        /// </summary>
        [SvgAttribute("color-interpolation-filters")]
        public SvgColourInterpolation ColorInterpolationFilters
        {
            get { return GetAttribute("color-interpolation-filters", true, SvgColourInterpolation.LinearRGB); }
            set { Attributes["color-interpolation-filters"] = value; }
        }

        /// <summary>
        /// Gets or sets a value to determine whether the element will be rendered.
        /// </summary>
        [SvgAttribute("visibility")]
        public virtual string Visibility
        {
            get { return GetAttribute("visibility", true, "visible"); }
            set { Attributes["visibility"] = value; }
        }

        /// <summary>
        /// Gets or sets a value to determine whether the element will be rendered.
        /// Needed to support SVG attribute display="none"
        /// </summary>
        [SvgAttribute("display")]
        public virtual string Display
        {
            get { return GetAttribute("display", false, "inline"); }
            set { Attributes["display"] = value; }
        }

        /// <summary>
        /// Gets or sets the text anchor.
        /// </summary>
        [SvgAttribute("text-anchor")]
        public virtual SvgTextAnchor TextAnchor
        {
            get { return GetAttribute("text-anchor", true, SvgTextAnchor.Start); }
            set { Attributes["text-anchor"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Specifies dominant-baseline positioning of text.
        /// </summary>
        [SvgAttribute("baseline-shift")]
        public virtual string BaselineShift
        {
            get { return GetAttribute("baseline-shift", false, "baseline"); }
            set { Attributes["baseline-shift"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Indicates which font family is to be used to render the text.
        /// </summary>
        [SvgAttribute("font-family")]
        public virtual string FontFamily
        {
            get { return GetAttribute<string>("font-family", true); }
            set { Attributes["font-family"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Refers to the size of the font from baseline to baseline when multiple lines of text are set solid in a multiline layout environment.
        /// </summary>
        [SvgAttribute("font-size")]
        public virtual SvgUnit FontSize
        {
            get { return GetAttribute("font-size", true, SvgUnit.Empty); }
            set { Attributes["font-size"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Refers to the style of the font.
        /// </summary>
        [SvgAttribute("font-style")]
        public virtual SvgFontStyle FontStyle
        {
            get { return GetAttribute("font-style", true, SvgFontStyle.Normal); }
            set { Attributes["font-style"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Refers to the varient of the font.
        /// </summary>
        [SvgAttribute("font-variant")]
        public virtual SvgFontVariant FontVariant
        {
            get { return GetAttribute("font-variant", true, SvgFontVariant.Normal); }
            set { Attributes["font-variant"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Refers to the boldness of the font.
        /// </summary>
        [SvgAttribute("text-decoration")]
        public virtual SvgTextDecoration TextDecoration
        {
            get { return GetAttribute("text-decoration", true, SvgTextDecoration.None); }
            set { Attributes["text-decoration"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Refers to the boldness of the font.
        /// </summary>
        [SvgAttribute("font-weight")]
        public virtual SvgFontWeight FontWeight
        {
            get { return GetAttribute("font-weight", true, SvgFontWeight.Normal); }
            set { Attributes["font-weight"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Indicates the desired amount of condensing or expansion in the glyphs used to render the text.
        /// </summary>
        [SvgAttribute("font-stretch")]
        public virtual SvgFontStretch FontStretch
        {
            get { return GetAttribute("font-stretch", true, SvgFontStretch.Normal); }
            set { Attributes["font-stretch"] = value; IsPathDirty = true; }
        }

        /// <summary>
        /// Refers to the text transformation.
        /// </summary>
        [SvgAttribute("text-transform")]
        public virtual SvgTextTransformation TextTransformation
        {
            get { return GetAttribute("text-transform", true, SvgTextTransformation.Inherit); }
            set { Attributes["text-transform"] = value; IsPathDirty = true; }
        }

        private enum FontParseState
        {
            fontStyle,
            fontVariant,
            fontWeight,
            fontSize,
            fontFamilyNext,
            fontFamilyCurr
        }

        /// <summary>
        /// Set all font information.
        /// </summary>
        [SvgAttribute("font")]
        public virtual string Font
        {
            get { return GetAttribute("font", true, string.Empty); }
            set
            {
                var state = FontParseState.fontStyle;
                var parts = value.Split(' ');

                SvgFontStyle fontStyle;
                SvgFontVariant fontVariant;
                SvgFontWeight fontWeight;
                SvgUnit fontSize;

                bool success;
                string[] sizes;
                string part;

                for (var i = 0; i < parts.Length; i++)
                {
                    part = parts[i];
                    success = false;
                    while (!success)
                    {
                        switch (state)
                        {
                            case FontParseState.fontStyle:
                                success = Enums.TryParse(part, out fontStyle);
                                if (success) FontStyle = fontStyle;
                                state++;
                                break;
                            case FontParseState.fontVariant:
                                success = Enums.TryParse(part, out fontVariant);
                                if (success) FontVariant = fontVariant;
                                state++;
                                break;
                            case FontParseState.fontWeight:
                                success = Enums.TryParse(part, out fontWeight);
                                if (success) FontWeight = fontWeight;
                                state++;
                                break;
                            case FontParseState.fontSize:
                                sizes = part.Split('/');
                                try
                                {
                                    fontSize = (SvgUnit)(new SvgUnitConverter().ConvertFromInvariantString(sizes[0]));
                                    success = true;
                                    FontSize = fontSize;
                                }
                                catch { }
                                state++;
                                break;
                            case FontParseState.fontFamilyNext:
                                state++;
                                success = true;
                                break;
                        }
                    }

                    switch (state)
                    {
                        case FontParseState.fontFamilyNext:
                            FontFamily = string.Join(" ", parts, i + 1, parts.Length - (i + 1));
                            i = int.MaxValue - 2;
                            break;
                        case FontParseState.fontFamilyCurr:
                            FontFamily = string.Join(" ", parts, i, parts.Length - (i));
                            i = int.MaxValue - 2;
                            break;
                    }

                }

                Attributes["font"] = value;
                IsPathDirty = true;
            }
        }
    }
}
