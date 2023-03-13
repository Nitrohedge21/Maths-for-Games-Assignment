using UnityEngine;

public class MyVector3Lerp : MonoBehaviour
{
    public GameObject lerpObject;
    public GameObject targetObject;
    public float speed = 2.0f;

    void Update()
    {
        MyVector3 position = new MyVector3(lerpObject.transform.position);
        MyVector3 targetPos = new MyVector3(targetObject.transform.position);

        lerpObject.transform.position = MyVector3.Vect3Lerp(position, targetPos, speed * Time.deltaTime).Convert2UnityVector3();
    }
}
