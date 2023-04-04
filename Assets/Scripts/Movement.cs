using UnityEngine;
public class Movement : MonoBehaviour
{
    //Required Stuff
    protected Transform Camera;
    protected CapsuleCollider Collider;
    protected AABB objectCollisionBox;
    protected MyVector3 Offset = new MyVector3(1f, 1f, 1f);
    protected float gravityForce = -9.81f;
    float capsuleRadius = 0.5f;
    float capsuleHeight = 2f;
    float capsuleHalfHeight;
    float rotationRate = 2.0f;
    //Editable Stuff
    [SerializeField] protected  internal float Speed = 6f;
    [SerializeField] protected float jumpForce = 10.0f;
    public bool isJumping = false;
    public bool isControllable = false;
    public bool isGrounded;

    protected virtual void Start()
    {
        Collider = GetComponent<CapsuleCollider>();
        objectCollisionBox = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
        capsuleHalfHeight = capsuleHeight / 2;
        Camera.transform.position = this.GetComponent<MyTransform>().Position;
    }
    
    protected virtual void FixedUpdate()
    {
        CharacterStuff();
        
        objectCollisionBox = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
        CollisionCheck();
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

    void CollisionCheck()
    {
        
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject floor in floors)
        {
            if (AABB.Intersects(objectCollisionBox, floor.GetComponent<GroundCollision>().collision))
            {
                AABB floorCollisionBox = floor.GetComponent<GroundCollision>().collision;
                this.GetComponent<MyTransform>().Position = new MyVector3(this.GetComponent<MyTransform>().Position.x, floorCollisionBox.Top + capsuleHalfHeight, this.GetComponent<MyTransform>().Position.z).Convert2UnityVector3();
                //This line only makes the colliding object go on top of the other one.
                isGrounded = true;
            }
            else { isGrounded = false; };
        }

    }

     void Move()
    {
        //Figure out how to use multiple input commands at once.
        if(isJumping == false && isGrounded == false)   //This if statement has to be changed as well
        {
            this.GetComponent<MyTransform>().Position.y += gravityForce * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.W))
        {
            this.GetComponent<MyTransform>().Position.x += 1f * Speed * Time.deltaTime;
            this.GetComponent<MyTransform>().Rotation = (new MyVector3(0f, 0f, 180f) / rotationRate).Convert2UnityVector3();
            Debug.Log("Moving Forward");
        }
        else if(Input.GetKey(KeyCode.S))
        {
            this.GetComponent<MyTransform>().Position.x += -1f * Speed * Time.deltaTime;
            this.GetComponent<MyTransform>().Rotation = (new MyVector3(0f, 0f, -180f) / rotationRate).Convert2UnityVector3();
            Debug.Log("Moving Backward");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<MyTransform>().Position.z += 1f * Speed * Time.deltaTime;
            this.GetComponent<MyTransform>().Rotation = (new MyVector3(0f, 0f, 90f) / rotationRate).Convert2UnityVector3();
            Debug.Log("Moving Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<MyTransform>().Position.z += -1f * Speed * Time.deltaTime;
            this.GetComponent<MyTransform>().Rotation = (new MyVector3(0f, 0f, -90f) / rotationRate).Convert2UnityVector3();
            //This doesn't work properly yet but it's gonna be something along these lines.
            Debug.Log("Moving Right");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            this.GetComponent<MyTransform>().Position.y += jumpForce * Time.deltaTime;
            isJumping = true;
            Debug.Log("Jump input pressed");
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isGrounded == false)
        {
            this.GetComponent<MyTransform>().Position.y += -jumpForce * Time.deltaTime;
            isJumping = false;
            Debug.Log("Jump input released");
        }
        else
        {
            Debug.Log("None of the inputs are being called");
        }
    }

}