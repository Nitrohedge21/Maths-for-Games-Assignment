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
    [SerializeField] protected internal float Speed = 6f;
    [SerializeField] protected internal float jumpForce = 1.0f;
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
        
        //gameObject.transform.forward = move.Convert2UnityVector3();
        //this.GetComponent<MyTransform>().Position = gameObject.transform.forward;


        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            this.GetComponent<MyTransform>().Position.y += Mathf.Sqrt(jumpForce * -1.5f * gravityForce);
            isJumping = true;
        }
        else
        {
            isJumping = false;
            //Debug.Log("None of the inputs are being called");
        }

        RotateWhileMoving();
    }


    void RotateWhileMoving()
    {
        //This isn't the best way to go but I might as well keep it as it is.
        var inputValue = Input.inputString; //This switch case was taken from here: https://stackoverflow.com/a/57125512
        switch (inputValue)
        {
            case ("w"):
                this.GetComponent<MyTransform>().Rotation = new MyVector3(0.0f, 0.0f, 00.0f).Convert2UnityVector3();
                break;
            case ("a"):
                this.GetComponent<MyTransform>().Rotation = new MyVector3(0.0f, 0.0f, -90.0f).Convert2UnityVector3();
                break;
            case ("s"):
                this.GetComponent<MyTransform>().Rotation = new MyVector3(0.0f, 0.0f, 180.0f).Convert2UnityVector3();
                break;
            case ("d"):
                this.GetComponent<MyTransform>().Rotation = new MyVector3(0.0f, 0.0f, 90.0f).Convert2UnityVector3();
                break;
        }
    }
}