using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public AABB collision;
    private MyVector3 Offset = new MyVector3(1f, 1f, 1f);

    void Start()
    {
        Offset = MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Scale / 5);
        collision = new AABB(MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) - Offset, MyVector3.Convert2MyVector3(this.GetComponent<MyTransform>().Position) + Offset);
    }
    private void FixedUpdate()
    {
        //This line is not really necessary but it's for my own sake.
        this.GetComponent<SphereCollider>().center = this.GetComponent<MyTransform>().Position;
    }
}
