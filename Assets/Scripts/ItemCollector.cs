using UnityEngine;
using UnityEngine.UI;
public class ItemCollector : MonoBehaviour
{
    public Text scoreText;

    void Start() 
    {
        IncreaseValue.orbAmount = 0;
        scoreText.text = "Orb Count : " + IncreaseValue.orbAmount;
    }

    private void FixedUpdate()
    {
        //If the collided object is a collectable, destroy the collectable object.
        GameObject orb = GameObject.FindGameObjectWithTag("Collectables");
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //This line is not working right because it's only able to target one character at a time. And the orb gets collected twice for some fucking reason IDK why.
        AABB orbCollision = orb.GetComponent<ObjectCollision>().collision;  //This line causes error spam after there are no orbs left in the scene. Might need to change it up a bit.
        AABB playerCollision = player.GetComponent<Movement>().playerCollision;
        if (AABB.Intersects(playerCollision, orbCollision))
        {
            IncreaseValue.orbAmount++;
            Destroy(orb);
            scoreText.text = "Orb Count : " + IncreaseValue.orbAmount;
            Debug.Log("Current orb amount: " + IncreaseValue.orbAmount);
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
