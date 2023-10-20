using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static int scoreValue = 0;
    TMP_Text score;

    void Start()
    {
        score = GetComponent<TMP_Text>();
    }

    void Update()
    {
        score.text = scoreValue.ToString();
    }
}
