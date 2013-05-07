using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public class RegionPlus
    {
        RegionPlus()
        {
            GpRegion region = new GpRegion();

            lastResult = NativeMethods.GdipCreateRegion(out region);

            SetNativeRegion(region);
        }


        RegionPlus(GpRectF rect)
        {
            GpRegion region = new GpRegion();

            lastResult = NativeMethods.GdipCreateRegionRect(ref rect, out region);

            SetNativeRegion(region);
        }


        RegionPlus(GpRect rect)
        {
            GpRegion region = new GpRegion();

            lastResult = NativeMethods.GdipCreateRegionRectI(ref rect, out region);

            SetNativeRegion(region);
        }


        RegionPlus(GraphicsPath path)
        {
            GpRegion region = new GpRegion();

            lastResult = NativeMethods.GdipCreateRegionPath(path.nativePath, out region);

            SetNativeRegion(region);
        }


        RegionPlus(byte[] regionData)
        {
            GpRegion region = new GpRegion();

            lastResult = NativeMethods.GdipCreateRegionRgnData(regionData, regionData.Length,
                                                             out region);

            SetNativeRegion(region);
        }


        RegionPlus(HRGN hRgn)
        {
            GpRegion region = new GpRegion();

            lastResult = NativeMethods.GdipCreateRegionHrgn(hRgn, out region);

            SetNativeRegion(region);
        }


        RegionPlus FromHRGN(HRGN hRgn)
        {
            GpRegion region = new GpRegion();

            if (NativeMethods.GdipCreateRegionHrgn(hRgn, out region) == GpStatus.Ok)
            {
                RegionPlus newRegion = new RegionPlus(region);

                if (newRegion == null)
                {
                    NativeMethods.GdipDeleteRegion(region);
                }

                return newRegion;
            }
            else
                return null;
        }


        ~RegionPlus()
        {
            NativeMethods.GdipDeleteRegion(nativeRegion);
        }

        RegionPlus
       Clone()
        {
            GpRegion region = new GpRegion();

            SetStatus(NativeMethods.GdipCloneRegion(nativeRegion, out region));

            return new RegionPlus(region);
        }

        GpStatus
       MakeInfinite()
        {
            return SetStatus(NativeMethods.GdipSetInfinite(nativeRegion));
        }

        GpStatus
       MakeEmpty()
        {
            return SetStatus(NativeMethods.GdipSetEmpty(nativeRegion));
        }

        GpStatus
       Intersect(GpRectF rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRect(nativeRegion, ref rect,
                                                                CombineMode.CombineModeIntersect));
        }

        GpStatus
       Intersect(GpRect rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRectI(nativeRegion, ref rect,
                                                                CombineMode.CombineModeIntersect));
        }

        GpStatus
       Intersect(GraphicsPath path)
        {
            return SetStatus(NativeMethods.GdipCombineRegionPath(nativeRegion,
                                                               path.nativePath,
                                                               CombineMode.CombineModeIntersect));
        }

        GpStatus
       Intersect(RegionPlus region)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRegion(nativeRegion,
                                                                 region.nativeRegion,
                                                                 CombineMode.CombineModeIntersect));
        }

        GpStatus
       Union(GpRectF rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRect(nativeRegion, ref rect,
                                                               CombineMode.CombineModeUnion));
        }

        GpStatus
       Union(GpRect rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRectI(nativeRegion, ref rect,
                                                                CombineMode.CombineModeUnion));
        }

        GpStatus
       Union(GraphicsPath path)
        {
            return SetStatus(NativeMethods.GdipCombineRegionPath(nativeRegion,
                                                               path.nativePath,
                                                               CombineMode.CombineModeUnion));
        }

        GpStatus
       Union(RegionPlus region)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRegion(nativeRegion,
                                                                 region.nativeRegion,
                                                                 CombineMode.CombineModeUnion));
        }

        GpStatus
       Xor(GpRectF rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRect(nativeRegion, ref rect,
                                                               CombineMode.CombineModeXor));
        }

        GpStatus
       Xor(GpRect rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRectI(nativeRegion, ref rect,
                                                                CombineMode.CombineModeXor));
        }

        GpStatus
       Xor(GraphicsPath path)
        {
            return SetStatus(NativeMethods.GdipCombineRegionPath(nativeRegion,
                                                               path.nativePath,
                                                               CombineMode.CombineModeXor));
        }

        GpStatus
       Xor(RegionPlus region)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRegion(nativeRegion,
                                                                 region.nativeRegion,
                                                                 CombineMode.CombineModeXor));
        }

        GpStatus
       Exclude(GpRectF rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRect(nativeRegion, ref rect,
                                                               CombineMode.CombineModeExclude));
        }

        GpStatus
       Exclude(GpRect rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRectI(nativeRegion, ref rect,
                                                                CombineMode.CombineModeExclude));
        }

        GpStatus
       Exclude(GraphicsPath path)
        {
            return SetStatus(NativeMethods.GdipCombineRegionPath(nativeRegion,
                                                               path.nativePath,
                                                               CombineMode.CombineModeExclude));
        }

        GpStatus
       Exclude(RegionPlus region)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRegion(nativeRegion,
                                                       region.nativeRegion,
                                                                 CombineMode.CombineModeExclude));
        }

        GpStatus
       Complement(GpRectF rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRect(nativeRegion, ref rect,
                                                               CombineMode.CombineModeComplement));
        }

        GpStatus
       Complement(GpRect rect)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRectI(nativeRegion, ref rect,
                                                                CombineMode.CombineModeComplement));
        }

        GpStatus
       Complement(GraphicsPath path)
        {
            return SetStatus(NativeMethods.GdipCombineRegionPath(nativeRegion,
                                                        path.nativePath,
                                                        CombineMode.CombineModeComplement));
        }

        GpStatus
       Complement(RegionPlus region)
        {
            return SetStatus(NativeMethods.GdipCombineRegionRegion(nativeRegion,
                                                          region.nativeRegion,
                                                                 CombineMode.CombineModeComplement));
        }

        GpStatus
       Translate(float dx, float dy)
        {
            return SetStatus(NativeMethods.GdipTranslateRegion(nativeRegion, dx, dy));
        }

        GpStatus
       Translate(int dx,
                          int dy)
        {
            return SetStatus(NativeMethods.GdipTranslateRegionI(nativeRegion, dx, dy));
        }


        GpStatus
       GetBounds(out GpRectF rect,
                          GraphicsPlus g)
        {
            return SetStatus(NativeMethods.GdipGetRegionBounds(nativeRegion,
                                                        g.nativeGraphics,
                                                        out rect));
        }

        GpStatus
       GetBounds(out GpRect rect,
                          GraphicsPlus g)
        {
            return SetStatus(NativeMethods.GdipGetRegionBoundsI(nativeRegion,
                                                        g.nativeGraphics,
                                                        out rect));
        }

        HRGN
       GetHRGN(GraphicsPlus g)
        {
            HRGN hrgn;

            SetStatus(NativeMethods.GdipGetRegionHRgn(nativeRegion,
                                                    g.nativeGraphics,
                                                    out hrgn));

            return hrgn;
        }

        bool
       IsEmpty(GraphicsPlus g)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsEmptyRegion(nativeRegion,
                                                    g.nativeGraphics,
                                                    out booln));

            return booln;
        }

        bool
       IsInfinite(GraphicsPlus g)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsInfiniteRegion(nativeRegion,
                                                         g.nativeGraphics,
                                                         out booln));

            return booln;
        }

        bool
       Equals(RegionPlus region,
                       GraphicsPlus g)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsEqualRegion(nativeRegion,
                                                      region.nativeRegion,
                                                      g.nativeGraphics,
                                                      out booln));
            return booln;
        }

        // Get the size of the buffer needed for the GetData method
        uint
       GetDataSize()
        {
            uint bufferSize = 0;

            SetStatus(NativeMethods.GdipGetRegionDataSize(nativeRegion, out bufferSize));

            return bufferSize;
        }

        // buffer     - where to put the data
        // bufferSize - how big the buffer is (should be at least as big as GetDataSize())
        // sizeFilled - if not null, this is an OUT param that says how many bytes
        //              of data were written to the buffer.
        GpStatus
       GetData(byte[] buffer, out uint sizeFilled)
        {
            return SetStatus(NativeMethods.GdipGetRegionData(nativeRegion, buffer,
                                                           (uint)buffer.Length, out sizeFilled));
        }

        /**
         * Hit testing operations
         */
        bool
       IsVisible(GpPointF point,
                          GraphicsPlus g)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsVisibleRegionPoint(nativeRegion,
                                             point.X, point.Y,
                                             (g == null) ? new GpGraphics() : g.nativeGraphics,
                                             out booln));
            return booln;
        }

        bool
       IsVisible(GpRectF rect,
                          GraphicsPlus g)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsVisibleRegionRect(nativeRegion, rect.X,
                                                            rect.Y, rect.Width,
                                                            rect.Height,
                                                            (g == null) ?
                                                              new GpGraphics() : g.nativeGraphics,
                                                            out booln));
            return booln;
        }

        bool
       IsVisible(GpPoint point,
                          GraphicsPlus g)
        {
            bool booln = false;


            SetStatus(NativeMethods.GdipIsVisibleRegionPointI(nativeRegion,
                                                           point.X,
                                                           point.Y,
                                                           (g == null)
                                                            ? new GpGraphics() : g.nativeGraphics,
                                                           out booln));
            return booln;
        }

        bool
       IsVisible(GpRect rect,
                          GraphicsPlus g)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsVisibleRegionRectI(nativeRegion,
                                                          rect.X,
                                                          rect.Y,
                                                          rect.Width,
                                                          rect.Height,
                                                          (g == null)
                                                            ? new GpGraphics() : g.nativeGraphics,
                                                          out booln));
            return booln;
        }

        uint
       GetRegionScansCount(Matrix matrix)
        {
            uint count = 0;

            SetStatus(NativeMethods.GdipGetRegionScansCount(nativeRegion,
                                                          out count,
                                                          matrix.nativeMatrix));
            return count;
        }

        // If rects is null, return the count of rects in the region.
        // Otherwise, assume rects is big enough to hold all the region rects
        // and fill them in and return the number of rects filled in.
        // The rects are returned in the units specified by the matrix
        // (which is typically a world-to-device transform).
        // Note that the number of rects returned can vary, depending on the
        // matrix that is used.

        GpStatus
       GetRegionScans(
            Matrix matrix,
           GpRectF[] rects,
            out int count)
        {
            count = rects.Length;
            return SetStatus(NativeMethods.GdipGetRegionScans(nativeRegion,
                                                  rects,
                                                  ref count,
                                                  matrix.nativeMatrix));
        }

        GpStatus
       GetRegionScans(
            Matrix matrix,
           GpRect[] rects,
            out int count)
        {
            count = rects.Length;
            return SetStatus(NativeMethods.GdipGetRegionScansI(nativeRegion,
                                                  rects,
                                                  ref count,
                                                  matrix.nativeMatrix));
        }

        RegionPlus(GpRegion nativeRegion)
        {
            SetNativeRegion(nativeRegion);
        }

        void SetNativeRegion(GpRegion nativeRegion)
        {
            this.nativeRegion = nativeRegion;
        }

        GpStatus SetStatus(GpStatus stat)
        {
            lastResult = stat;
            return stat;
        }
        GpStatus GetLastStatus()
        {
            GpStatus lastStatus = lastResult;
            lastResult = GpStatus.Ok;

            return lastStatus;
        }
        internal GpRegion nativeRegion;
        GpStatus lastResult;
    }

}
