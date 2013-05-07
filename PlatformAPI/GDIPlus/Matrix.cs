using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public class Matrix
    {
    Matrix()
    {
        GpMatrix matrix;

        lastResult = NativeMethods.GdipCreateMatrix(out matrix);
    
        SetNativeMatrix(matrix);
    }

    Matrix(float m11, 
           float m12,
           float m21, 
           float m22,
           float dx, 
           float dy)
    {
        GpMatrix matrix;

        lastResult = NativeMethods.GdipCreateMatrix2(m11, m12, m21, m22, 
                                                      dx, dy, out matrix);
    
        SetNativeMatrix(matrix);
    }
    
    Matrix( GpRectF rect, 
            GpPointF[] dstplg)
    {
        GpMatrix matrix;

        lastResult = NativeMethods.GdipCreateMatrix3(rect, 
                                                   dstplg,
                                                   out matrix);

        SetNativeMatrix(matrix);
    }


    ~Matrix()
    {
        NativeMethods.GdipDeleteMatrix(nativeMatrix);
    }


    GpStatus GetElements(float[] m)  
    {
        return SetStatus(NativeMethods.GdipGetMatrixElements(nativeMatrix, m));
    }
    
    GpStatus SetElements(float m11, 
                       float m12, 
                       float m21, 
                       float m22, 
                       float dx, 
                       float dy)
    {
        return SetStatus(NativeMethods.GdipSetMatrixElements(nativeMatrix,
                            m11, m12, m21, m22, dx, dy));
    }

    float OffsetX() 
    {
        float[] elements = new float [6];

        if (GetElements(elements) == GpStatus.Ok)
            return elements[4];
        else 
            return 0.0f;
    }

    float OffsetY() 
    {
        float[] elements = new float [6];

        if (GetElements(elements) == GpStatus.Ok)
           return elements[5];
       else 
           return 0.0f;
    }

    GpStatus Reset()
    {
        // set identity matrix elements 
        return SetStatus(NativeMethods.GdipSetMatrixElements(nativeMatrix,
                                             1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f));
    }
/*
    Status Multiply( Matrix matrix, 
                    MatrixOrder order )
    {
        return SetStatus(NativeMethods.GdipMultiplyMatrix(nativeMatrix, 
                                          matrix.nativeMatrix,
                                          order));
    }

    Status Translate(float offsetX, 
                     float offsetY, 
                     MatrixOrder order )
    {
        return SetStatus(NativeMethods.GdipTranslateMatrix(nativeMatrix, offsetX,
                                                         offsetY, order));
    }

    Status Scale(float scaleX, 
                 float scaleY, 
                 MatrixOrder order )
    {
        return SetStatus(NativeMethods.GdipScaleMatrix(nativeMatrix, scaleX, 
                                                     scaleY, order));
    }

    Status Rotate(float angle, 
                  MatrixOrder order)
    {
        return SetStatus(NativeMethods.GdipRotateMatrix(nativeMatrix, angle, 
                                                      order));
    }
    
    Status RotateAt(float angle, 
                     PointF center, 
                    MatrixOrder order )
    {
        if(order == MatrixOrderPrepend)
        {
            SetStatus(NativeMethods.GdipTranslateMatrix(nativeMatrix, center.X,
                                                      center.Y, order));
            SetStatus(NativeMethods.GdipRotateMatrix(nativeMatrix, angle, 
                                                   order));
            return SetStatus(NativeMethods.GdipTranslateMatrix(nativeMatrix,
                                                             -center.X, 
                                                             -center.Y, 
                                                             order));
        }
        else
        {
            SetStatus(NativeMethods.GdipTranslateMatrix(nativeMatrix, 
                                                      - center.X, 
                                                      - center.Y, 
                                                      order));
            SetStatus(NativeMethods.GdipRotateMatrix(nativeMatrix, angle, 
                                                   order));
            return SetStatus(NativeMethods.GdipTranslateMatrix(nativeMatrix, 
                                                             center.X, 
                                                             center.Y, 
                                                             order));
        }
    }

    Status Shear(float shearX, 
                 float shearY,
                 MatrixOrder order = MatrixOrderPrepend)
    {
        return SetStatus(NativeMethods.GdipShearMatrix(nativeMatrix, shearX, 
                                                     shearY, order));
    }

    Status Invert()
    {
        return SetStatus(NativeMethods.GdipInvertMatrix(nativeMatrix));
    }

    // float version
    Status TransformPoints(PointF[] pts) 
    {
        return SetStatus(NativeMethods.GdipTransformMatrixPoints(nativeMatrix, 
                                                               pts, count));
    }
    
    Status TransformPoints(Point[] pts) 
    {
        return SetStatus(NativeMethods.GdipTransformMatrixPointsI(nativeMatrix, 
                                                                pts, 
                                                                count));
    }

    Status TransformVectors(OUT PointF* pts, 
                            INT count = 1) 
    { 
        return SetStatus(NativeMethods.GdipVectorTransformMatrixPoints(
                                        nativeMatrix, pts, count));
    }

    Status TransformVectors(OUT Point* pts, 
                            INT count = 1) 
    { 
       return SetStatus(NativeMethods.GdipVectorTransformMatrixPointsI(
                                        nativeMatrix, 
                                        pts, 
                                        count));
    }
    
    BOOL IsInvertible() 
    {
        BOOL result = FALSE;

        SetStatus(NativeMethods.GdipIsMatrixInvertible(nativeMatrix, &result));
    
        return result;
    }

    BOOL IsIdentity() 
    {
       BOOL result = FALSE;

       SetStatus(NativeMethods.GdipIsMatrixIdentity(nativeMatrix, &result));
    
       return result;
    }
        */
    bool Equals( Matrix matrix) 
    {
        float []points1 = new float[6], points2 = new float[6];
        GetElements(points1);
        matrix.GetElements(points2);
        for(int i = 0; i < 6; i++ )
            if ( points1[i] != points2[i] )
                return false;
        return true;
    }
    
    GpStatus GetLastStatus() 
    {
        GpStatus lastStatus = lastResult;
        lastResult = GpStatus.Ok;
 
        return lastStatus;
    }


    private Matrix( Matrix m)
    {
        float[] points = new float[6];
        m.GetElements(points);
        SetElements(points[0], points[2], points[3], points[4], points[5], points[6]);
    }


    protected Matrix(GpMatrix nativeMatrix)
    {
        lastResult = GpStatus.Ok;
        SetNativeMatrix(nativeMatrix);
    }
    
    void SetNativeMatrix(GpMatrix nativeMatrix)
    {
        this.nativeMatrix = nativeMatrix;
    }

    GpStatus SetStatus(GpStatus status) 
    {
        if (status != GpStatus.Ok)
            return (lastResult = status);
        else
            return status;
    }


    internal GpMatrix nativeMatrix;
        protected GpStatus lastResult;
    }
}
