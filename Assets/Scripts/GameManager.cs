using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject canvas;
    private int totalOrbs = 5;
    void Start()
    {
        canvas = GameObject.Find("Canvas (UI)");
    }

    void Update()
    {
        RestartGame();
        isGameOver();
    }

    void RestartGame()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    bool isGameOver()
    {
        //If the goldAmount reaches 68, activates the victory screen image and ends the game.
        if (IncreaseValue.orbAmount == totalOrbs)
        {
            canvas.transform.GetChild(1).gameObject.SetActive(true);

            //The part from here till the end of foreach was suggested by ChatGPT.
            GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
            Movement[] movement = new Movement[characters.Length];
            for(int i = 0; i < characters.Length; i++)
            {
                movement[i] = characters[i].GetComponent<Movement>();
            }
            foreach (Movement component in movement)
            {
                component.isControllable = false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
