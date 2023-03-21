using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    public AABB collision;
    protected MyVector3 Offset = new MyVector3(1f, 1f, 1f);

    void Start()
    {
        Offset = MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Scale) / 2;
        collision = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
