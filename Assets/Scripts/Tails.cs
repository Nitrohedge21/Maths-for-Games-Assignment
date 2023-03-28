using UnityEngine;

#pragma warning disable CS0108 //This is to suppress the warning message on the update line.

public class Tails : Movement
{
    protected override void Start()
    {
        Speed = 4.0f;
        base.Start();
    }
    protected virtual void Update()
    {
        base.Update();
    }
    protected virtual void FixedUpdate()
    {
        base.FixedUpdate();  //This line is needed to be able to get the update from the base/parent class so that I don't have to copy and paste the whole code.

    }
}
