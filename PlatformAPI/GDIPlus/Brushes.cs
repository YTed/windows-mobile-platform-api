using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
        //----------------------------------------------------------------------------
        // Brush APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCloneBrush(GpBrush brush, out GpBrush cloneBrush);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipDeleteBrush(GpBrush brush);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipDeleteBrush(GpSolidFill brush);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetBrushType(GpBrush brush, out BrushType type);

        //----------------------------------------------------------------------------
        // HatchBrush APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateHatchBrush(HatchStyle hatchstyle, int forecol,
               int backcol, out GpHatch brush);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetHatchStyle(GpHatch brush, out HatchStyle hatchstyle);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetHatchForegroundColor(GpHatch brush, out int forecol);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetHatchBackgroundColor(GpHatch brush, out int backcol);

        //----------------------------------------------------------------------------
        // TextureBrush APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateTexture(GpImage image, WrapMode wrapmode,
           out GpTexture texture);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateTexture2(GpImage image, WrapMode wrapmode, float x,
    float y, float width, float height, out GpTexture texture);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateTextureIA(GpImage image,
     GpImageAttributes imageAttributes,
     float x, float y, float width, float height,
     out GpTexture texture);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateTexture2I(GpImage image, WrapMode wrapmode, int x,
     int y, int width, int height, out GpTexture texture);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateTextureIAI(GpImage image,
      GpImageAttributes imageAttributes,
      int x, int y, int width, int height,
      out GpTexture texture);


        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetTextureTransform(GpTexture brush, GpMatrix matrix);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSetTextureTransform(GpTexture brush, GpMatrix matrix);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipResetTextureTransform(GpTexture brush);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipMultiplyTextureTransform(GpTexture brush, GpMatrix matrix,
             MatrixOrder order);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipTranslateTextureTransform(GpTexture brush, float dx, float dy,
             MatrixOrder order);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipScaleTextureTransform(GpTexture brush, float sx, float sy,
             MatrixOrder order);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipRotateTextureTransform(GpTexture brush, float angle, MatrixOrder order);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSetTextureWrapMode(GpTexture brush, WrapMode wrapmode);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetTextureWrapMode(GpTexture brush, out WrapMode wrapmode);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetTextureImage(GpTexture brush, out GpImage image);

        //----------------------------------------------------------------------------
        // SolidBrush APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateSolidFill(int color, out GpSolidFill brush);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSetSolidFillColor(GpSolidFill brush, int color);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetSolidFillColor(GpSolidFill brush, out int color);

//----------------------------------------------------------------------------
// LineBrush APIs
//----------------------------------------------------------------------------

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateLineBrush(ref GpPointF point1,
                    ref GpPointF point2,
                    int color1, int color2,
                    WrapMode wrapMode,
                    out GpLineGradient lineGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateLineBrushI(ref GpPoint point1,
                     ref GpPoint point2,
                     int color1, int color2,
                     WrapMode wrapMode,
                     out GpLineGradient lineGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateLineBrushFromRect(ref GpRectF rect,
                            int color1, int color2,
                            LinearGradientMode mode,
                            WrapMode wrapMode,
                            out GpLineGradient lineGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateLineBrushFromRectI(ref GpRect rect,
                             int color1, int color2,
                             LinearGradientMode mode,
                             WrapMode wrapMode,
                             out GpLineGradient lineGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateLineBrushFromRectWithAngle(ref GpRectF rect,
                                     int color1, int color2,
                                     float angle,
                                     bool isAngleScalable,
                                     WrapMode wrapMode,
                                     out GpLineGradient lineGradient);

[DllImport("gdiplus")] public static extern GpStatus
GdipCreateLineBrushFromRectWithAngleI(ref GpRect rect,
                                     int color1, int color2,
                                     float angle,
                                     bool isAngleScalable,
                                     WrapMode wrapMode,
                                     out GpLineGradient lineGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLineColors(GpLineGradient brush, int color1, int color2);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineColors(GpLineGradient brush, int[] colors);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineRect(GpLineGradient brush, out GpRectF rect);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineRectI(GpLineGradient brush, out GpRect rect);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLineGammaCorrection(GpLineGradient brush, bool useGammaCorrection);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineGammaCorrection(GpLineGradient brush, out bool useGammaCorrection);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineBlendCount(GpLineGradient brush, out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineBlend(GpLineGradient brush, float[] blendfactors, float[] positions,
                 int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLineBlend(GpLineGradient brush, float[] blend,
                 float[] positions, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLinePresetBlendCount(GpLineGradient brush, out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLinePresetBlend(GpLineGradient brush, int[] blend,
                                           float[] positions, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLinePresetBlend(GpLineGradient brush, int[] blend,
                       float[] positions, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLineSigmaBlend(GpLineGradient brush, float focus, float scale);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLineLinearBlend(GpLineGradient brush, float focus, float scale);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLineWrapMode(GpLineGradient brush, WrapMode wrapmode);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineWrapMode(GpLineGradient brush, out WrapMode wrapmode);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetLineTransform(GpLineGradient brush, out GpMatrix matrix);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetLineTransform(GpLineGradient brush, GpMatrix matrix);

[DllImport("gdiplus")] public static extern GpStatus 
GdipResetLineTransform(GpLineGradient brush);

[DllImport("gdiplus")] public static extern GpStatus 
GdipMultiplyLineTransform(GpLineGradient brush, GpMatrix matrix,
                            MatrixOrder order);

[DllImport("gdiplus")] public static extern GpStatus 
GdipTranslateLineTransform(GpLineGradient brush, float dx, float dy,
                            MatrixOrder order);

[DllImport("gdiplus")] public static extern GpStatus 
GdipScaleLineTransform(GpLineGradient brush, float sx, float sy,
                            MatrixOrder order);

[DllImport("gdiplus")] public static extern GpStatus 
GdipRotateLineTransform(GpLineGradient brush, float angle, 
                        MatrixOrder order);

//----------------------------------------------------------------------------
// PathGradientBrush APIs
//----------------------------------------------------------------------------

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreatePathGradient(GpPointF[] points,
                                    int count,
                                    WrapMode wrapMode,
                                    out GpPathGradient polyGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreatePathGradientI(GpPoint[] points,
                                    int count,
                                    WrapMode wrapMode,
                                    out GpPathGradient polyGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreatePathGradientFromPath(GpPath path,
                                    out GpPathGradient polyGradient);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientCenterColor(
                        GpPathGradient brush, out int color);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientCenterColor(
                        GpPathGradient brush, int colors);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientSurroundColorsWithCount(
                        GpPathGradient brush, int[] color, out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientSurroundColorsWithCount(
                        GpPathGradient brush,
                        int[] color, ref int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientPath(GpPathGradient brush, out GpPath path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientPath(GpPathGradient brush, GpPath path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientCenterPoint(
                        GpPathGradient brush, out GpPointF points);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientCenterPointI(
                        GpPathGradient brush, out GpPoint points);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientCenterPoint(
                        GpPathGradient brush, ref GpPointF point);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientCenterPointI(
                        GpPathGradient brush, ref GpPoint point);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientRect(GpPathGradient brush, out GpRectF rect);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientRectI(GpPathGradient brush, out GpRect rect);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientPointCount(GpPathGradient brush, out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientSurroundColorCount(GpPathGradient brush, out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientGammaCorrection(GpPathGradient brush, 
                                   bool useGammaCorrection);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientGammaCorrection(GpPathGradient brush, 
                                   out bool useGammaCorrection);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientBlendCount(GpPathGradient brush,
                                             out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientBlend(GpPathGradient brush,
                                    float[] blend, float[] positions, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientBlend(GpPathGradient brush,
                float[] blend, float[] positions, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientPresetBlendCount(GpPathGradient brush, out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientPresetBlend(GpPathGradient brush, int[] blend,
                                                float[] positions, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientPresetBlend(GpPathGradient brush, int[] blend,
                                        float[] positions, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientSigmaBlend(GpPathGradient brush, float focus, float scale);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientLinearBlend(GpPathGradient brush, float focus, float scale);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientWrapMode(GpPathGradient brush,
                                         out WrapMode wrapmode);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientWrapMode(GpPathGradient brush,
                                         WrapMode wrapmode);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientTransform(GpPathGradient brush,
                                          out GpMatrix matrix);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientTransform(GpPathGradient brush,
                                          GpMatrix matrix);

[DllImport("gdiplus")] public static extern GpStatus 
GdipResetPathGradientTransform(GpPathGradient brush);

[DllImport("gdiplus")] public static extern GpStatus 
GdipMultiplyPathGradientTransform(GpPathGradient brush, 
                                  GpMatrix matrix,
                                  MatrixOrder order);

[DllImport("gdiplus")] public static extern GpStatus 
GdipTranslatePathGradientTransform(GpPathGradient brush, float dx, float dy,
                                   MatrixOrder order);

[DllImport("gdiplus")] public static extern GpStatus 
GdipScalePathGradientTransform(GpPathGradient brush, float sx, float sy,
                               MatrixOrder order);

[DllImport("gdiplus")] public static extern GpStatus 
GdipRotatePathGradientTransform(GpPathGradient brush, float angle,
                                MatrixOrder order);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathGradientFocusScales(GpPathGradient brush, out float xScale, 
                               out float yScale);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathGradientFocusScales(GpPathGradient brush, float xScale, 
                               float yScale);

    }
}
