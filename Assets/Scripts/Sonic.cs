using UnityEngine;

#pragma warning disable CS0108 //This is to suppress the warning message on the update line.

public class Sonic : Movement
{
    protected override void Start()
    {
        Speed = 6.0f;
        base.Start();
    }
    protected virtual void Update()
    {
        base.Update();
    }
    protected virtual void FixedUpdate()
    {
        base.FixedUpdate();  //This line is needed to be able to get the update from the base/parent class so that I don't have to copy and paste the whole code.
        //Debug.Log("Sonic update test");
    }
}
