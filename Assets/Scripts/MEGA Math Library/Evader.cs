using Unity.VisualScripting;
using UnityEngine;

public class Evader : MonoBehaviour
{
    private float speed = 20.0f;
    GameObject cube2;
    

    void Start()
    {
        cube2 = GameObject.Find("Evader");
    }
    void Update()
    {
        MyVector3 rotation = new MyVector3(cube2.transform.rotation.eulerAngles);
        MyVector3 forward = MathsLibrary.EulerAnglesToDirection(rotation / (180.0f / Mathf.PI));
        MyVector3 backward = MathsLibrary.EulerAnglesToDirection(rotation / (180.0f / Mathf.PI)) * -1;
        MyVector3 right = MathsLibrary.CrossProduct(forward, new MyVector3(0,-1,0));
        MyVector3 left = MathsLibrary.CrossProduct(backward, new MyVector3(0,-1,0)); //This is -1 because the backward direction is multiplied by -1.

        if (Input.GetKey(KeyCode.RightArrow))
        {
            cube2.transform.position += (right * speed * Time.deltaTime).Convert2UnityVector3();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cube2.transform.position += (left * speed * Time.deltaTime).Convert2UnityVector3();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cube2.transform.position += (forward * speed * Time.deltaTime).Convert2UnityVector3();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cube2.transform.position += (backward * speed * Time.deltaTime).Convert2UnityVector3();
        }

        //Rotates the object downwards
        if(Input.GetKey(KeyCode.Q))
        {
            rotation += new MyVector3(10,0,0) * Time.deltaTime;
            cube2.transform.rotation = Quaternion.Euler(rotation.Convert2UnityVector3());
        }
        //Rotates the object upwards
        if (Input.GetKey(KeyCode.E))
        {
            rotation += new MyVector3(-10, 0, 0) * Time.deltaTime;
            cube2.transform.rotation = Quaternion.Euler(rotation.Convert2UnityVector3());
        }

    }
}
