using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanishscript : MonoBehaviour
{
    public GameObject target;

    public float interval = 1f;

    IEnumerator Start() {
         if(target == gameObject)
             Debug.LogError("ActiveAlternator cannot target itself and still re-activate");

        while(true) {
            yield return new WaitForSeconds(interval);
            target.SetActive(!target.activeSelf);
        }

    }
}
