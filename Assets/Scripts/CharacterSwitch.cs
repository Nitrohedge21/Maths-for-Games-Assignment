
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [Header("Camera")]
    public Transform CurrentCharacter;
    public List<Transform> Characters;
    public int CharacterIndex;
    //private Transform Camera;

    void Start()
    {
        if(CurrentCharacter == null && Characters.Count >= 1)
        {
            CurrentCharacter = Characters[0];
        }
    }

    void Update()
    {
        Switch();
    }
    //This script isn't done yet so it's not gonna work yet.
    void Switch()
    {
        CurrentCharacter = Characters[CharacterIndex];
        if(CurrentCharacter.name == "Sonic")
        {
            CurrentCharacter.GetComponent<Sonic>().enabled = true;
            //CurrentCharacter.GetComponent<Tails>().enabled = false;
            Debug.Log("The current character is Sonic");
        }
        else if(CurrentCharacter.name == "Tails")
        {
            CurrentCharacter.GetComponent<Tails>().enabled = true;
            //CurrentCharacter.GetComponent<Sonic>().enabled = false;
            Debug.Log("The current character is Tails");
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(CharacterIndex == 0)
            {
                CharacterIndex = Characters.Count - 1;
            }
            else
            {
                CharacterIndex -= 1; 
            }
            //Camera = Character1.transform;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (CharacterIndex == Characters.Count - 1)
            {
                CharacterIndex = 0;
            }
            else
            {
                CharacterIndex += 1;
            }

            //Camera = Character2.transform;
        }
        //Camera.position = new Vector3(Camera.position.x, Camera.position.y, Camera.position.z);
    }

}
