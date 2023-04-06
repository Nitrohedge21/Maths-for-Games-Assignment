using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [Header("Camera")]
    public Movement CurrentCharacter;
    public List<Movement> Characters;
    public int CharacterIndex;
    private Transform Camera;
    private MyVector3 camOffset = new MyVector3(-7f, 2f, 0f);
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
        Follow();
        Camera.transform.position = new MyVector3(CurrentCharacter.GetComponent<MyTransform>().Position).Convert2UnityVector3() + camOffset.Convert2UnityVector3();
        //Camera.transform.rotation = new Quat(new MyVector3(0, 90, 0)).Convert2UnityQuat();
        //Make a matrix, plug in the rotation and position, get the current character's matrix M and multiply them together
        
        if (Input.GetKeyDown(KeyCode.Q))
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
        //Debug.Log("The current character is " + CurrentCharacter.name);

    }

    void Follow()
    {
        MyVector3 followDistance = new MyVector3(3.0f, 0f, 0f);
        //Required Stuff
        MyVector3 follow0 = MyVector3.Vect3Lerp(new MyVector3(Characters[1].GetComponent<MyTransform>().Position), new MyVector3(Characters[0].GetComponent<MyTransform>().Position) - followDistance, Characters[1].GetComponent<Movement>().Speed / 1.5f * Time.deltaTime);
        MyVector3 follow1 = MyVector3.Vect3Lerp(new MyVector3(Characters[0].GetComponent<MyTransform>().Position), new MyVector3(Characters[1].GetComponent<MyTransform>().Position) - followDistance, Characters[0].GetComponent<Movement>().Speed / 1.5f * Time.deltaTime);

        if (CurrentCharacter == Characters[0])
        {
            Characters[1].GetComponent<MyTransform>().Position = follow0.Convert2UnityVector3();    //Might need to get the lerping delayed or something
            Characters[1].GetComponent<CapsuleCollider>().center = Characters[1].GetComponent<MyTransform>().Position;
        }
        else if (CurrentCharacter == Characters[1])
        {
            Characters[0].GetComponent<MyTransform>().Position = follow1.Convert2UnityVector3();
            Characters[0].GetComponent<CapsuleCollider>().center = Characters[0].GetComponent<MyTransform>().Position;
        }

    }
}
