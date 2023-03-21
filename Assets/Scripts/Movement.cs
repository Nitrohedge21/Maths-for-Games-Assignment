using UnityEngine;

public class Movement : MonoBehaviour
{
    protected CharacterController CharacterController;
    protected Transform Camera;
    protected CapsuleCollider Collider;
    [SerializeField] protected float Speed = 6f;
    protected float RotationRate = 10f;
    protected float JumpForce;
    protected MyVector3 Velocity = new MyVector3(0f,0f,0f);
    protected bool GroundedPlayer;
    protected float JumpHeight = 1.0f;
    protected float GravityValue = -9.81f;
    protected AABB collision;
    protected MyVector3 Offset = new MyVector3(5f, 5f, 5f);

    float capsuleRadius = 0.5f;
    float capsuleHeight = 2f;
    float controllerRadius = 8.5f;
    float controllerHeight = 2f;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Collider = GetComponent<CapsuleCollider>();
        collision = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
    }

    void Update()
    {
        CharacterStuff();

        GroundedPlayer = this.GetComponent<CharacterController>().isGrounded;
        if (GroundedPlayer && Velocity.y < 0)
        {
            Velocity.y = 0f;
        }

        MyVector3 Move = new MyVector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));    //These could be switched to GetAxis to make them more slippery.
        //The line above might be the reason why it the objects don't move, this most likely only works with normal vector3s.
        MyVector3 Direction = Move.NormalizeVector();

        //this.GetComponent<MyTransform>().Position += Direction.Convert2UnityVector3() * Time.deltaTime * Speed;

        if (Direction != new MyVector3(0, 0, 0))
        {
            Quaternion DesiredRotation = Quaternion.LookRotation(Direction.Convert2UnityVector3(), new MyVector3(0, 1, 0).Convert2UnityVector3());
            //Quat DesiredRotation = Quaternion.LookRotation(Direction.Convert2UnityVector3(), Vector3.up); //An attempt on using my own functions, it doesn't work.
            transform.rotation = Quaternion.Slerp(transform.rotation, DesiredRotation, RotationRate * Time.deltaTime);
            //transform.rotation = Quat.SLERP(transform.rotation, DesiredRotation, RotationRate * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && GroundedPlayer)
        {
            Velocity.y += Mathf.Sqrt(JumpHeight * -3.0f * GravityValue);
        }

        Velocity.y += GravityValue * Time.deltaTime;
        this.GetComponent<MyTransform>().Position += Velocity.Convert2UnityVector3() * Time.deltaTime;
        collision = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
        //You can also use the min and max extents


    }

    void CharacterStuff()
    {
        this.GetComponent<CapsuleCollider>().center = GetComponent<MyTransform>().Position;
        this.GetComponent<CapsuleCollider>().radius = Mathf.Max(GetComponent<MyTransform>().Scale.x, GetComponent<MyTransform>().Scale.z) * capsuleRadius;
        this.GetComponent<CapsuleCollider>().height = GetComponent<MyTransform>().Scale.y * capsuleHeight;

        //The lines below were added specifically for this project.
        this.GetComponent<CharacterController>().center = GetComponent<MyTransform>().Position;
        this.GetComponent<CharacterController>().radius = Mathf.Max(GetComponent<MyTransform>().Scale.x, GetComponent<MyTransform>().Scale.z) * controllerRadius;
        this.GetComponent<CharacterController>().height = GetComponent<MyTransform>().Scale.y * controllerHeight;
        this.GetComponent<CharacterController>().skinWidth = 0.0001f;   //This one and minMoveDistance doesn't work for some fucking reason.
        this.GetComponent<CharacterController>().minMoveDistance = 0f;
    }

    void CollisionCheck()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject floor in floors)
        {
            if (AABB.Intersects(collision, floor.GetComponent<GroundCollision>().collision))
            {
                //set the position to the floor level
                //you gotta check the top and bottom of the objects
            }
        }

    }
}