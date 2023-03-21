using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [Header("Camera")]
    public Movement CurrentCharacter;
    public List<Movement> Characters;
    public int CharacterIndex;
    private Transform Camera;
    void Start()
    {
        if(CurrentCharacter == null && Characters.Count >= 1)
        {
            CurrentCharacter = Characters[0];
        }
        Camera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Switch();
            Camera.transform.position = new MyVector3(CurrentCharacter.GetComponent<MyTransform>().Position).Convert2UnityVector3();
            //Debug.Log("Current Target is " + CurrentCharacter);
        }
            
    }
    void Switch()
    {

        CurrentCharacter.enabled = false;
        CharacterIndex++;
        if (CharacterIndex == Characters.Count)
        {
            CharacterIndex = 0;
        }
        CurrentCharacter = Characters[CharacterIndex];
        CurrentCharacter.enabled = true;
        Debug.Log("The current character is " + CurrentCharacter.name);

    }

}