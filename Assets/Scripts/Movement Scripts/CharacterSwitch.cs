using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [Header("Camera")]
    public Movement CurrentCharacter;
    public List<Movement> Characters;
    public int CharacterIndex;
    private Transform Camera;
    private MyVector3 camOffset = new MyVector3(0f, 2f, -7f);   //This isn't really a good idea and might be fixed if needed
    bool canSwitch = true;
    float stoppingDistance = 1.75f;
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
        
        if (Input.GetKeyDown(KeyCode.Q) && canSwitch)
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

    //IEnumerator switchTimer()
    //{
    //    if (!canSwitch)
    //    {
    //        yield return new WaitForSecondsRealtime(5);
    //        canSwitch = true;
    //    }
    //}

    void Follow()
    {
        //Required Stuff
        MyVector3 position0 = new MyVector3(Characters[0].GetComponent<MyTransform>().Position);
        MyVector3 position1 = new MyVector3(Characters[1].GetComponent<MyTransform>().Position);
        MyVector3 follow0 = MyVector3.Vect3Lerp(position1, position0, Characters[1].GetComponent<Movement>().Speed / 1.5f * Time.deltaTime);
        MyVector3 follow1 = MyVector3.Vect3Lerp(position0, position1, Characters[0].GetComponent<Movement>().Speed / 1.5f * Time.deltaTime);
        if (CurrentCharacter == Characters[0] && MyVector3.Distance(position1, position0) > stoppingDistance)
        {
            Characters[1].GetComponent<MyTransform>().Position = follow0.Convert2UnityVector3();    //Might need to get the lerping delayed or something
            Characters[1].GetComponent<CapsuleCollider>().center = Characters[1].GetComponent<MyTransform>().Position;
        }
        else if (CurrentCharacter == Characters[1] && MyVector3.Distance(position0, position1) > stoppingDistance)
        {
            Characters[0].GetComponent<MyTransform>().Position = follow1.Convert2UnityVector3();
            Characters[0].GetComponent<CapsuleCollider>().center = Characters[0].GetComponent<MyTransform>().Position;
        }

    }
}
