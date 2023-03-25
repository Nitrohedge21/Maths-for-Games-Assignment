using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    protected Transform orientation;
    protected MyTransform player;
    protected Rigidbody rb;

    public float rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyVector3 viewDirection = MyVector3.Convert2MyVector3(player.Position - new MyVector3(transform.position.x, player.Position.x, transform.position.z).Convert2UnityVector3());
        orientation.forward = viewDirection.NormalizeVector().Convert2UnityVector3();

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        MyVector3 inputDirection = MyVector3.Convert2MyVector3(orientation.forward * verticalInput + orientation.right * horizontalInput);

        if(inputDirection != new MyVector3(0f,0f,0f))
        {
            //this is forward vector of the object
            //player.Position.z = Vector3.Slerp(player.Position.z, inputDirection.NormalizeVector().Convert2UnityVector3(), Time.deltaTime * rotationSpeed);
        }
    
    }
}
