using UnityEngine;

public class Pursuer : MonoBehaviour
{
    GameObject cube;
    GameObject cube2;
    private float speed = 10.0f;
    void Start()
    {
        cube = GameObject.Find("Pursuer");
        cube2 = GameObject.Find("Evader");
    }

    void Update()
    {
        MyVector3 _cubePos = new MyVector3(cube.transform.position);
        MyVector3 _cube2Pos = new MyVector3(cube2.transform.position);
        MyVector3 returnVal = MyVector3.Subtract(_cube2Pos,_cubePos);
        MyVector3 normalizedValue = returnVal.NormalizeVector();
        cube.transform.position = (_cubePos + normalizedValue * speed * Time.deltaTime).Convert2UnityVector3();
            
        // normalize and then multiply by speed * deltatime
        // Follows the target
    }
}
