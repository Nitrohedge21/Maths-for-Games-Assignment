using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [Header("Camera")]
    public Movement CurrentCharacter;
    public List<Movement> Characters;
    public int CharacterIndex;
    private Transform Camera;
    private MyVector3 camOffset = new MyVector3(-7f, 1f, 0f);
    void Start()
    {
        if(CurrentCharacter == null && Characters.Count >= 1)
        {
            CurrentCharacter = Characters[0];
            CurrentCharacter.isControllable = true;
            Characters[1].isControllable = false;
        }
        Camera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        Camera.transform.position = new MyVector3(CurrentCharacter.GetComponent<MyTransform>().Position).Convert2UnityVector3() + camOffset.Convert2UnityVector3();
        //Camera.transform.rotation = new Quat(new MyVector3(0, 90, 0)).Convert2UnityQuat();
        //Make a matrix, plug in the rotation and position, get the current character's matrix M and multiply them together

        if (Input.GetKeyDown(KeyCode.R))
        {
            Switch();
            Camera.transform.position = new MyVector3(CurrentCharacter.GetComponent<MyTransform>().Position).Convert2UnityVector3() + camOffset.Convert2UnityVector3();
        }
            
    }
    void Switch()
    {

        CurrentCharacter.isControllable = false;
        CharacterIndex++;
        if (CharacterIndex == Characters.Count)
        {
            CharacterIndex = 0;
        }
        CurrentCharacter = Characters[CharacterIndex];
        CurrentCharacter.isControllable = true;
        Debug.Log("The current character is " + CurrentCharacter.name);

    }

}