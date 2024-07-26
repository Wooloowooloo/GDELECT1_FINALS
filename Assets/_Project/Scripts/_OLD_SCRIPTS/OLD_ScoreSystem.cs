using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OLD_ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;

    [HideInInspector] public int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = currentScore.ToString();
    }
}
