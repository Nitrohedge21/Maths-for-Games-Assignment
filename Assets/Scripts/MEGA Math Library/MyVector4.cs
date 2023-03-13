using UnityEngine;

public class MyVector4
{
    public float x, y, z, w;

    public MyVector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public MyVector4(Vector4 unityVector)
    {
        this.x = unityVector.x;
        this.y = unityVector.y;
        this.z = unityVector.z;
        this.w = unityVector.w;
    }

    public static MyVector4 Add(MyVector4 a, MyVector4 b)
    {
        MyVector4 returnValue = new MyVector4(0, 0, 0, 0);
        returnValue.x = a.x + b.x;
        returnValue.y = a.y + b.y;
        returnValue.z = a.z + b.z;
        returnValue.z = a.w + b.w;

        return returnValue;
    }

    public static MyVector4 Subtract(MyVector4 a, MyVector4 b)
    {
        MyVector4 returnValue = new MyVector4(0, 0, 0, 0);
        returnValue.x = a.x - b.x;
        returnValue.y = a.y - b.y;
        returnValue.z = a.z - b.z;
        returnValue.z = a.w - b.w;

        return returnValue;
    }

    public float Length()
    {
        //Gets the length of the vector.
        float returnValue = 0.0f;

        returnValue = Mathf.Sqrt(x * x + y * y + z * z + w * w);

        return returnValue;
    }

    public Vector4 Convert2UnityVector4()
    {
        Vector4 returnValue = new Vector4();
        //You could also put x,y,z inside the Vector3() function instead of doing the return lines below.
        returnValue.x = x;
        returnValue.y = y;
        returnValue.z = z;
        returnValue.w = w;

        return returnValue;
    }

    public MyVector4 Convert2MyVector4(Vector4 v)
    {
        MyVector4 returnValue = new MyVector4(0, 0, 0, 0);
        //Not entirely sure if this does what I intended
        returnValue.x = v.x;
        returnValue.y = v.y;
        returnValue.z = v.z;
        returnValue.w = v.w;

        return returnValue;
    }

    public float LengthSq()
    {
        //Gets the length of the vector3 squared.
        float returnValue = 0.0f;

        returnValue = x * x + y * y + z * z + w * w;

        return returnValue;
    }

    static MyVector4 ScaleVector(MyVector4 v, float scalar)
    {
        MyVector4 rv = new MyVector4(0, 0, 0, 0);

        rv.x = v.x * scalar;
        rv.y = v.y * scalar;
        rv.z = v.z * scalar;
        rv.w = v.w * scalar;

        return rv;
    }

    static MyVector4 DivideVector(MyVector4 v, float divisor)
    {
        MyVector4 rv = new MyVector4(0, 0, 0, 0);

        rv.x = v.x / divisor;
        rv.y = v.y / divisor;
        rv.z = v.z / divisor;
        rv.w = v.w / divisor;

        return rv;
    }

    public MyVector4 NormalizeVector()
    {
        MyVector4 rv = new MyVector4(0, 0, 0, 0);

        rv = DivideVector(this, this.Length());

        return rv;
    } 
    

    static float DotProduct(MyVector4 a, MyVector4 b, bool ShouldNormalize = true)
    {
        float rv = 0.0f;


        if (ShouldNormalize)
        {
            MyVector4 normalizedA = a.NormalizeVector();
            MyVector4 normalizedB = b.NormalizeVector();

            rv = (normalizedA.x * normalizedB.x) + (normalizedA.y * normalizedB.y) + (normalizedA.z * normalizedB.z) + (normalizedA.w * normalizedB.w);
        }
        else
        {
            rv = (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        return rv;
    }

    public static MyVector4 operator *(MyVector4 lhs, float rhs)
    {
        return ScaleVector(lhs, rhs);
    }
    public static MyVector4 operator /(MyVector4 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

    public static MyVector4 operator +(MyVector4 lhs, MyVector4 rhs)
    {
        return Add(lhs, rhs);
    }
    public static MyVector4 operator -(MyVector4 lhs, MyVector4 rhs)
    {
        return Subtract(lhs, rhs);
    }
}
