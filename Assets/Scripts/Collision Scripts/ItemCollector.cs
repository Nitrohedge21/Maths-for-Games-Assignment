using UnityEngine;
using UnityEngine.UI;
public class ItemCollector : MonoBehaviour
{
    public Text scoreText;
    void Start() 
    {
        IncreaseValue.orbAmount = 0;
        scoreText.text = "Orbs : " + IncreaseValue.orbAmount;
    }

    private void FixedUpdate()
    {
        // The original 2D project has this script attached to both of the characters.
        // However, that one uses Unity's own functions and such.
        //This one only requires one of them to work properly, the count doubles the amount when the script is on both of them.
        GameObject[] orbs = GameObject.FindGameObjectsWithTag("Collectables");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) 
        {
            AABB playerCollision = player.GetComponent<Movement>().playerCollision;
            foreach (GameObject orb in orbs)
            {
                AABB orbCollision = orb.GetComponent<ObjectCollision>().collision;
                if (AABB.Intersects(playerCollision, orbCollision))
                {
                    IncreaseValue.orbAmount++;
                    Destroy(orb);
                    scoreText.text = "Orbs : " + IncreaseValue.orbAmount;
                    //Debug.Log("Current orb amount: " + IncreaseValue.orbAmount);
                }
            }
        }
        

    }
}

public static class IncreaseValue
{
    private static int _orbAmount;
    public static int orbAmount
    {
        get
        {
            return _orbAmount;
        }
        set
        {
            _orbAmount = value;
        }
    }
}
