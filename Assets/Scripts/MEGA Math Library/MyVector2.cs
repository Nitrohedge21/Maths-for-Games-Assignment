using UnityEngine;

public class MyVector2
{
    public float x, y;

    public MyVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public MyVector2(Vector2 unityVector)
    {
        this.x = unityVector.x;
        this.y = unityVector.y;
    }

    public static MyVector2 Add(MyVector2 a, MyVector2 b)
    {
        MyVector2 returnValue = new MyVector2(0, 0);
        returnValue.x = a.x + b.x;
        returnValue.y = a.y + b.y;

        return returnValue;
    }

    public static MyVector2 Subtract(MyVector2 a, MyVector2 b)
    {
        MyVector2 returnValue = new MyVector2(0, 0);
        returnValue.x = a.x - b.x;
        returnValue.y = a.y - b.y;

        return returnValue;
    }

    public float Length()
    {
        //Gets the length of the vector.
        float returnValue = 0.0f;

        returnValue = Mathf.Sqrt(x * x + y * y);

        return returnValue;
    }

    public Vector3 Convert2UnityVector2()
    {
        Vector3 returnValue = new Vector3();
        //You could also put x,y,z inside the Vector3() function instead of doing the return lines below.
        returnValue.x = x;
        returnValue.y = y;

        return returnValue;
    }

    public float LengthSq()
    {
        //Gets the length of the vector3 squared.
        float returnValue = 0.0f;

        returnValue = x * x + y * y;

        return returnValue;
    }

    static MyVector2 ScaleVector(MyVector2 v, float scalar)
    {
        MyVector2 rv = new MyVector2(0, 0);

        rv.x = v.x * scalar;
        rv.y = v.y * scalar;

        return rv;
    }

    static MyVector2 DivideVector(MyVector2 v, float divisor)
    {
        MyVector2 rv = new MyVector2(0, 0);

        rv.x = v.x / divisor;
        rv.y = v.y / divisor;

        return rv;
    }

    public MyVector2 NormalizeVector()
    {
        MyVector2 rv = new MyVector2(0, 0);

        rv = DivideVector(this, this.Length());

        return rv;
    }

    static float DotProduct(MyVector2 a, MyVector2 b, bool ShouldNormalize = true)
    {
        float rv = 0.0f;


        if (ShouldNormalize)
        {
            MyVector2 normalizedA = a.NormalizeVector();
            MyVector2 normalizedB = b.NormalizeVector();

            rv = (normalizedA.x * normalizedB.x) + (normalizedA.y * normalizedB.y);
        }
        else
        {
            rv = (a.x * b.x) + (a.y * b.y);
        }


        return rv;
    }

    public static MyVector2 operator *(MyVector2 lhs, float rhs)
    {
        return ScaleVector(lhs, rhs);
    }
    public static MyVector2 operator /(MyVector2 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

    public static MyVector2 operator +(MyVector2 lhs, MyVector2 rhs)
    {
        return Add(lhs, rhs);
    }
    public static MyVector2 operator -(MyVector2 lhs, MyVector2 rhs)
    {
        return Subtract(lhs, rhs);
    }
}
