using UnityEngine;
public class Movement : MonoBehaviour
{
    //Required Stuff
    protected Transform Camera;
    protected CapsuleCollider Collider;
    [HideInInspector] public AABB playerCollision;
    protected MyVector3 Offset = new MyVector3(1f, 1f, 1f);
    protected float gravityForce = -9.81f;
    float capsuleRadius = 0.5f;
    float capsuleHeight = 2f;
    float capsuleHalfHeight;
    //float rotationRate = 2.0f;
    //Editable Stuff
    [SerializeField] protected  internal float Speed = 6f;
    [SerializeField] protected float jumpForce = 1.0f;
    public bool isJumping = false;
    public bool isControllable = false;
    public bool isGrounded;

    protected virtual void Start()
    {
        Camera = GameObject.Find("Main Camera").transform;
        Collider = GetComponent<CapsuleCollider>();
        playerCollision = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
        capsuleHalfHeight = capsuleHeight / 2;
        Camera.transform.position = this.GetComponent<MyTransform>().Position;
    }
    
    protected virtual void FixedUpdate()
    {
        CharacterStuff();
        
        playerCollision = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
        GroundCollisionCheck();
       // ObjectCollisionCheck(); //This function is gonna be kept for a while as I can't figure out how to fix the other script.
    }
    protected virtual void Update()
    {
        if(isControllable == true)
        {
            Move();
        }
    }
    void CharacterStuff()
    {
        this.GetComponent<CapsuleCollider>().center = this.GetComponent<MyTransform>().Position;
        this.GetComponent<CapsuleCollider>().radius = Mathf.Max(this.GetComponent<MyTransform>().Scale.x, this.GetComponent<MyTransform>().Scale.z) * capsuleRadius;
        this.GetComponent<CapsuleCollider>().height = this.GetComponent<MyTransform>().Scale.y * capsuleHeight;
    }

    void GroundCollisionCheck()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject floor in floors)
        {
            AABB floorCollisionBox = floor.GetComponent<GroundCollision>().collision;
            if (AABB.Intersects(playerCollision, floorCollisionBox))
            {
                this.GetComponent<MyTransform>().Position = new MyVector3(this.GetComponent<MyTransform>().Position.x, floorCollisionBox.Top + capsuleHalfHeight, this.GetComponent<MyTransform>().Position.z).Convert2UnityVector3();
                //This line only makes the colliding object go on top of the other one.
                isGrounded = true;
            }
            else { isGrounded = false; };
        }
    }


     void Move()
     {
        if (isJumping == false && isGrounded == false) { this.GetComponent<MyTransform>().Position.y += gravityForce * Time.deltaTime; }    //Applies gravity while it's not jumping and it's not grounded
        MyVector3 move = new MyVector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        this.GetComponent<MyTransform>().Position += move.Convert2UnityVector3() * Speed * Time.deltaTime;
        //if (move != new MyVector3(0, 0, 0)){  }  //Either make an implicit operator or comment this line out


        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            this.GetComponent<MyTransform>().Position.y += Mathf.Sqrt(jumpForce * -3.0f * gravityForce);
            isJumping = true;
        }
        else
        {
            isJumping = false;
            //Debug.Log("None of the inputs are being called");
        }
     }

}