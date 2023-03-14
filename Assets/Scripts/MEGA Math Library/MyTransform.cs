using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class MyTransform : MonoBehaviour
{
    MyVector3[] ModelSpaceVertices;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale = new Vector3(1.0f, 1.0f, 1.0f);
    public Mesh mesh;
    public Matrix4by4 scaleMatrix = Matrix4by4.Identity;
    public Matrix4by4 translationMatrix = Matrix4by4.Identity;
    public Matrix4by4 R = Matrix4by4.Identity;
    public Matrix4by4 M = Matrix4by4.Identity;
    float capsuleRadius = 0.5f;
    float capsuleHeight = 2f;
    float controllerRadius = 8.5f;
    float controllerHeight = 2f;
    void OnValidate()
    {
        if(!Application.isPlaying)
        {
            MeshFilter MF = GetComponent<MeshFilter>();
            MF.sharedMesh = Instantiate(mesh);  //This line calls SendMessage function for some reason and it's causing the warning to appear
            ModelSpaceVertices = MyVector3.Convert2MyArray(MF.sharedMesh.vertices);
        }
        // https://forum.unity.com/threads/sendmessage-cannot-be-called-during-awake-checkconsistency-or-onvalidate-can-we-suppress.537265/
        //Not sure why or how this works but i'll take it
        //Thanks to curio124 for the suggestion
        
    }
    private void Start()
    {
        MeshFilter MF = GetComponent<MeshFilter>();
        MF.sharedMesh = Instantiate(mesh);
        ModelSpaceVertices = MyVector3.Convert2MyArray(MF.sharedMesh.vertices);
    }

    void Update()
    {
        //Define a new array with the correct size
        MyVector3[] TransformedVertices = new MyVector3[ModelSpaceVertices.Length];

        //Create our translation matrix
        scaleMatrix = new Matrix4by4(
            new MyVector3(1, 0, 0) * Scale.x,
            new MyVector3(0, 1, 0) * Scale.y,
            new MyVector3(0, 0, 1) * Scale.z,
            new MyVector3(0, 0, 0));

         translationMatrix = new Matrix4by4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, 1, 0),
            new MyVector3(0, 0, 1),
            new MyVector3(Position.x, Position.y, Position.z));


        Matrix4by4 rollMatrix = new Matrix4by4(
            new MyVector3(Mathf.Cos(Rotation.y), Mathf.Sin(Rotation.y), 0),
            new MyVector3(-Mathf.Sin(Rotation.y), Mathf.Cos(Rotation.y), 0),
            new MyVector3(0, 0, 1),
            new MyVector3(0, 0, 0));

        Matrix4by4 pitchMatrix = new Matrix4by4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, Mathf.Cos(Rotation.x), Mathf.Sin(Rotation.x)),
            new MyVector3(0, -Mathf.Sin(Rotation.x), Mathf.Cos(Rotation.x)),
            new MyVector3(0, 0, 0));

        Matrix4by4 yawMatrix = new Matrix4by4(
            new MyVector3(Mathf.Cos(Rotation.z), 0, -Mathf.Sin(Rotation.z)),
            new MyVector3(0, 1, 0),
            new MyVector3(Mathf.Sin(Rotation.z), 0, Mathf.Cos(Rotation.z)),
            new MyVector3(0, 0, 0));

        Quat q = new Quat(Rotation.y, new MyVector3(0, 1, 0));
        //R = q.Quat2Rotation(); //This one rotates the object using the quaternions
        R = yawMatrix * (pitchMatrix * rollMatrix);  //Rotation Matrix
        M = translationMatrix * (R * scaleMatrix);    //This is the combination of all the matrices, check old version for seperate editing of verts.
        //Transform each individual vertex, the part that effects the mesh
        for (int i = 0; i < TransformedVertices.Length; i++)
        {

            TransformedVertices[i] = M * new MyVector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1);

            this.GetComponent<CapsuleCollider>().center = Position;
            this.GetComponent<CapsuleCollider>().radius = Mathf.Max(Scale.x, Scale.z) * capsuleRadius;
            this.GetComponent<CapsuleCollider>().height = Scale.y * capsuleHeight;

            //The lines below were added specifically for this project.
            this.GetComponent<CharacterController>().center = Position;
            this.GetComponent<CharacterController>().radius = Mathf.Max(Scale.x, Scale.z) * controllerRadius;
            this.GetComponent<CharacterController>().height = Scale.y * controllerHeight;
            this.GetComponent<CharacterController>().skinWidth = 0.0001f;   //This one and minMoveDistance doesn't work for some fucking reason.
            this.GetComponent<CharacterController>().minMoveDistance = 0f;

        }
        MeshFilter MF = GetComponent<MeshFilter>();
        MF.sharedMesh.vertices = MyVector3.Convert2UnityArray(TransformedVertices);

        //These might be necessary depending on what's being done.
        MF.sharedMesh.RecalculateNormals();
        MF.sharedMesh.RecalculateBounds();
    }
}
