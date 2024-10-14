using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class trapobject : MonoBehaviour
{
    int decayAmount=10;
    
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            Debug.Log($"{name} Triggered");
            FindObjectOfType<healthbar>().LoseHealth(decayAmount);
        }
    }
}

