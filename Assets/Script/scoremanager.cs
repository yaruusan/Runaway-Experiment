using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoremanager : MonoBehaviour
{
    public static scoremanager instance;
    public TextMeshProUGUI text;
    int score;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int pickValue)
    {
        score = score + pickValue;
        text.text = "X" + score.ToString();
    }
}
