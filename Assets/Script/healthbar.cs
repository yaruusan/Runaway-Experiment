using UnityEngine;

public class healthbar : MonoBehaviour
{
    public float health;

    public void LoseHealth(int value)
    {
        if (health <= 0)
            return;
        health -= value;
        if (health<=0)
        {
            FindObjectOfType<playermove>().Die();
        }
    }
}
