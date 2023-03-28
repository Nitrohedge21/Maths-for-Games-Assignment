using UnityEngine;

public class MathsLibrary
{
    public static MyVector2 RadiansToVector(float angle)
    {
        MyVector2 rv = new MyVector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rv.x = Mathf.Cos(angle);
        rv.y = Mathf.Sin(angle);


        return rv;
    }
    public static float VectorToRadians(MyVector2 V)
    {
        float rv = 0.0f;

        rv = Mathf.Atan2(V.y, V.x);
        //There are two ways of doing this, Atan and Atan2. Apparently Atan2 does things for you so it's much easier.


        return rv;
    }
    //Pitch: X
    //Roll: Y
    //Yaw: Z
    public static MyVector3 EulerAnglesToDirection(MyVector3 EulerAngles)
    {
        MyVector3 rv = new MyVector3(0,0,0);
        rv.x = Mathf.Cos(EulerAngles.x * -1) * Mathf.Sin(EulerAngles.y);
        rv.y = Mathf.Sin(EulerAngles.x * -1);
        rv.z = Mathf.Cos(EulerAngles.y) * Mathf.Cos(EulerAngles.x * -1);

        return rv;
    }
    public static MyVector3 CrossProduct(MyVector3 a, MyVector3 b)
    {
        MyVector3 C = new MyVector3(0,0,0);

        C.x = ((a.y * b.z) - (a.z * b.y));
        C.y = ((a.z * b.x) - (a.x * b.z));
        C.z = ((a.x * b.y) - (a.y * b.x));

        return C;
    }

    public static MyVector3 RotateVertexAroundAxis(float Angle, MyVector3 v, MyVector3 N)
    {
        //V is Axis, N is Vertex
        //The Rodrigues Rotation Formula
        //Make sure the angle is in radians
        MyVector3 NPrime = N * Mathf.Cos(Angle) +
            MyVector3.DotProduct(N, v) * v * (1.0f - Mathf.Cos(Angle)) +
            CrossProduct(v, N) * Mathf.Sin(Angle);

        return NPrime;
    }

}

public class Matrix4by4
{
    public float[,] values;

    public Matrix4by4(MyVector4 column1, MyVector4 column2, MyVector4 column3, MyVector4 column4)
    {
        values = new float[4, 4];

        //Column1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        //Column2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        //Column3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        //Column4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;
    }
    public Matrix4by4(MyVector3 column1, MyVector3 column2, MyVector3 column3, MyVector3 column4)
    {
        values = new float[4, 4];

        //Column1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        //Column2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        //Column3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        //Column4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }

    public static Matrix4by4 Identity
    {
        get
        {
            return new Matrix4by4(
                new MyVector4(1, 0, 0, 0),
                new MyVector4(0, 1, 0, 0),
                new MyVector4(0, 0, 1, 0),
                new MyVector4(0, 0, 0, 1));
        }

    }

    public static MyVector4 operator *(Matrix4by4 lhs, MyVector4 rhs)
    {
        MyVector4 rv = new MyVector4(0,0,0,0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0,2] * rhs.z + lhs.values[0,3] * rhs.w;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * rhs.w;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * rhs.w;
        rv.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * rhs.w;

        return rv;
    }

    public static MyVector3 operator *(Matrix4by4 lhs, MyVector3 rhs)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z;

        return rv;
    }

    public static Vector3 operator *(Matrix4by4 lhs, Vector3 rhs)
    {
        Vector3 rv = new Vector3(0, 0, 0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z;

        return rv;
    }

    public static Matrix4by4 operator *(Matrix4by4 lhs, Matrix4by4 rhs)
    {
        Matrix4by4 rv = Identity;

        rv.values[0, 0] = (lhs.values[0, 0] * rhs.values[0, 0] + lhs.values[0, 1] * rhs.values[1, 0] + lhs.values[0, 2] * rhs.values[2, 0] + lhs.values[0, 3] * rhs.values[3, 0]);
        rv.values[1, 0] = (lhs.values[1, 0] * rhs.values[0, 0] + lhs.values[1, 1] * rhs.values[1, 0] + lhs.values[1, 2] * rhs.values[2, 0] + lhs.values[1, 3] * rhs.values[3, 0]);
        rv.values[2, 0] = (lhs.values[2, 0] * rhs.values[0, 0] + lhs.values[2, 1] * rhs.values[1, 0] + lhs.values[2, 2] * rhs.values[2, 0] + lhs.values[2, 3] * rhs.values[3, 0]);
        rv.values[3, 0] = (lhs.values[3, 0] * rhs.values[0, 0] + lhs.values[3, 1] * rhs.values[1, 0] + lhs.values[3, 2] * rhs.values[2, 0] + lhs.values[3, 3] * rhs.values[3, 0]);
        rv.values[0, 1] = (lhs.values[0, 0] * rhs.values[0, 1] + lhs.values[0, 1] * rhs.values[1, 1] + lhs.values[0, 2] * rhs.values[2, 1] + lhs.values[0, 3] * rhs.values[3, 1]);
        rv.values[1, 1] = (lhs.values[1, 0] * rhs.values[0, 1] + lhs.values[1, 1] * rhs.values[1, 1] + lhs.values[1, 2] * rhs.values[2, 1] + lhs.values[1, 3] * rhs.values[3, 1]);
        rv.values[2, 1] = (lhs.values[2, 0] * rhs.values[0, 1] + lhs.values[2, 1] * rhs.values[1, 1] + lhs.values[2, 2] * rhs.values[2, 1] + lhs.values[2, 3] * rhs.values[3, 1]);
        rv.values[3, 1] = (lhs.values[3, 0] * rhs.values[0, 1] + lhs.values[3, 1] * rhs.values[1, 1] + lhs.values[3, 2] * rhs.values[2, 1] + lhs.values[3, 3] * rhs.values[3, 1]);
        rv.values[0, 2] = (lhs.values[0, 0] * rhs.values[0, 2] + lhs.values[0, 1] * rhs.values[1, 2] + lhs.values[0, 2] * rhs.values[2, 2] + lhs.values[3, 3] * rhs.values[3, 2]);
        rv.values[1, 2] = (lhs.values[1, 0] * rhs.values[0, 2] + lhs.values[1, 1] * rhs.values[1, 2] + lhs.values[1, 2] * rhs.values[2, 2] + lhs.values[1, 3] * rhs.values[3, 2]);
        rv.values[2, 2] = (lhs.values[2, 0] * rhs.values[0, 2] + lhs.values[2, 1] * rhs.values[1, 2] + lhs.values[2, 2] * rhs.values[2, 2] + lhs.values[2, 3] * rhs.values[3, 2]);
        rv.values[3, 2] = (lhs.values[3, 0] * rhs.values[0, 2] + lhs.values[3, 1] * rhs.values[1, 2] + lhs.values[3, 2] * rhs.values[2, 2] + lhs.values[3, 3] * rhs.values[3, 2]);
        rv.values[0, 3] = (lhs.values[0, 0] * rhs.values[0, 3] + lhs.values[0, 1] * rhs.values[1, 3] + lhs.values[0, 2] * rhs.values[2, 3] + lhs.values[0, 3] * rhs.values[3, 3]);
        rv.values[1, 3] = (lhs.values[1, 0] * rhs.values[0, 3] + lhs.values[1, 1] * rhs.values[1, 3] + lhs.values[1, 2] * rhs.values[2, 3] + lhs.values[1, 3] * rhs.values[3, 3]);
        rv.values[2, 3] = (lhs.values[2, 0] * rhs.values[0, 3] + lhs.values[2, 1] * rhs.values[1, 3] + lhs.values[2, 2] * rhs.values[2, 3] + lhs.values[2, 3] * rhs.values[3, 3]);
        rv.values[3, 3] = (lhs.values[3, 0] * rhs.values[0, 3] + lhs.values[3, 1] * rhs.values[1, 3] + lhs.values[3, 2] * rhs.values[2, 3] + lhs.values[3, 3] * rhs.values[3, 3]);


        return rv;
    }

    public MyVector4 GetRow(int row)
    {
        return new MyVector4(
            values[0, row],
            values[1, row],
            values[2, row],
            values[3, row]);
    }
    //Literally found something about this in stackoverflow
    // https://stackoverflow.com/questions/55640065/c-sharp-getting-the-rows-of-matrices

    public MyVector4 GetColumn(int column)
    {
        return new MyVector4(
            values[column, 0],
            values[column, 1],
            values[column, 2],
            values[column, 3]);
    }

    public void SetColumn(int columnIndex, MyVector4 rv)
    {
        values[columnIndex, 0] = rv.x;
        values[columnIndex, 1] = rv.y;
        values[columnIndex, 2] = rv.z;
        values[columnIndex, 3] = rv.w;
    }

    public Matrix4by4 InvertTR()
    {
        Matrix4by4 rv = Identity;

        //Transpose the 3x3 part
        //transpose(r3)
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                rv.values[i, j] = values[j, i];

        //Sets the last column to -transpose(r3)*t
        rv.SetColumn(3, (rv * GetColumn(3)) * -1);
        return rv;

    }
    public Matrix4by4 TranslationInverse()
    {
        Matrix4by4 rv = Identity;
        rv.values[0, 3] = -values[0, 3];
        rv.values[1, 3] = -values[1, 3];
        rv.values[2, 3] = -values[2, 3];

        return rv;
    }
    public Matrix4by4 RotationInverse()
    {
        return new Matrix4by4(GetRow(0), GetRow(1), GetRow(2), GetRow(3));
    }

    public Matrix4by4 ScaleInverse()
    {
        Matrix4by4 rv = Identity;

        rv.values[0, 0] = 1.0f / values[0, 0];
        rv.values[1, 1] = 1.0f / values[1, 1];
        rv.values[2, 2] = 1.0f / values[2, 2];

        return rv;
    }
}

public class AABB
{

    public AABB(MyVector3 Min, MyVector3 Max)
    {
        MinExtent = Min;
        MaxExtent = Max;
    }

    //These could be Vector2s if I were to do 2D bound boxes.
    MyVector3 MinExtent;
    MyVector3 MaxExtent;

    public float Top
    {
        get { return MaxExtent.y; }
    }
    public float Bottom
    {
        get { return MinExtent.y; }

    }
    public float Left
    {
        get { return MinExtent.x; }
    }
    public float Right
    {
        get { return MaxExtent.x; }
    }
    public float Front
    {
        get { return MaxExtent.z; }
    }
    public float Back
    {
        get { return MinExtent.z; }
    }

    public static bool Intersects(AABB Box1, AABB Box2)
    {
        return !(Box2.Left > Box1.Right
            || Box2.Right < Box1.Left
            || Box2.Top < Box1.Bottom
            || Box2.Bottom > Box1.Top
            || Box2.Back > Box1.Front
            || Box2.Front < Box1.Back);
    }

    public static bool LineIntersection(AABB Box, MyVector3 StartPoint, MyVector3 EndPoint, out MyVector3 IntersectionPoint)
    {
        //Define the initial lowest and highest
        float Lowest = 0.0f;
        float Highest = 1.0f;

        //Default value for intersection point is needed
        IntersectionPoint = new MyVector3(0, 0, 0);

        //We do an intersection check on every axis(we're reusing the IntersectingAxis function)
        if (!IntersectingAxis(new MyVector3(1, 0, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        if (!IntersectingAxis(new MyVector3(0, 1, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        if (!IntersectingAxis(new MyVector3(0, 0, 1), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;

        //Calculate our intersection point through interpolation. 
        //There are several ways of doing this but this is apparently the convenient way since we learnt about it in the previous lecture.
        IntersectionPoint = MyVector3.Vect3Lerp(StartPoint, EndPoint, Lowest);

        return true;
    }
    public static bool IntersectingAxis(MyVector3 Axis, AABB Box, MyVector3 StartPoint, MyVector3 EndPoint,
        ref float Lowest, ref float Highest)
    {
        //Calculate our Minimum and Maximum based on the current axis
        float Minimum = 0.0f;
        float Maximum = 1.0f;

        if(Axis == new MyVector3(1, 0, 0))
        {
            Minimum = (Box.Left - StartPoint.x) / (EndPoint.x - StartPoint.x);
            Maximum = (Box.Right - StartPoint.x) / (EndPoint.x - StartPoint.x);
        }

        else if (Axis == new MyVector3(0, 1, 0))
        {
            Minimum = (Box.Bottom - StartPoint.y) / (EndPoint.y - StartPoint.y);
            Maximum = (Box.Top - StartPoint.y) / (EndPoint.y - StartPoint.y);
        }

        else if (Axis == new MyVector3(0, 0, 1))
        {
            Minimum = (Box.Back - StartPoint.z) / (EndPoint.z - StartPoint.z);
            Maximum = (Box.Front - StartPoint.z) / (EndPoint.z - StartPoint.z);
        }

        if(Maximum < Minimum)
        {
            //Swapping values
            float temp = Maximum;
            Maximum = Minimum;
            Minimum = temp;
        }

        //Eliminate non-intersections early
        if (Maximum < Lowest)
            return false;

        if (Minimum > Highest)
            return false;

        Lowest = Mathf.Max(Minimum, Lowest);
        Highest = Mathf.Min(Maximum, Highest);

        if (Lowest > Highest)
            return false;

        return true;
    }

}

public class Quat
{
    public float w;
    public MyVector3 v;
    //Got rid of x,y,z and made a new Vector3 instead. Check the topic 6 video to see why

    public Quat()
    {
        w = 0.0f;
        v = new MyVector3(0, 0, 0);
    }

    public Quat(float Angle, MyVector3 Axis)
    {
        float halfAngle = Angle / 2;
        w = Mathf.Cos(halfAngle);

        v = Axis * Mathf.Sin(halfAngle);
    }
    public Quat Inverse()
    {
        Quat rv = new Quat();
        rv.w = w;
        rv.SetAxis(-GetAxis());
        return rv;
    }
    public Quat(MyVector3 Position)
    {
        w = 0.0f;
        v = new MyVector3(Position.x, Position.y, Position.z);
    }

    public MyVector3 GetAxis()
    {
        return new MyVector3(v.x, v.y, v.z);
    }
    public MyVector3 SetAxis(MyVector3 rv)
    {
        rv = new MyVector3(0, 0, 0);

        v.x = rv.x;
        v.y = rv.y;
        v.z = rv.z;

        return v;
    }

    public MyVector4 GetAxisAngle()
    {
        MyVector4 rv = new MyVector4(0,0,0,0);

        //Inverse cosine to get our half angle back
        float halfAngle = Mathf.Acos(w);
        rv.w = halfAngle * 2; //This is literally the full angle

        //Calculates the normal axis using the half angle
        rv.x = v.x / Mathf.Sin(halfAngle);
        rv.y = v.y / Mathf.Sin(halfAngle);
        rv.z = v.z / Mathf.Sin(halfAngle);
        //The lines above do not use v in the slides but i'm assuming it's the vector's xyz.
        return rv;
    }

    public static Quat SLERP(Quat q, Quat r, float t)
    {
        t = Mathf.Clamp(t,0.0f,1.0f);

        Quat d = r * q.Inverse();
        MyVector4 AxisAngle = d.GetAxisAngle();
        Quat dT = new Quat(AxisAngle.w * t, new MyVector3(AxisAngle.x, AxisAngle.y, AxisAngle.z));

        return dT * q;
    }
    public static Quat operator *(Quat r, Quat s)
    {
        Quat RS = new Quat();

        //Do the formula
        RS.w = (r.w * s.w) - MyVector3.DotProduct(s.v, r.v,false);
        RS.v = (s.w * r.v) + (r.w * s.v) + MathsLibrary.CrossProduct(r.v, s.v);
        //Tried to use EularAngles for this but it was't able to convert (float, MyVector3) into MyVector3
        return RS;
    }

    public Quaternion Convert2UnityQuat()
    {
        Quaternion returnValue = new Quaternion();

        returnValue.w = w;
        returnValue.x = v.x;
        returnValue.y = v.y;
        returnValue.z = v.z;

        return returnValue;
    }

    public Quat Convert2MyQuat()
    {
        Quat returnValue = new Quat();

        w = returnValue.w;
        v = returnValue.v;

        return returnValue;
    }

    public Matrix4by4 Quat2Rotation()   //Converts a quaternion to rotation
    {
        Matrix4by4 rv = Matrix4by4.Identity;

        float xx = v.x * v.x;
        float xy = v.x * v.y;
        float xz = v.x * v.z;
        float xw = v.x * w;

        float yy = v.y * v.y;
        float yz = v.y * v.z;
        float yw = v.y * w;

        float zz = v.z * v.z;
        float zw = v.z * w;

        rv.values[0,0] = 1f - 2f * (yy + zz);
        rv.values[1,0] = 2 * (xy - zw);
        rv.values[2,0] = 2 * (xz + yw);

        rv.values[0,1] = 2 * (xy + zw);
        rv.values[1,1] = 1 - 2 * (xx + zz);
        rv.values[2,1] = 2 * (yz - xw);

        rv.values[0,2] = 2 * (xz - yw);
        rv.values[1,2] = 2 * (yz + xw);
        rv.values[2,2] = 1 - 2 * (xx + yy);

        rv.values[3,0] = rv.values[3,1] = rv.values[3,2] = rv.values[0,3] = rv.values[1,3] = rv.values[2,3] = 0;
        rv.values[3,3] = 1;

        return rv;
    }

}