using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Pick")
        {
            Destroy(other.gameObject);
        }
    }
}
