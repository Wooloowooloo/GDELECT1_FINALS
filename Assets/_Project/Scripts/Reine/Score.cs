using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI display_score;

    public int currentScore;

    void Start()
    {
        currentScore = 0;
    }

    void Update()
    {
        display_score.text = currentScore.ToString();
    }
}
