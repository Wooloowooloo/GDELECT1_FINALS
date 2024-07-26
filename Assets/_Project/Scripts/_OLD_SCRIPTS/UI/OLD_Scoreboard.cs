using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OLD_Scoreboard : MonoBehaviour
{
    [SerializeField] private GameObject scoreWindow;
    [SerializeField] private GameObject[] scorePage;
    [SerializeField] private OLD_ScoreSystem ScoreData;
    [SerializeField] private TextMeshProUGUI scoreOut;

    public int pageNumber;

    // Update is called once per frame
    void Update()
    {
        int value = ScoreData.currentScore;

        PageCheck();

        scoreOut.text = value.ToString();
    }

    public void PageCheck()
    {
        if (ScoreData.currentScore <= 100)
        {
            pageNumber = 1;
            scorePage[0].SetActive(true);
            scorePage[1].SetActive(false);
            scorePage[2].SetActive(false);

            Debug.Log("Failure");
        }
        else if (ScoreData.currentScore == 500)
        {
            pageNumber = 3;
            scorePage[0].SetActive(false);
            scorePage[1].SetActive(false);
            scorePage[2].SetActive(true);

            Debug.Log("Fighting!");
        }
        else
        {
            scorePage[0].SetActive(false);
            scorePage[1].SetActive(true);
            scorePage[2].SetActive(false);

            Debug.Log("WOW");
        }
    }
}