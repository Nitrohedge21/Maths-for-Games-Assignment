using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        
    }

    void Update()
    {
        RestartGame();
    }

    void RestartGame()
    {
        if(Input.GetKey(KeyCode.R) && this == player)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
