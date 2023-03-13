using UnityEngine;
public class Topic6Stuff : MonoBehaviour
{
    float t;
    public Mesh mesh;
    MyVector3[] ModelSpaceVertices;
    void Update()
    {
        //This script requires the transform component to be disabled
        t += Time.deltaTime * 0.5f;
        MyVector3[] TransformedVertices = new MyVector3[ModelSpaceVertices.Length];

        Quat q = new Quat(t, new MyVector3(0, 1, 0)); //This sets the yaw rotation, change it to desired one when needed
        //there is an issue with this script, it makes the object scale too for some reason we couldn't figure out with Jay.
        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            Quat K = new Quat(ModelSpaceVertices[i]);
            Quat newK = (q * K) * q.Inverse();
            MyVector3 newP = newK.GetAxis();
            TransformedVertices[i] = newP;
        }

        MeshFilter MF = GetComponent<MeshFilter>();
        MF.mesh.vertices = MyVector3.Convert2UnityArray(TransformedVertices);
        //These might be necessary depending on what's being done.
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();
    }

    private void Start()
    {
        MeshFilter MF = GetComponent<MeshFilter>();
        MF.mesh = Instantiate(mesh);
        ModelSpaceVertices = MyVector3.Convert2MyArray(MF.mesh.vertices);
    }
}

//This part could be used for planet rotation kinda thing






////Defining two rotations
//Quat q = new Quat(Mathf.PI * 0.5f, new MyVector3(0, 1, 0)); //This line affects the movement position
//Quat r = new Quat(Mathf.PI * 0.25f, new MyVector3(1, 0, 0));

////The slerped value
//Quat slerped = Quat.SLERP(q, r, t);

////Define a vector which we will rotate
//MyVector3 p = new MyVector3(1, 2, 3);

////Store that vector in a quaternion (an overloaded constructor is used here to store the raw position)
//Quat K = new Quat(p);

////newK will have our new position inside of it
//Quat newK = slerped * K * slerped.Inverse();

////Get the position as a vector
//MyVector3 newP = newK.GetAxis();

////Set the position so we can see if it's working
//transform.position = newP.Convert2UnityVector3();
////Ask Jay about using our own transform component to do something similar to the line above.