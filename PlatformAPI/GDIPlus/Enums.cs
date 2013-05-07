using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    //--------------------------------------------------------------------------
    // Fill mode constants
    //--------------------------------------------------------------------------

    public enum FillMode
    {
        FillModeAlternate,        // 0
        FillModeWinding           // 1
    };

    //--------------------------------------------------------------------------
    // Quality mode constants
    //--------------------------------------------------------------------------

    public enum QualityMode
    {
        QualityModeInvalid = -1,
        QualityModeDefault = 0,
        QualityModeLow = 1, // Best performance
        QualityModeHigh = 2  // Best rendering quality
    };

    //--------------------------------------------------------------------------
    // Alpha Compositing mode constants
    //--------------------------------------------------------------------------

    public enum CompositingMode
    {
        CompositingModeSourceOver,    // 0
        CompositingModeSourceCopy     // 1
    };

    //--------------------------------------------------------------------------
    // Alpha Compositing quality constants
    //--------------------------------------------------------------------------

    public enum CompositingQuality
    {
        CompositingQualityInvalid = QualityMode.QualityModeInvalid,
        CompositingQualityDefault = QualityMode.QualityModeDefault,
        CompositingQualityHighSpeed = QualityMode.QualityModeLow,
        CompositingQualityHighQuality = QualityMode.QualityModeHigh,
        CompositingQualityGammaCorrected,
        CompositingQualityAssumeLinear
    };

    //--------------------------------------------------------------------------
    // Unit constants
    //--------------------------------------------------------------------------

    public enum Unit
    {
        UnitWorld,      // 0 -- World coordinate (non-physical unit)
        UnitDisplay,    // 1 -- Variable -- for PageTransform only
        UnitPixel,      // 2 -- Each unit is one device pixel.
        UnitPoint,      // 3 -- Each unit is a printer's point, or 1/72 inch.
        UnitInch,       // 4 -- Each unit is 1 inch.
        UnitDocument,   // 5 -- Each unit is 1/300 inch.
        UnitMillimeter  // 6 -- Each unit is 1 millimeter.
    };

    //--------------------------------------------------------------------------
    // MetafileFrameUnit
    //
    // The frameRect for creating a metafile can be specified in any of these
    // units.  There is an extra frame unit value (MetafileFrameUnitGdi) so
    // that units can be supplied in the same units that GDI expects for
    // frame rects -- these units are in .01 (1/100ths) millimeter units
    // as defined by GDI.
    //--------------------------------------------------------------------------

    public enum MetafileFrameUnit
    {
        MetafileFrameUnitPixel = Unit.UnitPixel,
        MetafileFrameUnitPoint = Unit.UnitPoint,
        MetafileFrameUnitInch = Unit.UnitInch,
        MetafileFrameUnitDocument = Unit.UnitDocument,
        MetafileFrameUnitMillimeter = Unit.UnitMillimeter,
        MetafileFrameUnitGdi                        // GDI compatible .01 MM units
    };

    //--------------------------------------------------------------------------
    // Coordinate space identifiers
    //--------------------------------------------------------------------------

    public enum CoordinateSpace
    {
        CoordinateSpaceWorld,     // 0
        CoordinateSpacePage,      // 1
        CoordinateSpaceDevice     // 2
    };

    //--------------------------------------------------------------------------
    // Various wrap modes for brushes
    //--------------------------------------------------------------------------

    public enum WrapMode
    {
        WrapModeTile,        // 0
        WrapModeTileFlipX,   // 1
        WrapModeTileFlipY,   // 2
        WrapModeTileFlipXY,  // 3
        WrapModeClamp        // 4
    };

    //--------------------------------------------------------------------------
    // Various hatch styles
    //--------------------------------------------------------------------------

    public enum HatchStyle
    {
        HatchStyleHorizontal,                   // 0
        HatchStyleVertical,                     // 1
        HatchStyleForwardDiagonal,              // 2
        HatchStyleBackwardDiagonal,             // 3
        HatchStyleCross,                        // 4
        HatchStyleDiagonalCross,                // 5
        HatchStyle05Percent,                    // 6
        HatchStyle10Percent,                    // 7
        HatchStyle20Percent,                    // 8
        HatchStyle25Percent,                    // 9
        HatchStyle30Percent,                    // 10
        HatchStyle40Percent,                    // 11
        HatchStyle50Percent,                    // 12
        HatchStyle60Percent,                    // 13
        HatchStyle70Percent,                    // 14
        HatchStyle75Percent,                    // 15
        HatchStyle80Percent,                    // 16
        HatchStyle90Percent,                    // 17
        HatchStyleLightDownwardDiagonal,        // 18
        HatchStyleLightUpwardDiagonal,          // 19
        HatchStyleDarkDownwardDiagonal,         // 20
        HatchStyleDarkUpwardDiagonal,           // 21
        HatchStyleWideDownwardDiagonal,         // 22
        HatchStyleWideUpwardDiagonal,           // 23
        HatchStyleLightVertical,                // 24
        HatchStyleLightHorizontal,              // 25
        HatchStyleNarrowVertical,               // 26
        HatchStyleNarrowHorizontal,             // 27
        HatchStyleDarkVertical,                 // 28
        HatchStyleDarkHorizontal,               // 29
        HatchStyleDashedDownwardDiagonal,       // 30
        HatchStyleDashedUpwardDiagonal,         // 31
        HatchStyleDashedHorizontal,             // 32
        HatchStyleDashedVertical,               // 33
        HatchStyleSmallConfetti,                // 34
        HatchStyleLargeConfetti,                // 35
        HatchStyleZigZag,                       // 36
        HatchStyleWave,                         // 37
        HatchStyleDiagonalBrick,                // 38
        HatchStyleHorizontalBrick,              // 39
        HatchStyleWeave,                        // 40
        HatchStylePlaid,                        // 41
        HatchStyleDivot,                        // 42
        HatchStyleDottedGrid,                   // 43
        HatchStyleDottedDiamond,                // 44
        HatchStyleShingle,                      // 45
        HatchStyleTrellis,                      // 46
        HatchStyleSphere,                       // 47
        HatchStyleSmallGrid,                    // 48
        HatchStyleSmallCheckerBoard,            // 49
        HatchStyleLargeCheckerBoard,            // 50
        HatchStyleOutlinedDiamond,              // 51
        HatchStyleSolidDiamond,                 // 52

        HatchStyleTotal,
        HatchStyleLargeGrid = HatchStyleCross,  // 4

        HatchStyleMin = HatchStyleHorizontal,
        HatchStyleMax = HatchStyleTotal - 1,
    };

    //--------------------------------------------------------------------------
    // Dash style constants
    //--------------------------------------------------------------------------

    public enum DashStyle
    {
        DashStyleSolid,          // 0
        DashStyleDash,           // 1
        DashStyleDot,            // 2
        DashStyleDashDot,        // 3
        DashStyleDashDotDot,     // 4
        DashStyleCustom          // 5
    };

    //--------------------------------------------------------------------------
    // Dash cap constants
    //--------------------------------------------------------------------------

    public enum DashCap
    {
        DashCapFlat = 0,
        DashCapRound = 2,
        DashCapTriangle = 3
    };

    //--------------------------------------------------------------------------
    // Line cap constants (only the lowest 8 bits are used).
    //--------------------------------------------------------------------------

    public enum LineCap
    {
        LineCapFlat = 0,
        LineCapSquare = 1,
        LineCapRound = 2,
        LineCapTriangle = 3,

        LineCapNoAnchor = 0x10, // corresponds to flat cap
        LineCapSquareAnchor = 0x11, // corresponds to square cap
        LineCapRoundAnchor = 0x12, // corresponds to round cap
        LineCapDiamondAnchor = 0x13, // corresponds to triangle cap
        LineCapArrowAnchor = 0x14, // no correspondence

        LineCapCustom = 0xff, // custom cap

        LineCapAnchorMask = 0xf0  // mask to check for anchor or not.
    };

    //--------------------------------------------------------------------------
    // Custom Line cap type constants
    //--------------------------------------------------------------------------

    public enum CustomLineCapType
    {
        CustomLineCapTypeDefault = 0,
        CustomLineCapTypeAdjustableArrow = 1
    };

    //--------------------------------------------------------------------------
    // Line join constants
    //--------------------------------------------------------------------------

    public enum LineJoin
    {
        LineJoinMiter = 0,
        LineJoinBevel = 1,
        LineJoinRound = 2,
        LineJoinMiterClipped = 3
    };

    //--------------------------------------------------------------------------
    // Path point types (only the lowest 8 bits are used.)
    //  The lowest 3 bits are interpreted as point type
    //  The higher 5 bits are reserved for flags.
    //--------------------------------------------------------------------------

    public enum PathPointType
    {
        PathPointTypeStart = 0,    // move
        PathPointTypeLine = 1,    // line
        PathPointTypeBezier = 3,    // default Bezier (= cubic Bezier)
        PathPointTypePathTypeMask = 0x07, // type mask (lowest 3 bits).
        PathPointTypeDashMode = 0x10, // currently in dash mode.
        PathPointTypePathMarker = 0x20, // a marker for the path.
        PathPointTypeCloseSubpath = 0x80, // closed flag

        // Path types used for advanced path.

        PathPointTypeBezier3 = 3,         // cubic Bezier
    };


    //--------------------------------------------------------------------------
    // WarpMode constants
    //--------------------------------------------------------------------------

    public enum WarpMode
    {
        WarpModePerspective,    // 0
        WarpModeBilinear        // 1
    };

    //--------------------------------------------------------------------------
    // LineGradient Mode
    //--------------------------------------------------------------------------

    public enum LinearGradientMode
    {
        LinearGradientModeHorizontal,         // 0
        LinearGradientModeVertical,           // 1
        LinearGradientModeForwardDiagonal,    // 2
        LinearGradientModeBackwardDiagonal    // 3
    };

    //--------------------------------------------------------------------------
    // Region Comine Modes
    //--------------------------------------------------------------------------

    public enum CombineMode
    {
        CombineModeReplace,     // 0
        CombineModeIntersect,   // 1
        CombineModeUnion,       // 2
        CombineModeXor,         // 3
        CombineModeExclude,     // 4
        CombineModeComplement   // 5 (Exclude From)
    };

    //--------------------------------------------------------------------------
    // Image types
    //--------------------------------------------------------------------------

    public enum ImageType
    {
        ImageTypeUnknown,   // 0
        ImageTypeBitmap,    // 1
        ImageTypeMetafile   // 2
    };

    //--------------------------------------------------------------------------
    // Interpolation modes
    //--------------------------------------------------------------------------

    public enum InterpolationMode
    {
        InterpolationModeInvalid = QualityMode.QualityModeInvalid,
        InterpolationModeDefault = QualityMode.QualityModeDefault,
        InterpolationModeLowQuality = QualityMode.QualityModeLow,
        InterpolationModeHighQuality = QualityMode.QualityModeHigh,
        InterpolationModeBilinear,
        InterpolationModeBicubic,
        InterpolationModeNearestNeighbor,
        InterpolationModeHighQualityBilinear,
        InterpolationModeHighQualityBicubic
    };

    //--------------------------------------------------------------------------
    // Pen types
    //--------------------------------------------------------------------------

    public enum PenAlignment
    {
        PenAlignmentCenter = 0,
        PenAlignmentInset = 1
    };

    //--------------------------------------------------------------------------
    // Brush types
    //--------------------------------------------------------------------------

    public enum BrushType
    {
        BrushTypeSolidColor = 0,
        BrushTypeHatchFill = 1,
        BrushTypeTextureFill = 2,
        BrushTypePathGradient = 3,
        BrushTypeLinearGradient = 4
    };

    //--------------------------------------------------------------------------
    // Pen's Fill types
    //--------------------------------------------------------------------------

    public enum PenType
    {
        PenTypeSolidColor = BrushType.BrushTypeSolidColor,
        PenTypeHatchFill = BrushType.BrushTypeHatchFill,
        PenTypeTextureFill = BrushType.BrushTypeTextureFill,
        PenTypePathGradient = BrushType.BrushTypePathGradient,
        PenTypeLinearGradient = BrushType.BrushTypeLinearGradient,
        PenTypeUnknown = -1
    };

    //--------------------------------------------------------------------------
    // Matrix Order
    //--------------------------------------------------------------------------

    public enum MatrixOrder
    {
        MatrixOrderPrepend = 0,
        MatrixOrderAppend = 1
    };

    //--------------------------------------------------------------------------
    // Generic font families
    //--------------------------------------------------------------------------

    public enum GenericFontFamily
    {
        GenericFontFamilySerif,
        GenericFontFamilySansSerif,
        GenericFontFamilyMonospace

    };

    //--------------------------------------------------------------------------
    // FontStyle: face types and common styles
    //--------------------------------------------------------------------------

    public enum FontStyle
    {
        FontStyleRegular = 0,
        FontStyleBold = 1,
        FontStyleItalic = 2,
        FontStyleBoldItalic = 3,
        FontStyleUnderline = 4,
        FontStyleStrikeout = 8
    };

    //---------------------------------------------------------------------------
    // Smoothing Mode
    //---------------------------------------------------------------------------

    public enum SmoothingMode
    {
        SmoothingModeInvalid = QualityMode.QualityModeInvalid,
        SmoothingModeDefault = QualityMode.QualityModeDefault,
        SmoothingModeHighSpeed = QualityMode.QualityModeLow,
        SmoothingModeHighQuality = QualityMode.QualityModeHigh,
        SmoothingModeNone,
        SmoothingModeAntiAlias,
        //#if (GDIPVER >= 0x0110)
        //    SmoothingModeAntiAlias8x4 = SmoothingModeAntiAlias,
        //    SmoothingModeAntiAlias8x8
        //#endif //(GDIPVER >= 0x0110) 
    };

    //---------------------------------------------------------------------------
    // Pixel Format Mode
    //---------------------------------------------------------------------------

    public enum PixelOffsetMode
    {
        PixelOffsetModeInvalid = QualityMode.QualityModeInvalid,
        PixelOffsetModeDefault = QualityMode.QualityModeDefault,
        PixelOffsetModeHighSpeed = QualityMode.QualityModeLow,
        PixelOffsetModeHighQuality = QualityMode.QualityModeHigh,
        PixelOffsetModeNone,    // No pixel offset
        PixelOffsetModeHalf     // Offset by -0.5, -0.5 for fast anti-alias perf
    };

    //---------------------------------------------------------------------------
    // Text Rendering Hint
    //---------------------------------------------------------------------------

    public enum TextRenderingHint
    {
        TextRenderingHintSystemDefault = 0,            // Glyph with system default rendering hint
        TextRenderingHintSingleBitPerPixelGridFit,     // Glyph bitmap with hinting
        TextRenderingHintSingleBitPerPixel,            // Glyph bitmap without hinting
        TextRenderingHintAntiAliasGridFit,             // Glyph anti-alias bitmap with hinting
        TextRenderingHintAntiAlias,                    // Glyph anti-alias bitmap without hinting
        TextRenderingHintClearTypeGridFit              // Glyph CT bitmap with hinting
    };

    //---------------------------------------------------------------------------
    // Metafile Types
    //---------------------------------------------------------------------------

    public enum MetafileType
    {
        MetafileTypeInvalid,            // Invalid metafile
        MetafileTypeWmf,                // Standard WMF
        MetafileTypeWmfPlaceable,       // Placeable WMF
        MetafileTypeEmf,                // EMF (not EMF+)
        MetafileTypeEmfPlusOnly,        // EMF+ without dual, down-level records
        MetafileTypeEmfPlusDual         // EMF+ with dual, down-level records
    };

    //---------------------------------------------------------------------------
    // Specifies the type of EMF to record
    //---------------------------------------------------------------------------

    public enum EmfType
    {
        EmfTypeEmfOnly = MetafileType.MetafileTypeEmf,          // no EMF+, only EMF
        EmfTypeEmfPlusOnly = MetafileType.MetafileTypeEmfPlusOnly,  // no EMF, only EMF+
        EmfTypeEmfPlusDual = MetafileType.MetafileTypeEmfPlusDual   // both EMF+ and EMF
    };

    //---------------------------------------------------------------------------
    // EMF+ Persistent object types
    //---------------------------------------------------------------------------

    public enum ObjectType
    {
        ObjectTypeInvalid,
        ObjectTypeBrush,
        ObjectTypePen,
        ObjectTypePath,
        ObjectTypeRegion,
        ObjectTypeImage,
        ObjectTypeFont,
        ObjectTypeStringFormat,
        ObjectTypeImageAttributes,
        ObjectTypeCustomLineCap,
        ObjectTypeMax = ObjectTypeCustomLineCap,
        ObjectTypeMin = ObjectTypeBrush
    }

    //---------------------------------------------------------------------------
    // String format flags
    //
    //  DirectionRightToLeft          - For horizontal text, the reading order is
    //                                  right to left. This value is called
    //                                  the base embedding level by the Unicode
    //                                  bidirectional engine.
    //                                  For vertical text, columns are read from
    //                                  right to left.
    //                                  By default, horizontal or vertical text is
    //                                  read from left to right.
    //
    //  DirectionVertical             - Individual lines of text are vertical. In
    //                                  each line, characters progress from top to
    //                                  bottom.
    //                                  By default, lines of text are horizontal,
    //                                  each new line below the previous line.
    //
    //  NoFitBlackBox                 - Allows parts of glyphs to overhang the
    //                                  bounding rectangle.
    //                                  By default glyphs are first aligned
    //                                  inside the margines, then any glyphs which
    //                                  still overhang the bounding box are
    //                                  repositioned to avoid any overhang.
    //                                  For example when an italic
    //                                  lower case letter f in a font such as
    //                                  Garamond is aligned at the far left of a
    //                                  rectangle, the lower part of the f will
    //                                  reach slightly further left than the left
    //                                  edge of the rectangle. Setting this flag
    //                                  will ensure the character aligns visually
    //                                  with the lines above and below, but may
    //                                  cause some pixels outside the formatting
    //                                  rectangle to be clipped or painted.
    //
    //  DisplayFormatControl          - Causes control characters such as the
    //                                  left-to-right mark to be shown in the
    //                                  output with a representative glyph.
    //
    //  NoFontFallback                - Disables fallback to alternate fonts for
    //                                  characters not supported in the requested
    //                                  font. Any missing characters will be
    //                                  be displayed with the fonts missing glyph,
    //                                  usually an open square.
    //
    //  NoWrap                        - Disables wrapping of text between lines
    //                                  when formatting within a rectangle.
    //                                  NoWrap is implied when a point is passed
    //                                  instead of a rectangle, or when the
    //                                  specified rectangle has a zero line length.
    //
    //  NoClip                        - By default text is clipped to the
    //                                  formatting rectangle. Setting NoClip
    //                                  allows overhanging pixels to affect the
    //                                  device outside the formatting rectangle.
    //                                  Pixels at the end of the line may be
    //                                  affected if the glyphs overhang their
    //                                  cells, and either the NoFitBlackBox flag
    //                                  has been set, or the glyph extends to far
    //                                  to be fitted.
    //                                  Pixels above/before the first line or
    //                                  below/after the last line may be affected
    //                                  if the glyphs extend beyond their cell
    //                                  ascent / descent. This can occur rarely
    //                                  with unusual diacritic mark combinations.

    //---------------------------------------------------------------------------

    public enum StringFormatFlags : uint
    {
        StringFormatFlagsDirectionRightToLeft = 0x00000001,
        StringFormatFlagsDirectionVertical = 0x00000002,
        StringFormatFlagsNoFitBlackBox = 0x00000004,
        StringFormatFlagsDisplayFormatControl = 0x00000020,
        StringFormatFlagsNoFontFallback = 0x00000400,
        StringFormatFlagsMeasureTrailingSpaces = 0x00000800,
        StringFormatFlagsNoWrap = 0x00001000,
        StringFormatFlagsLineLimit = 0x00002000,

        StringFormatFlagsNoClip = 0x00004000,
        StringFormatFlagsBypassGDI = 0x80000000
    };

    //---------------------------------------------------------------------------
    // StringTrimming
    //---------------------------------------------------------------------------

    public enum StringTrimming
    {
        StringTrimmingNone = 0,
        StringTrimmingCharacter = 1,
        StringTrimmingWord = 2,
        StringTrimmingEllipsisCharacter = 3,
        StringTrimmingEllipsisWord = 4,
        StringTrimmingEllipsisPath = 5
    };

    //---------------------------------------------------------------------------
    // National language digit substitution
    //---------------------------------------------------------------------------

    public enum StringDigitSubstitute
    {
        StringDigitSubstituteUser = 0,  // As NLS setting
        StringDigitSubstituteNone = 1,
        StringDigitSubstituteNational = 2,
        StringDigitSubstituteTraditional = 3
    };

    //---------------------------------------------------------------------------
    // Hotkey prefix interpretation
    //---------------------------------------------------------------------------

    public enum HotkeyPrefix
    {
        HotkeyPrefixNone = 0,
        HotkeyPrefixShow = 1,
        HotkeyPrefixHide = 2
    };

    //---------------------------------------------------------------------------
    // String alignment flags
    //---------------------------------------------------------------------------

    public enum StringAlignment
    {
        // Left edge for left-to-right text,
        // right for right-to-left text,
        // and top for vertical
        StringAlignmentNear = 0,
        StringAlignmentCenter = 1,
        StringAlignmentFar = 2
    };

    //---------------------------------------------------------------------------
    // DriverStringOptions
    //---------------------------------------------------------------------------

    public enum DriverStringOptions
    {
        DriverStringOptionsCmapLookup = 1,
        DriverStringOptionsVertical = 2,
        DriverStringOptionsRealizedAdvance = 4,
        DriverStringOptionsLimitSubpixel = 8
    };

    //---------------------------------------------------------------------------
    // Flush Intention flags
    //---------------------------------------------------------------------------

    public enum FlushIntention
    {
        FlushIntentionFlush = 0,        // Flush all batched rendering operations
        FlushIntentionSync = 1          // Flush all batched rendering operations
        // and wait for them to complete
    };

    //---------------------------------------------------------------------------
    // Image encoder parameter related types
    //---------------------------------------------------------------------------

    public enum EncoderParameterValueType
    {
        EncoderParameterValueTypeByte = 1,    // 8-bit unsigned int
        EncoderParameterValueTypeASCII = 2,    // 8-bit byte containing one 7-bit ASCII
        // code. NULL terminated.
        EncoderParameterValueTypeShort = 3,    // 16-bit unsigned int
        EncoderParameterValueTypeLong = 4,    // 32-bit unsigned int
        EncoderParameterValueTypeRational = 5,    // Two Longs. The first Long is the
        // numerator, the second Long expresses the
        // denomintor.
        EncoderParameterValueTypeLongRange = 6,    // Two longs which specify a range of
        // integer values. The first Long specifies
        // the lower end and the second one
        // specifies the higher end. All values
        // are inclusive at both ends
        EncoderParameterValueTypeUndefined = 7,    // 8-bit byte that can take any value
        // depending on field definition
        EncoderParameterValueTypeRationalRange = 8,    // Two Rationals. The first Rational
        // specifies the lower end and the second
        // specifies the higher end. All values
        // are inclusive at both ends
        //#if (GDIPVER >= 0x0110)
        //    EncoderParameterValueTypePointer        = 9     // a pointer to a parameter defined data.
        //#endif //(GDIPVER >= 0x0110)
    };

    //---------------------------------------------------------------------------
    // Image encoder value types
    //---------------------------------------------------------------------------

    public enum EncoderValue
    {
        EncoderValueColorTypeCMYK,
        EncoderValueColorTypeYCCK,
        EncoderValueCompressionLZW,
        EncoderValueCompressionCCITT3,
        EncoderValueCompressionCCITT4,
        EncoderValueCompressionRle,
        EncoderValueCompressionNone,
        EncoderValueScanMethodInterlaced,
        EncoderValueScanMethodNonInterlaced,
        EncoderValueVersionGif87,
        EncoderValueVersionGif89,
        EncoderValueRenderProgressive,
        EncoderValueRenderNonProgressive,
        EncoderValueTransformRotate90,
        EncoderValueTransformRotate180,
        EncoderValueTransformRotate270,
        EncoderValueTransformFlipHorizontal,
        EncoderValueTransformFlipVertical,
        EncoderValueMultiFrame,
        EncoderValueLastFrame,
        EncoderValueFlush,
        EncoderValueFrameDimensionTime,
        EncoderValueFrameDimensionResolution,
        EncoderValueFrameDimensionPage,
        //#if (GDIPVER >= 0x0110)
        //    EncoderValueColorTypeGray,
        //    EncoderValueColorTypeRGB,
        //#endif
    };

    public enum RotateFlipType
    {
        RotateNoneFlipNone = 0,
        Rotate90FlipNone = 1,
        Rotate180FlipNone = 2,
        Rotate270FlipNone = 3,

        RotateNoneFlipX = 4,
        Rotate90FlipX = 5,
        Rotate180FlipX = 6,
        Rotate270FlipX = 7,

        RotateNoneFlipY = Rotate180FlipX,
        Rotate90FlipY = Rotate270FlipX,
        Rotate180FlipY = RotateNoneFlipX,
        Rotate270FlipY = Rotate90FlipX,

        RotateNoneFlipXY = Rotate180FlipNone,
        Rotate90FlipXY = Rotate270FlipNone,
        Rotate180FlipXY = RotateNoneFlipNone,
        Rotate270FlipXY = Rotate90FlipNone
    };

    //----------------------------------------------------------------------------
    // Color Adjust Type
    //----------------------------------------------------------------------------

    public enum ColorAdjustType
    {
        ColorAdjustTypeDefault,
        ColorAdjustTypeBitmap,
        ColorAdjustTypeBrush,
        ColorAdjustTypePen,
        ColorAdjustTypeText,
        ColorAdjustTypeCount,
        ColorAdjustTypeAny      // Reserved
    };

    public enum GraphicsState: uint{}
}
