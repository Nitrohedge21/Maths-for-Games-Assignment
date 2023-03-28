using UnityEngine;
using Cinemachine;
public class Movement : MonoBehaviour
{
    protected Transform Camera;
    protected CapsuleCollider Collider;
    [SerializeField] protected float Speed = 6f;
    protected float RotationRate = 10f;
    protected float JumpForce;
    protected bool GroundedPlayer;
    protected float JumpHeight = 1.0f;
    protected float GravityValue = -9.81f;
    protected AABB objectCollisionBox;
    protected MyVector3 Offset = new MyVector3(1f, 1f, 1f);
    protected MyVector3 gravityForce = new MyVector3(0f,-9.8f,0f);
    protected MyVector3 playerPosition;
    float capsuleRadius = 0.5f;
    float capsuleHeight = 2f;
    float capsuleHalfHeight;

    protected virtual void Start()
    {
        Collider = GetComponent<CapsuleCollider>();
        playerPosition = MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position);
        objectCollisionBox = new AABB(playerPosition - Offset, playerPosition + Offset);
        capsuleHalfHeight = capsuleHeight / 2;
        Camera.transform.position = playerPosition.Convert2UnityVector3();
    }
    
    protected virtual void FixedUpdate()
    {
        CharacterStuff();
        
        objectCollisionBox = new AABB(playerPosition - Offset, playerPosition + Offset);
        CollisionCheck();
    }
    protected virtual void Update()
    {
        Move();
    }
    void CharacterStuff()
    {
        this.GetComponent<CapsuleCollider>().center = playerPosition.Convert2UnityVector3();
        this.GetComponent<CapsuleCollider>().radius = Mathf.Max(this.GetComponent<MyTransform>().Scale.x, this.GetComponent<MyTransform>().Scale.z) * capsuleRadius;
        this.GetComponent<CapsuleCollider>().height = this.GetComponent<MyTransform>().Scale.y * capsuleHeight;
    }

    void CollisionCheck()
    {
        
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject floor in floors)
        {
            if (AABB.Intersects(objectCollisionBox, floor.GetComponent<GroundCollision>().collision))
            {
                AABB floorCollisionBox = floor.GetComponent<GroundCollision>().collision;
                playerPosition = new MyVector3(playerPosition.x, floorCollisionBox.Top + capsuleHalfHeight, playerPosition.z);
            }
        }

    }

     void Move()
    {
        //Figure out how to implement basic gravity without a rigidbody.

        if(Input.GetKey(KeyCode.W))
        {
            playerPosition.x += 1f * Speed * Time.deltaTime;
            Debug.Log("W is being called");
        }
        else if(Input.GetKey(KeyCode.S))
        {
            playerPosition.x += -1f * Speed * Time.deltaTime;
            Debug.Log("S is being called");
        }
        else
        {
            Debug.LogError("None of the inputs are being called");
        }
    }

}

//test
/*
 Velocity += gravityForce * Time.deltaTime;

        //GroundedPlayer = this.GetComponent<CharacterController>().isGrounded;

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
 */

//cut stuff
/*
     //this.GetComponent<CharacterController>().center = GetComponent<MyTransform>().Position;
     //this.GetComponent<CharacterController>().radius = Mathf.Max(GetComponent<MyTransform>().Scale.x, GetComponent<MyTransform>().Scale.z) * controllerRadius;
     //this.GetComponent<CharacterController>().height = GetComponent<MyTransform>().Scale.y * controllerHeight;
     //this.GetComponent<CharacterController>().skinWidth = 0.0001f;   //This one and minMoveDistance doesn't work for some fucking reason.
     //this.GetComponent<CharacterController>().minMoveDistance = 0f;
 */