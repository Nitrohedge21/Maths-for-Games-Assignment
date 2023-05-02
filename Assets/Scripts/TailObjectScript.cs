using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailObjectScript : MonoBehaviour
{
    GameObject tail1;
    GameObject tail2;
    GameObject parentObject;
    void Start()
    {
        parentObject = GameObject.Find("Tails");
        tail1 = GameObject.Find("Left Tail");
        tail2 = GameObject.Find("Right Tail");
    }

    // Update is called once per frame
    void Update()
    {
        tail1.transform.position = parentObject.GetComponent<MyTransform>().Position + new MyVector3(0.3f, 25.6f, -2.6f).Convert2UnityVector3();
        tail2.transform.position = parentObject.GetComponent<MyTransform>().Position + new MyVector3(-0.3f, 25.6f, -2.6f).Convert2UnityVector3();

        //tail1.transform.rotation = new Quat(MyVector3.Convert2MyVector3(parentObject.GetComponent<MyTransform>().Rotation)).Convert2UnityQuat();
        //tail2.transform.rotation = new Quat(MyVector3.Convert2MyVector3(parentObject.GetComponent<MyTransform>().Rotation)).Convert2UnityQuat();
    }
}
