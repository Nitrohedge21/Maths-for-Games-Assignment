using UnityEngine;


public class MyVector3
{
    public float x, y, z;
    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public MyVector3(Vector3 unityVector)
    {
        this.x = unityVector.x;
        this.y = unityVector.y;
        this.z = unityVector.z;
    }

    public static MyVector3 Add(MyVector3 a, MyVector3 b)
    {
        MyVector3 returnValue = new MyVector3(0, 0, 0);
        returnValue.x = a.x + b.x;
        returnValue.y = a.y + b.y;
        returnValue.z = a.z + b.z;

        return returnValue;
    }

    public static MyVector3 Subtract(MyVector3 a, MyVector3 b)
    {
        MyVector3 returnValue = new MyVector3(0, 0, 0);
        returnValue.x = a.x - b.x;
        returnValue.y = a.y - b.y;
        returnValue.z = a.z - b.z;

        return returnValue;
    }

    public float Length()
    {
        //Gets the length of the vector.
        float returnValue = 0.0f;

        returnValue = Mathf.Sqrt(x * x + y * y + z * z);

        return returnValue;
    }

    public Vector3 Convert2UnityVector3()
    {
        Vector3 returnValue = new Vector3();
        //You could also put x,y,z inside the Vector3() function instead of doing the return lines below.
        returnValue.x = x;
        returnValue.y = y;
        returnValue.z = z;

        return returnValue;
    } 
    public static MyVector3 Convert2MyVector3(Vector3 v)
    {
        MyVector3 returnValue = new MyVector3(0,0,0);

        returnValue.x = v.x;
        returnValue.y = v.y;
        returnValue.z = v.z;

        return returnValue;
    }

    public static MyVector3[] Convert2MyArray(Vector3[] a)
    {
        MyVector3[] returnArray = new MyVector3[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            returnArray[i] = new MyVector3(a[i]);
        }
        return returnArray;
    }

    public static Vector3[] Convert2UnityArray(MyVector3[] a)
    {
        Vector3[] returnArray = new Vector3[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            returnArray[i] = new Vector3(a[i].x, a[i].y, a[i].z);
        }
        return returnArray;
    }

    public float LengthSq()
    {
        //Gets the length of the vector3 squared.
        float returnValue = 0.0f;

        returnValue = x * x + y * y + z * z;

        return returnValue;
    }

    static MyVector3 ScaleVector(MyVector3 v, float scalar)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v.x * scalar;
        rv.y = v.y * scalar;
        rv.z = v.z * scalar;

        return rv;
    }

    static MyVector3 DivideVector(MyVector3 v, float divisor)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v.x / divisor;
        rv.y = v.y / divisor;
        rv.z = v.z / divisor;

        return rv;
    }

    public MyVector3 NormalizeVector()
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv = DivideVector(this, this.Length());

        return rv;
    }

    public static float DotProduct(MyVector3 a, MyVector3 b, bool ShouldNormalize = true)
    {
        float rv = 0.0f;


        if(ShouldNormalize)
        {
            MyVector3 normalizedA = a.NormalizeVector();
            MyVector3 normalizedB = b.NormalizeVector();

            rv = (normalizedA.x * normalizedB.x) + (normalizedA.y * normalizedB.y) + (normalizedA.z * normalizedB.z);
        }
        else
        {
            rv = (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }
        

        return rv;
    }

    //Not sure if the method below was supposed to be created for all the vectors.
    public static MyVector3 Vect3Lerp(MyVector3 A, MyVector3 B, float t)
    {
        return A * (1.0f - t) + B * t;
    }

    public static MyVector3 operator*(MyVector3 lhs, float rhs)
    {
        return ScaleVector(lhs, rhs);
    }
    public static MyVector3 operator *(float lhs, MyVector3 rhs)
    {
        return ScaleVector(rhs, lhs);
    }

    public static MyVector3 operator/(MyVector3 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

    public static MyVector3 operator+(MyVector3 lhs, MyVector3 rhs)
    {
        return Add(lhs, rhs);
    }
    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs)
    {
        return Subtract(lhs, rhs);
    }
    //[IMPORTANT] This might not be right!! //
    public static MyVector3 operator -(MyVector3 a)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = -a.x;
        rv.y = -a.y;
        rv.z = -a.z;

        return rv;
    }

    public static implicit operator MyVector3(MyVector4 v)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v.x;
        rv.y = v.y;
        rv.z = v.z;

        return rv;

    }

    

}
