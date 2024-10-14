using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick : MonoBehaviour
{
    public int pickValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            scoremanager.instance.ChangeScore(pickValue);
        }
    }
}
