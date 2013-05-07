using System;
using System.Collections.Generic;
using System.Text;
using Color = System.Drawing.Color;

namespace PlatformAPI.GDIPlus
{
    public class ImageAttributesPlus
    {
        ImageAttributesPlus()
        {
            nativeImageAttr = new GpImageAttributes();
            lastResult = NativeMethods.GdipCreateImageAttributes(out nativeImageAttr);
        }

        ~ImageAttributesPlus()
        {
            NativeMethods.GdipDisposeImageAttributes(nativeImageAttr);
        }

        ImageAttributesPlus Clone()
        {
            GpImageAttributes clone;

            SetStatus(NativeMethods.GdipCloneImageAttributes(
                                                nativeImageAttr,
                                                out clone));

            return new ImageAttributesPlus(clone, lastResult);
        }

        GpStatus
        SetToIdentity(
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipSetImageAttributesToIdentity(
                                                nativeImageAttr,
                                                type));
        }

        GpStatus
        Reset(
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipResetImageAttributes(
                                                nativeImageAttr,
                                                type));
        }

        //Status
        //SetColorMatrix(
        //     ColorMatrix colorMatrix,
        //    ColorMatrixFlags mode = ColorMatrixFlagsDefault,
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesColorMatrix(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        true,
        //                                        colorMatrix,
        //                                        null,
        //                                        mode));
        //}

        //Status ClearColorMatrix(
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesColorMatrix(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        false,
        //                                        null,
        //                                        null,
        //                                        ColorMatrixFlagsDefault));
        //}

        //Status
        //SetColorMatrices(
        //     ColorMatrix *colorMatrix,
        //     ColorMatrix *grayMatrix,
        //    ColorMatrixFlags mode = ColorMatrixFlagsDefault,
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesColorMatrix(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        true,
        //                                        colorMatrix,
        //                                        grayMatrix,
        //                                        mode));
        //}

        //Status ClearColorMatrices(
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesColorMatrix(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        false,
        //                                        null,
        //                                        null,
        //                                        ColorMatrixFlagsDefault));
        //}

        //Status SetThreshold(
        //    float threshold,
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesThreshold(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        true,
        //                                        threshold));
        //}

        //Status ClearThreshold(
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesThreshold(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        false,
        //                                        0.0));
        //}

        GpStatus SetGamma(
            float gamma,
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipSetImageAttributesGamma(
                                                nativeImageAttr,
                                                type,
                                                true,
                                                gamma));
        }

        GpStatus ClearGamma(
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipSetImageAttributesGamma(
                                                nativeImageAttr,
                                                type,
                                                false,
                                                0.0f));
        }

        GpStatus SetNoOp(
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipSetImageAttributesNoOp(
                                                nativeImageAttr,
                                                type,
                                                true));
        }

        GpStatus ClearNoOp(
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipSetImageAttributesNoOp(
                                                nativeImageAttr,
                                                type,
                                                false));
        }

        GpStatus SetColorKey(
             Color colorLow,
             Color colorHigh,
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipSetImageAttributesColorKeys(
                                                nativeImageAttr,
                                                type,
                                                true,
                                                colorLow.ToArgb(),
                                                colorHigh.ToArgb()));
        }

        GpStatus ClearColorKey(
            ColorAdjustType type
            )
        {
            return SetStatus(NativeMethods.GdipSetImageAttributesColorKeys(
                                                nativeImageAttr,
                                                type,
                                                false,
                                                0,
                                                0));
        }

        //Status SetOutputChannel(
        //    ColorChannelFlags channelFlags,
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesOutputChannel(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        true,
        //                                        channelFlags));
        //}

        //Status ClearOutputChannel(
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesOutputChannel(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        false,
        //                                        ColorChannelFlagsLast));
        //}

        //Status SetOutputChannelColorProfile(
        //     WCHAR *colorProfileFilename,
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesOutputChannelColorProfile(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        true,
        //                                        colorProfileFilename));
        //}

        //Status ClearOutputChannelColorProfile(
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesOutputChannelColorProfile(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        false,
        //                                        null));
        //}

        //Status SetRemapTable(
        //    UINT mapSize, 
        //     ColorMap *map,
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesRemapTable(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        true,
        //                                        mapSize,
        //                                        map));
        //}

        //Status ClearRemapTable(
        //    ColorAdjustType type = ColorAdjustTypeDefault
        //    )
        //{
        //    return SetStatus(NativeMethods.GdipSetImageAttributesRemapTable(
        //                                        nativeImageAttr,
        //                                        type,
        //                                        false,
        //                                        0,
        //                                        null));
        //}

        //Status SetBrushRemapTable(UINT mapSize, 
        //                           ColorMap *map)
        //{
        //    return this.SetRemapTable(mapSize, map, ColorAdjustTypeBrush);
        //}

        //Status ClearBrushRemapTable()
        //{
        //    return this.ClearRemapTable(ColorAdjustTypeBrush);
        //}

        //Status SetWrapMode(WrapMode wrap, 
        //                    Color& color = Color(), 
        //                   BOOL clamp = false) 
        //{
        //    ARGB argb = color.ToArgb();

        //    return SetStatus(NativeMethods.GdipSetImageAttributesWrapMode(
        //                       nativeImageAttr, wrap, argb, clamp));
        //}

        //// The flags of the palette are ignored.

        //Status GetAdjustedPalette(OUT ColorPalette* colorPalette,
        //                          ColorAdjustType colorAdjustType)  
        //{
        //    return SetStatus(NativeMethods.GdipGetImageAttributesAdjustedPalette(
        //                       nativeImageAttr, colorPalette, colorAdjustType));
        //}

        GpStatus GetLastStatus()
        {
            GpStatus lastStatus = lastResult;
            lastResult = GpStatus.Ok;

            return lastStatus;
        }



        internal ImageAttributesPlus(GpImageAttributes imageAttr, GpStatus status)
        {
            SetNativeImageAttr(imageAttr);
            lastResult = status;
        }

        internal void SetNativeImageAttr(GpImageAttributes nativeImageAttr)
        {
            this.nativeImageAttr = nativeImageAttr;
        }

        GpStatus SetStatus(GpStatus status)
        {
            if (status != GpStatus.Ok)
                return (lastResult = status);
            else
                return status;
        }


        internal GpImageAttributes nativeImageAttr;
        private GpStatus lastResult;
    }
}
