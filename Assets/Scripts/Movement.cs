using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController CharacterController;
    protected Transform Camera;
    protected Rigidbody Rb;
    protected CapsuleCollider Collider;
    [SerializeField] protected float Speed = 6f;
    protected float RotationRate = 10f;
    protected float JumpForce;
    protected MyVector3 Velocity;
    protected bool GroundedPlayer;
    protected float JumpHeight = 1.0f;
    protected float GravityValue = -9.81f;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Rb = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        Camera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        GroundedPlayer = CharacterController.isGrounded;
        if (GroundedPlayer && Velocity.y < 0)
        {
            Velocity.y = 0f;
        }

        MyVector3 Move = new MyVector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));    //These could be switched to GetAxis to make them more slippery.
        //The line above might be the reason why it the objects don't move, this most likely only works with normal vector3s.
        MyVector3 Direction = Move.NormalizeVector();

        CharacterController.Move(Direction.Convert2UnityVector3() * Time.deltaTime * Speed);

        if (Direction != new MyVector3(0,0,0))
        {
            Quaternion DesiredRotation = Quaternion.LookRotation(Direction.Convert2UnityVector3(), new MyVector3(0, 1, 0).Convert2UnityVector3());
            //Quat DesiredRotation = Quaternion.LookRotation(Direction.Convert2UnityVector3(), Vector3.up); //An attempt on using my own functions, it doesn't work.
            transform.rotation = Quaternion.Slerp(transform.rotation,DesiredRotation, RotationRate * Time.deltaTime);
            //transform.rotation = Quat.SLERP(transform.rotation, DesiredRotation, RotationRate * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && GroundedPlayer)
        {
            Velocity.y += Mathf.Sqrt(JumpHeight * -3.0f * GravityValue);
        }

        Velocity.y += GravityValue * Time.deltaTime;
        CharacterController.Move(Velocity.Convert2UnityVector3() * Time.deltaTime);
    }

}