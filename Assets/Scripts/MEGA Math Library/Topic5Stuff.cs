using UnityEngine;

public class Topic5Stuff : MonoBehaviour
{
    void Start()
    {
       
    }
    void Update()
    {
        MyTransform myTransform = GetComponent<MyTransform>();

        //We Define some AABB for this GameObject
        AABB theBox = new AABB(new MyVector3(0, 0, 0), new MyVector3(3, 3, 3));

        //Define a start and end point
        MyVector3 LineStart = new MyVector3(-2, -2, -2);
        MyVector3 LineEnd = new MyVector3(3, 4, 5);

        // We need to transform these global start and end positions into local space, we must define an InverseM
        Matrix4by4 InverseM = myTransform.scaleMatrix.ScaleInverse() * (myTransform.R.RotationInverse() * myTransform.translationMatrix.TranslationInverse());

        MyVector3 LocalStart = InverseM * LineStart;
        MyVector3 LocalEnd = InverseM * LineEnd;

        //Perform the intersection.
        MyVector3 i; // [IMPORTANT!] This is called i on the slides but i had to rename mine to -i
        if (AABB.LineIntersection(theBox, LineStart, LineEnd, out i))
        {
            Debug.Log("Intersecting! Intersection point: " + i);
            Debug.Log("Global Intersection point: " + (myTransform.M * i));
        }
        else
        {
            Debug.Log("Did not intersect! i is uninitialised, so don't do anything with it!");
        }


    }
}


