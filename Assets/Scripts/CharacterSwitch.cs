using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] GameObject Character1;
    [SerializeField] GameObject Character2;

    [Header("Camera")]
    [SerializeField] Transform Transform1;
    [SerializeField] Transform Transform2;
    private Transform Camera;
    //This part might not be necessary because I could use the gameobjects instead.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CameraSwitch()
    {
        if (this == Character1)
        {
            Camera = Character1.transform;
        }
        else if (this == Character2)
        {
            Camera = Character2.transform;
        }
        Camera.position = new Vector3(Camera.position.x, Camera.position.y, Camera.position.z);
    }
}
