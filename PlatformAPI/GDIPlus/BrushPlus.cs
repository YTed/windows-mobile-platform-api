using System;
using System.Collections.Generic;
using System.Text;
using Color = System.Drawing.Color;

namespace PlatformAPI.GDIPlus
{
    public class BrushPlus: IDisposable
    {
        ~BrushPlus()
        {
            Dispose(true);
        }

        internal virtual BrushPlus Clone()
        {
            GpBrush brush = new GpBrush();

            SetStatus(NativeMethods.GdipCloneBrush(nativeBrush, out brush));

            BrushPlus newBrush = new BrushPlus(brush, lastResult);

            if (newBrush == null)
            {
                NativeMethods.GdipDeleteBrush(brush);
            }

            return newBrush;
        }

        BrushType Type
        {
            get
            {
                BrushType type = (BrushType)(-1);

                SetStatus(NativeMethods.GdipGetBrushType(nativeBrush, out type));

                return type;
            }
        }

        public GpStatus GetLastStatus()
        {
            GpStatus lastStatus = lastResult;
            lastResult = GpStatus.Ok;

            return lastStatus;
        }



        public BrushPlus()
        {
            SetStatus(GpStatus.NotImplemented);
        }




        public BrushPlus(GpBrush nativeBrush, GpStatus status)
        {
            lastResult = status;
            SetNativeBrush(nativeBrush);
        }

        public void SetNativeBrush(GpBrush nativeBrush)
        {
            this.nativeBrush = nativeBrush;
        }

        protected GpStatus SetStatus(GpStatus status)
        {
            if (status != GpStatus.Ok)
                return (lastResult = status);
            else
                return status;
        }

        internal GpBrush nativeBrush;
        protected GpStatus lastResult;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            // free native resources if there are any.
            if ((IntPtr)nativeBrush != IntPtr.Zero)
            {
                NativeMethods.GdipDeleteBrush(nativeBrush);
                nativeBrush = new GpBrush();
            }
        }
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class SolidBrushPlus : BrushPlus
    {
        public SolidBrushPlus()
        {
        }
        public SolidBrushPlus(Color color)
        {
            GpSolidFill brush;

            lastResult = NativeMethods.GdipCreateSolidFill(color.ToArgb(), out brush);

            SetNativeBrush(brush);
        }

        public GpStatus GetColor(out Color color)
        {
            int argb;

            SetStatus(NativeMethods.GdipGetSolidFillColor((GpSolidFill)nativeBrush,
                                                        out argb));

            color = Color.FromArgb(argb);

            return lastResult;
        }

        public GpStatus SetColor(Color color)
        {
            return SetStatus(NativeMethods.GdipSetSolidFillColor((GpSolidFill)nativeBrush,
                                                               color.ToArgb()));
        }


    }

    public class TextureBrushPlus : BrushPlus
    {
        public TextureBrushPlus() { }
        public TextureBrushPlus(ImagePlus image,
                     WrapMode wrapMode)
        {
            GpTexture texture;

            GpRectF rc; Unit unit;
            image.GetBounds(out rc, out unit);
            lastResult = NativeMethods.GdipCreateTextureIA(
                                                      image.nativeImage,
                                                      new GpImageAttributes(),
                                                      rc.X,
                                                      rc.Y,
                                                      rc.Width,
                                                      rc.Height,
                                                      out texture);

            SetNativeBrush(texture);
        }

        // When creating a texture brush from a metafile image, the dstRect
        // is used to specify the size that the metafile image should be
        // rendered at in the device units of the destination graphics.
        // It is NOT used to crop the metafile image, so only the width 
        // and height values matter for metafiles.

        TextureBrushPlus(ImagePlus image,
                     WrapMode wrapMode,
                     GpRectF dstRect)
        {
            GpTexture texture;

            lastResult = NativeMethods.GdipCreateTexture2(
                                                       image.nativeImage,
                                                       wrapMode,
                                                       dstRect.X,
                                                       dstRect.Y,
                                                       dstRect.Width,
                                                       dstRect.Height,
                                                       out texture);

            SetNativeBrush(texture);
        }

        public TextureBrushPlus(ImagePlus image,
                     GpRectF dstRect,
                     ImageAttributesPlus imageAttributes)
        {
            GpTexture texture;

            lastResult = NativeMethods.GdipCreateTextureIA(
                image.nativeImage,
                (imageAttributes != null) ? imageAttributes.nativeImageAttr : new GpImageAttributes(),
                dstRect.X,
                dstRect.Y,
                dstRect.Width,
                dstRect.Height,
                out texture
            );

            SetNativeBrush(texture);
        }

        TextureBrushPlus(ImagePlus image,
                     GpRect dstRect,
                     ImageAttributesPlus imageAttributes)
        {
            GpTexture texture;

            lastResult = NativeMethods.GdipCreateTextureIAI(
                image.nativeImage,
                (imageAttributes != null) ? imageAttributes.nativeImageAttr : new GpImageAttributes(),
                dstRect.X,
                dstRect.Y,
                dstRect.Width,
                dstRect.Height,
                out texture
            );

            SetNativeBrush(texture);
        }

        TextureBrushPlus(
            ImagePlus image,
            WrapMode wrapMode,

            GpRect dstRect
        )
        {
            GpTexture texture;

            lastResult = NativeMethods.GdipCreateTexture2I(
                                                        image.nativeImage,
                                                        wrapMode,
                                                        dstRect.X,
                                                        dstRect.Y,
                                                        dstRect.Width,
                                                        dstRect.Height,
                                                        out texture);

            SetNativeBrush(texture);
        }

        TextureBrushPlus(ImagePlus image,
                     WrapMode wrapMode,
                     float dstX,
                     float dstY,
                     float dstWidth,
                     float dstHeight)
        {
            GpTexture texture;

            lastResult = NativeMethods.GdipCreateTexture2(
                                                       image.nativeImage,
                                                       wrapMode,
                                                       dstX,
                                                       dstY,
                                                       dstWidth,
                                                       dstHeight,
                                                       out texture);

            SetNativeBrush(texture);
        }

        TextureBrushPlus(ImagePlus image,
                     WrapMode wrapMode,
                     int dstX,
                     int dstY,
                     int dstWidth,
                     int dstHeight)
        {
            GpTexture texture;

            lastResult = NativeMethods.GdipCreateTexture2I(
                                                        image.nativeImage,
                                                        wrapMode,
                                                        dstX,
                                                        dstY,
                                                        dstWidth,
                                                        dstHeight,
                                                        out texture);

            SetNativeBrush(texture);
        }
        GpStatus SetWrapMode(WrapMode wrapMode)
        {
            return SetStatus(NativeMethods.GdipSetTextureWrapMode((GpTexture)nativeBrush,
                                                                wrapMode));
        }

        WrapMode GetWrapMode()
        {
            WrapMode wrapMode;

            SetStatus(NativeMethods.GdipGetTextureWrapMode((GpTexture)nativeBrush,
                                                         out wrapMode));
            return wrapMode;
        }

        ImagePlus GetImage()
        {
            GpImage image;

            SetStatus(NativeMethods.GdipGetTextureImage((GpTexture)nativeBrush,
                                                      out image));

            ImagePlus retimage = new ImagePlus(image, lastResult);

            return retimage;
        }


    }

    public class LinearGradientBrush : BrushPlus
    {
        public LinearGradientBrush() { }
        public LinearGradientBrush(GpPointF point1,
                            GpPointF point2,
                            Color color1,
                            Color color2)
        {
            GpLineGradient brush;

            lastResult = NativeMethods.GdipCreateLineBrush(ref point1,
                                                         ref point2,
                                                         color1.ToArgb(),
                                                         color2.ToArgb(),
                                                         WrapMode.WrapModeTile,
                                                         out brush);

            SetNativeBrush(brush);
        }

        LinearGradientBrush(GpPoint point1,
                            GpPoint point2,
                            Color color1,
                            Color color2)
        {
            GpLineGradient brush;

            lastResult = NativeMethods.GdipCreateLineBrushI(ref point1,
                                                          ref point2,
                                                          color1.ToArgb(),
                                                          color2.ToArgb(),
                                                          WrapMode.WrapModeTile,
                                                          out brush);

            SetNativeBrush(brush);
        }

        public LinearGradientBrush(GpRectF rect,
                            Color color1,
                            Color color2,
                            LinearGradientMode mode)
        {
            GpLineGradient brush;

            lastResult = NativeMethods.GdipCreateLineBrushFromRect(ref rect,
                                                                 color1.ToArgb(),
                                                                 color2.ToArgb(),
                                                                 mode,
                                                                 WrapMode.WrapModeTile,
                                                                 out brush);

            SetNativeBrush(brush);
        }

        LinearGradientBrush(GpRect rect,
                            Color color1,
                            Color color2,
                            LinearGradientMode mode)
        {
            GpLineGradient brush;

            lastResult = NativeMethods.GdipCreateLineBrushFromRectI(ref rect,
                                                                  color1.ToArgb(),
                                                                  color2.ToArgb(),
                                                                  mode,
                                                                  WrapMode.WrapModeTile,
                                                                  out brush);

            SetNativeBrush(brush);
        }

        LinearGradientBrush(GpRectF rect,
                            Color color1,
                            Color color2,
                            float angle,
                            bool isAngleScalable)
        {
            GpLineGradient brush;

            lastResult = NativeMethods.GdipCreateLineBrushFromRectWithAngle(ref rect,
                                                                          color1.ToArgb(),
                                                                          color2.ToArgb(),
                                                                          angle,
                                                                          isAngleScalable,
                                                                          WrapMode.WrapModeTile,
                                                                          out brush);

            SetNativeBrush(brush);
        }

        LinearGradientBrush(GpRect rect,
                            Color color1,
                            Color color2,
                            float angle,
                            bool isAngleScalable)
        {
            GpLineGradient brush = new GpLineGradient();

            lastResult = NativeMethods.GdipCreateLineBrushFromRectWithAngleI(ref rect,
                                                                           color1.ToArgb(),
                                                                           color2.ToArgb(),
                                                                           angle,
                                                                           isAngleScalable,
                                                                           WrapMode.WrapModeTile,
                                                                           out brush);

            SetNativeBrush(brush);
        }

        GpStatus SetLinearColors(Color color1,
                               Color color2)
        {
            return SetStatus(NativeMethods.GdipSetLineColors((GpLineGradient)nativeBrush,
                                                           color1.ToArgb(),
                                                           color2.ToArgb()));
        }

        GpStatus GetLinearColors(Color[] colors)
        {
            int[] argb = new int[2];


            GpStatus status = SetStatus(NativeMethods.GdipGetLineColors((GpLineGradient)nativeBrush, argb));

            if (status == GpStatus.Ok)
            {
                // use bitwise copy operator for Color copy
                colors[0] = Color.FromArgb(argb[0]);
                colors[1] = Color.FromArgb(argb[1]);
            }

            return status;
        }

        GpStatus GetRectangle(out GpRectF rect)
        {
            return SetStatus(NativeMethods.GdipGetLineRect((GpLineGradient)nativeBrush, out rect));
        }

        GpStatus GetRectangle(out GpRect rect)
        {
            return SetStatus(NativeMethods.GdipGetLineRectI((GpLineGradient)nativeBrush, out rect));
        }

        GpStatus SetGammaCorrection(bool useGammaCorrection)
        {
            return SetStatus(NativeMethods.GdipSetLineGammaCorrection((GpLineGradient)nativeBrush,
                        useGammaCorrection));
        }

        bool GetGammaCorrection()
        {
            bool useGammaCorrection;

            SetStatus(NativeMethods.GdipGetLineGammaCorrection((GpLineGradient)nativeBrush,
                        out useGammaCorrection));

            return useGammaCorrection;
        }

        int GetBlendCount()
        {
            int count = 0;

            SetStatus(NativeMethods.GdipGetLineBlendCount((GpLineGradient)
                                                        nativeBrush,
                                                        out count));

            return count;
        }

        GpStatus SetBlend(float[] blendFactors,
                        float[] blendPositions)
        {
            return SetStatus(NativeMethods.GdipSetLineBlend((GpLineGradient)
                                                          nativeBrush,
                                                          blendFactors,
                                                          blendPositions,
                                                          blendFactors.Length));
        }

        GpStatus GetBlend(float[] blendFactors,
                        float[] blendPositions)
        {
            return SetStatus(NativeMethods.GdipGetLineBlend((GpLineGradient)nativeBrush,
                                                          blendFactors,
                                                          blendPositions,
                                                          blendFactors.Length));
        }

        int GetInterpolationColorCount()
        {
            int count = 0;

            SetStatus(NativeMethods.GdipGetLinePresetBlendCount((GpLineGradient)
                                                              nativeBrush,
                                                              out count));

            return count;
        }

        GpStatus SetInterpolationColors(Color[] presetColors,
                                      float[] blendPositions)
        {
            int count = presetColors.Length;
            int[] argbs = new int[count];

            for (int i = 0; i < count; i++)
            {
                argbs[i] = presetColors[i].ToArgb();
            }

            GpStatus status = SetStatus(NativeMethods.GdipSetLinePresetBlend(
                                                                        (GpLineGradient)nativeBrush,
                                                                        argbs,
                                                                        blendPositions,
                                                                        argbs.Length));
            return status;
        }

        GpStatus GetInterpolationColors(Color[] presetColors,
                                      float[] blendPositions)
        {
            int count = presetColors.Length;

            int[] argbs = new int[count];

            GpStatus status = SetStatus(NativeMethods.GdipGetLinePresetBlend((GpLineGradient)nativeBrush,
                                                                         argbs,
                                                                         blendPositions,
                                                                         argbs.Length));
            if (status == GpStatus.Ok)
            {
                for (int i = 0; i < count; i++)
                {
                    presetColors[i] = Color.FromArgb(argbs[i]);
                }
            }


            return status;
        }

        GpStatus SetBlendBellShape(float focus,
                                 float scale)
        {
            return SetStatus(NativeMethods.GdipSetLineSigmaBlend((GpLineGradient)nativeBrush, focus, scale));
        }

        GpStatus SetBlendTriangularShape(
            float focus,
            float scale)
        {
            return SetStatus(NativeMethods.GdipSetLineLinearBlend((GpLineGradient)nativeBrush, focus, scale));
        }
        GpStatus SetWrapMode(WrapMode wrapMode)
        {
            return SetStatus(NativeMethods.GdipSetLineWrapMode((GpLineGradient)nativeBrush,
                                                             wrapMode));
        }

        WrapMode GetWrapMode()
        {
            WrapMode wrapMode;

            SetStatus(NativeMethods.GdipGetLineWrapMode((GpLineGradient)
                                                      nativeBrush,
                                                      out wrapMode));

            return wrapMode;
        }
    }

    public class HatchBrush : BrushPlus
    {
        public HatchBrush(HatchStyle hatchStyle,
                   Color foreColor,
                   Color backColor)
        {

            GpHatch brush = new GpHatch();

            lastResult = NativeMethods.GdipCreateHatchBrush(hatchStyle,
                                                          foreColor.ToArgb(),
                                                          backColor.ToArgb(),
                                                          out brush);
            SetNativeBrush(brush);
        }

        HatchStyle GetHatchStyle()
        {
            HatchStyle hatchStyle;

            SetStatus(NativeMethods.GdipGetHatchStyle((GpHatch)nativeBrush,
                                                    out hatchStyle));

            return hatchStyle;
        }

        GpStatus GetForegroundColor(out Color color)
        {
            int argb;

            GpStatus status = SetStatus(NativeMethods.GdipGetHatchForegroundColor(
                                                            (GpHatch)nativeBrush,
                                                            out argb));

            color = Color.FromArgb(argb);

            return status;
        }

        GpStatus GetBackgroundColor(out Color color)
        {
            int argb;

            GpStatus status = SetStatus(NativeMethods.GdipGetHatchBackgroundColor(
                                                            (GpHatch)nativeBrush,
                                                            out argb));

            color = Color.FromArgb(argb);

            return status;
        }

    }

    public class PathGradientBrush : BrushPlus
    {
        public PathGradientBrush(
            GpPointF[] points,
            WrapMode wrapMode)
        {
            GpPathGradient brush = new GpPathGradient();

            lastResult = NativeMethods.GdipCreatePathGradient(
                                            points, points.Length,
                                            wrapMode, out brush);
            SetNativeBrush(brush);
        }

        public PathGradientBrush(
        GpPoint[] points,
        WrapMode wrapMode)
        {
            GpPathGradient brush = new GpPathGradient();

            lastResult = NativeMethods.GdipCreatePathGradientI(
                                            points, points.Length,
                                            wrapMode, out brush);

            SetNativeBrush(brush);
        }

        public PathGradientBrush(
        GraphicsPath path
        )
        {
            GpPathGradient brush = new GpPathGradient();

            lastResult = NativeMethods.GdipCreatePathGradientFromPath(
                                            path.nativePath, out brush);
            SetNativeBrush(brush);
        }

        public GpStatus GetCenterColor(out Color color)
        {
            int argb;

            SetStatus(NativeMethods.GdipGetPathGradientCenterColor(
                           (GpPathGradient)nativeBrush, out argb));

            color = Color.FromArgb(argb);

            return lastResult;
        }

        public GpStatus SetCenterColor(Color color)
        {
            SetStatus(NativeMethods.GdipSetPathGradientCenterColor(
                           (GpPathGradient)nativeBrush,
                           color.ToArgb()));

            return lastResult;
        }

        public int GetPointCount()
        {
            int count;

            SetStatus(NativeMethods.GdipGetPathGradientPointCount(
                           (GpPathGradient)nativeBrush, out count));

            return count;
        }

        public int GetSurroundColorCount()
        {
            int count;

            SetStatus(NativeMethods.GdipGetPathGradientSurroundColorCount(
                           (GpPathGradient)nativeBrush, out count));

            return count;
        }

        public GpStatus GetSurroundColors(Color[] colors, ref int count)
        {

            int count1;

            SetStatus(NativeMethods.GdipGetPathGradientSurroundColorCount(
                            (GpPathGradient)nativeBrush, out count1));

            if (lastResult != GpStatus.Ok)
                return lastResult;

            if ((count < count1) || (count1 <= 0))
                return SetStatus(GpStatus.InsufficientBuffer);

            int[] argbs = new int[count1];

            SetStatus(NativeMethods.GdipGetPathGradientSurroundColorsWithCount(
                        (GpPathGradient)nativeBrush, argbs, out count1));

            if (lastResult == GpStatus.Ok)
            {
                for (int i = 0; i < count1; i++)
                {
                    colors[i] = Color.FromArgb(argbs[i]);
                }
                count = count1;
            }

            return lastResult;
        }

        public GpStatus SetSurroundColors(Color[] colors,
                             ref int count)
        {
            int count1 = GetPointCount();

            if ((count > count1) || (count1 <= 0))
                return SetStatus(GpStatus.InvalidParameter);

            count1 = count;

            int[] argbs = new int[count1];

            for (int i = 0; i < count1; i++)
                argbs[i] = colors[i].ToArgb();

            SetStatus(NativeMethods.GdipSetPathGradientSurroundColorsWithCount(
                        (GpPathGradient)nativeBrush, argbs, ref count1));

            if (lastResult == GpStatus.Ok)
                count = count1;


            return lastResult;
        }

        public GpStatus GetGraphicsPath(out GraphicsPath path)
        {
            path = new GraphicsPath();
            return SetStatus(NativeMethods.GdipGetPathGradientPath(
                        (GpPathGradient)nativeBrush, out path.nativePath));
        }

        public GpStatus SetGraphicsPath(GraphicsPath path)
        {
            if (path == null)
                return SetStatus(GpStatus.InvalidParameter);

            return SetStatus(NativeMethods.GdipSetPathGradientPath(
                        (GpPathGradient)nativeBrush, path.nativePath));
        }

        public GpStatus GetCenterPoint(out GpPointF point)
        {
            return SetStatus(NativeMethods.GdipGetPathGradientCenterPoint(
                                    (GpPathGradient)nativeBrush,
                                    out point));
        }

        public GpStatus GetCenterPoint(out GpPoint point)
        {
            return SetStatus(NativeMethods.GdipGetPathGradientCenterPointI(
                                    (GpPathGradient)nativeBrush,
                                    out point));
        }

        public GpStatus SetCenterPoint(GpPointF point)
        {
            return SetStatus(NativeMethods.GdipSetPathGradientCenterPoint(
                                    (GpPathGradient)nativeBrush,
                                    ref point));
        }

        public GpStatus SetCenterPoint(GpPoint point)
        {
            return SetStatus(NativeMethods.GdipSetPathGradientCenterPointI(
                                    (GpPathGradient)nativeBrush,
                                    ref point));
        }

        public GpStatus GetRectangle(out GpRectF rect)
        {
            rect = new GpRectF();
            return SetStatus(NativeMethods.GdipGetPathGradientRect(
                                (GpPathGradient)nativeBrush, out rect));
        }

        public GpStatus GetRectangle(out GpRect rect)
        {
            rect = new GpRect();
            return SetStatus(NativeMethods.GdipGetPathGradientRectI(
                                (GpPathGradient)nativeBrush, out rect));
        }

        public GpStatus SetGammaCorrection(bool useGammaCorrection)
        {
            return SetStatus(NativeMethods.GdipSetPathGradientGammaCorrection(
                (GpPathGradient)nativeBrush, useGammaCorrection));
        }

        public bool GetGammaCorrection()
        {
            bool useGammaCorrection;

            SetStatus(NativeMethods.GdipGetPathGradientGammaCorrection(
                (GpPathGradient)nativeBrush, out useGammaCorrection));

            return useGammaCorrection;
        }

        public int GetBlendCount()
        {
            int count = 0;

            SetStatus(NativeMethods.GdipGetPathGradientBlendCount(
                                (GpPathGradient)nativeBrush, out count));

            return count;
        }

        public GpStatus GetBlend(float[] blendFactors,
                    float[] blendPositions)
        {
            return SetStatus(NativeMethods.GdipGetPathGradientBlend(
                                (GpPathGradient)nativeBrush,
                                blendFactors, blendPositions, blendFactors.Length));
        }

        public GpStatus SetBlend(float[] blendFactors,
                    float[] blendPositions)
        {
            return SetStatus(NativeMethods.GdipSetPathGradientBlend(
                                (GpPathGradient)nativeBrush,
                                blendFactors, blendPositions, blendFactors.Length));
        }

        public int GetInterpolationColorCount()
        {
            int count = 0;

            SetStatus(NativeMethods.GdipGetPathGradientPresetBlendCount(
                             (GpPathGradient)nativeBrush, out count));

            return count;
        }

        public GpStatus SetInterpolationColors(Color[] presetColors,
                                  float[] blendPositions)
        {

            int[] argbs = new int[presetColors.Length];
            for (int i = 0; i < argbs.Length; i++)
            {
                argbs[i] = presetColors[i].ToArgb();
            }

            GpStatus status = SetStatus(NativeMethods.
                               GdipSetPathGradientPresetBlend(
                                    (GpPathGradient)nativeBrush,
                                    argbs,
                                    blendPositions,
                                    argbs.Length));
            return status;
        }

        public GpStatus GetInterpolationColors(Color[] presetColors,
                                  float[] blendPositions)
        {
            int[] argbs = new int[presetColors.Length];

            GpStatus status = SetStatus(NativeMethods.GdipGetPathGradientPresetBlend(
                                    (GpPathGradient)nativeBrush,
                                    argbs,
                                    blendPositions,
                                    argbs.Length));

            for (int i = 0; i < presetColors.Length; i++)
            {
                presetColors[i] = Color.FromArgb(argbs[i]);
            }

            return status;
        }

        public GpStatus SetBlendBellShape(float focus,
                             float scale)
        {
            return SetStatus(NativeMethods.GdipSetPathGradientSigmaBlend(
                                (GpPathGradient)nativeBrush, focus, scale));
        }

        public GpStatus SetBlendTriangularShape(
        float focus,
        float scale
    )
        {
            return SetStatus(NativeMethods.GdipSetPathGradientLinearBlend(
                                (GpPathGradient)nativeBrush, focus, scale));
        }

        //GpStatus GetFocusScales(OUT float* xScale, 
        //                      OUT float* yScale) 
        //{
        //    return SetStatus(NativeMethods.GdipGetPathGradientFocusScales(
        //                        (GpPathGradient ) nativeBrush, xScale, yScale));
        //}

        //GpStatus SetFocusScales(float xScale,
        //                      float yScale)
        //{
        //    return SetStatus(NativeMethods.GdipSetPathGradientFocusScales(
        //                        (GpPathGradient ) nativeBrush, xScale, yScale));
        //}

        public WrapMode GetWrapMode()
        {
            WrapMode wrapMode;

            SetStatus(NativeMethods.GdipGetPathGradientWrapMode(
                         (GpPathGradient)nativeBrush, out wrapMode));

            return wrapMode;
        }

        public GpStatus SetWrapMode(WrapMode wrapMode)
        {
            return SetStatus(NativeMethods.GdipSetPathGradientWrapMode(
                                (GpPathGradient)nativeBrush, wrapMode));
        }




        public PathGradientBrush()
        {
        }
    }
}
