using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    //--------------------------------------------------------------------------
    // Represents a dimension in a 2D coordinate system (floating-point coordinates)
    //--------------------------------------------------------------------------

    public struct GpSizeF
    {
        public GpSizeF(GpSizeF size)
        {
            Width = size.Width;
            Height = size.Height;
        }

        public GpSizeF(float width,
              float height)
        {
            Width = width;
            Height = height;
        }

        public static GpSizeF operator +(GpSizeF sz1, GpSizeF sz2)
        {
            return new GpSizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        public static GpSizeF operator -(GpSizeF sz1, GpSizeF sz2)
        {
            return new GpSizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        public override bool Equals(object obj)
        {
            return (obj != null) &&
                (obj is GpSizeF) &&
                (Equals((GpSizeF)obj));
        }

        public bool Equals(GpSizeF sz)
        {
            return (Width == sz.Width) && (Height == sz.Height);
        }

        public bool Empty()
        {
            return (Width == 0.0f && Height == 0.0f);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public float Width;
        public float Height;
    }

    //--------------------------------------------------------------------------
    // Represents a dimension in a 2D coordinate system (integer coordinates)
    //--------------------------------------------------------------------------

    public struct GpSize
    {


        public GpSize(GpSize size)
        {
            Width = size.Width;
            Height = size.Height;
        }

        public GpSize(int width,
             int height)
        {
            Width = width;
            Height = height;
        }

        public static GpSize operator +(GpSize sz1, GpSize sz2)
        {
            return new GpSize(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        public static GpSize operator -(GpSize sz1, GpSize sz2)
        {
            return new GpSize(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        public override bool Equals(object obj)
        {
            return (obj != null) &&
                (obj is GpSize) &&
                Equals((GpSize)obj);
        }
        public bool Equals(GpSize sz)
        {
            return (Width == sz.Width) && (Height == sz.Height);
        }

        public bool Empty()
        {
            return (Width == 0 && Height == 0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int Width;
        public int Height;
    }

    //--------------------------------------------------------------------------
    // Represents a location in a 2D coordinate system (floating-point coordinates)
    //--------------------------------------------------------------------------

    public struct GpPointF
    {

        public GpPointF(GpPointF point)
        {
            X = point.X;
            Y = point.Y;
        }

        public GpPointF(GpSizeF size)
        {
            X = size.Width;
            Y = size.Height;
        }

        public GpPointF(float x,
               float y)
        {
            X = x;
            Y = y;
        }

        public static GpPointF operator +(GpPointF point1, GpPointF point2)
        {
            return new GpPointF(point1.X + point2.X,
                          point1.Y + point2.Y);
        }

        public static GpPointF operator -(GpPointF point1, GpPointF point2)
        {
            return new GpPointF(point1.X - point2.X,
                          point1.Y - point2.Y);
        }

        public override bool Equals(object obj)
        {
            return (obj != null) &&
                (obj is GpPointF) &&
                Equals((GpPointF)obj);
        }
        public bool Equals(GpPointF point)
        {
            return (X == point.X) && (Y == point.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public float X;
        public float Y;
    }

    //--------------------------------------------------------------------------
    // Represents a location in a 2D coordinate system (integer coordinates)
    //--------------------------------------------------------------------------

    public struct GpPoint
    {


        public GpPoint(GpPoint point)
        {
            X = point.X;
            Y = point.Y;
        }

        public GpPoint(GpSize size)
        {
            X = size.Width;
            Y = size.Height;
        }

        public GpPoint(int x,
              int y)
        {
            X = x;
            Y = y;
        }

        public static GpPoint operator +(GpPoint point1, GpPoint point2)
        {
            return new GpPoint(point1.X + point2.X,
                         point1.Y + point2.Y);
        }

        public static GpPoint operator -(GpPoint point1, GpPoint point2)
        {
            return new GpPoint(point1.X - point2.X,
                         point1.Y - point2.Y);
        }

        public override bool Equals(object obj)
        {
            return (obj != null) &&
                (obj is GpPoint) &&
                Equals((GpPoint)obj);
        }

        public bool Equals(GpPoint point)
        {
            return (X == point.X) && (Y == point.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int X;
        public int Y;
    }

    //--------------------------------------------------------------------------
    // Represents a rectangle in a 2D coordinate system (floating-point coordinates)
    //--------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential)]
    public struct GpRectF
    {

        public GpRectF(float x,
              float y,
              float width,
              float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public GpRectF(GpPointF location,
              GpSizeF size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public GpRectF Clone()
        {
            return new GpRectF(X, Y, Width, Height);
        }

        public void GetLocation(out GpPointF point)
        {
            point.X = X;
            point.Y = Y;
        }

        public void GetGpSize(out GpSizeF size)
        {
            size.Width = Width;
            size.Height = Height;
        }

        public void GetBounds(out GpRectF rect)
        {
            rect.X = X;
            rect.Y = Y;
            rect.Width = Width;
            rect.Height = Height;
        }

        public float GetLeft()
        {
            return X;
        }

        public float GetTop()
        {
            return Y;
        }

        public float GetRight()
        {
            return X + Width;
        }

        public float GetBottom()
        {
            return Y + Height;
        }

        public bool IsEmptyArea()
        {
            return (Width <= float.Epsilon) || (Height <= float.Epsilon);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is GpRectF))
                return false;
            return Equals((GpRectF)obj);
        }

        public bool Equals(GpRectF rect)
        {
            return X == rect.X &&
                   Y == rect.Y &&
                   Width == rect.Width &&
                   Height == rect.Height;
        }

        public bool Contains(float x,
                      float y)
        {
            return x >= X && x < X + Width &&
                   y >= Y && y < Y + Height;
        }

        public bool Contains(GpPointF pt)
        {
            return Contains(pt.X, pt.Y);
        }

        public bool Contains(GpRectF rect)
        {
            return (X <= rect.X) && (rect.GetRight() <= GetRight()) &&
                   (Y <= rect.Y) && (rect.GetBottom() <= GetBottom());
        }

        public void Inflate(float dx,
                     float dy)
        {
            X -= dx;
            Y -= dy;
            Width += 2 * dx;
            Height += 2 * dy;
        }

        public void Inflate(GpPointF point)
        {
            Inflate(point.X, point.Y);
        }

        public bool Intersect(GpRectF rect)
        {
            return Intersect(out this, this, rect);
        }

        static public bool Intersect(out GpRectF c,
                              GpRectF a,
                              GpRectF b)
        {
            float right = Math.Min(a.GetRight(), b.GetRight());
            float bottom = Math.Min(a.GetBottom(), b.GetBottom());
            float left = Math.Max(a.GetLeft(), b.GetLeft());
            float top = Math.Max(a.GetTop(), b.GetTop());

            c.X = left;
            c.Y = top;
            c.Width = right - left;
            c.Height = bottom - top;
            return !c.IsEmptyArea();
        }

        public bool IntersectsWith(GpRectF rect)
        {
            return (GetLeft() < rect.GetRight() &&
                    GetTop() < rect.GetBottom() &&
                    GetRight() > rect.GetLeft() &&
                    GetBottom() > rect.GetTop());
        }

        static public bool Union(out GpRectF c,
                          GpRectF a,
                          GpRectF b)
        {
            float right = Math.Max(a.GetRight(), b.GetRight());
            float bottom = Math.Max(a.GetBottom(), b.GetBottom());
            float left = Math.Min(a.GetLeft(), b.GetLeft());
            float top = Math.Min(a.GetTop(), b.GetTop());

            c.X = left;
            c.Y = top;
            c.Width = right - left;
            c.Height = bottom - top;
            return !c.IsEmptyArea();
        }

        public void Offset(GpPointF point)
        {
            Offset(point.X, point.Y);
        }

        public void Offset(float dx,
                    float dy)
        {
            X += dx;
            Y += dy;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public float X;
        public float Y;
        public float Width;
        public float Height;
    }

    //--------------------------------------------------------------------------
    // Represents a rectangle in a 2D coordinate system (integer coordinates)
    //--------------------------------------------------------------------------

    public struct GpRect
    {
        public GpRect(int x,
             int y,
             int width,
             int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public GpRect(GpPoint location,
             GpSize size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public GpRect Clone()
        {
            return new GpRect(X, Y, Width, Height);
        }

        public void GetLocation(out GpPoint point)
        {
            point.X = X;
            point.Y = Y;
        }

        public void GetGpSize(out GpSize size)
        {
            size.Width = Width;
            size.Height = Height;
        }

        public void GetBounds(out GpRect rect)
        {
            rect.X = X;
            rect.Y = Y;
            rect.Width = Width;
            rect.Height = Height;
        }

        public int GetLeft()
        {
            return X;
        }

        public int GetTop()
        {
            return Y;
        }

        public int GetRight()
        {
            return X + Width;
        }

        public int GetBottom()
        {
            return Y + Height;
        }

        public bool IsEmptyArea()
        {
            return (Width <= 0) || (Height <= 0);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is GpRect))
                return false;
            return Equals((GpRect)obj);
        }
        public bool Equals(GpRect rect)
        {
            return X == rect.X &&
                   Y == rect.Y &&
                   Width == rect.Width &&
                   Height == rect.Height;
        }

        public bool Contains(int x,
                      int y)
        {
            return x >= X && x < X + Width &&
                   y >= Y && y < Y + Height;
        }

        public bool Contains(GpPoint pt)
        {
            return Contains(pt.X, pt.Y);
        }

        public bool Contains(GpRect rect)
        {
            return (X <= rect.X) && (rect.GetRight() <= GetRight()) &&
                   (Y <= rect.Y) && (rect.GetBottom() <= GetBottom());
        }

        public void Inflate(int dx,
                     int dy)
        {
            X -= dx;
            Y -= dy;
            Width += 2 * dx;
            Height += 2 * dy;
        }

        public void Inflate(GpPoint point)
        {
            Inflate(point.X, point.Y);
        }

        public bool Intersect(GpRect rect)
        {
            return Intersect(out this, this, rect);
        }

        static public bool Intersect(out GpRect c,
                              GpRect a,
                              GpRect b)
        {
            int right = Math.Min(a.GetRight(), b.GetRight());
            int bottom = Math.Min(a.GetBottom(), b.GetBottom());
            int left = Math.Max(a.GetLeft(), b.GetLeft());
            int top = Math.Max(a.GetTop(), b.GetTop());

            c.X = left;
            c.Y = top;
            c.Width = right - left;
            c.Height = bottom - top;
            return !c.IsEmptyArea();
        }

        public bool IntersectsWith(GpRect rect)
        {
            return (GetLeft() < rect.GetRight() &&
                    GetTop() < rect.GetBottom() &&
                    GetRight() > rect.GetLeft() &&
                    GetBottom() > rect.GetTop());
        }

        static public bool Union(out GpRect c,
                          GpRect a,
                          GpRect b)
        {
            int right = Math.Max(a.GetRight(), b.GetRight());
            int bottom = Math.Max(a.GetBottom(), b.GetBottom());
            int left = Math.Min(a.GetLeft(), b.GetLeft());
            int top = Math.Min(a.GetTop(), b.GetTop());

            c.X = left;
            c.Y = top;
            c.Width = right - left;
            c.Height = bottom - top;
            return !c.IsEmptyArea();
        }

        public void Offset(GpPoint point)
        {
            Offset(point.X, point.Y);
        }

        public void Offset(int dx,
                    int dy)
        {
            X += dx;
            Y += dy;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int X;
        public int Y;
        public int Width;
        public int Height;
    }
}
