using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_TutorialScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialText;
    [SerializeField] private Transform box;
    [SerializeField] private CanvasGroup background;

    private float keyCDMax = 2f;
    private bool keyCDActive = false;
    private float keyCD = 0;

    private void OnEnable()
    {
        tutorialText[0].SetActive(true);

        Debug.Log("opening");
        background.alpha = 0;
        //background.LeanAlpha(1, 0.5f);

        box.localPosition = new Vector2(-420, -473);
        //box.LeanMoveLocalY(-236.25f, 2f).setEaseOutExpo().setOnComplete(StopTime).delay = 0.1f;
        Debug.Log("done");
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    public void Update()
    {
        if (keyCDActive)
        {
            keyCD += 0.1f;
            if (keyCD >= keyCDMax)
            {
                keyCDActive = false;
            }
        }
        else
        {
            keyCD = 0;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (tutorialText[0].activeInHierarchy && keyCDActive == false)
            {
                tutorialText[0].SetActive(false);
                keyCDActive = true;
                tutorialText[1].SetActive(true);
            }
            else if (tutorialText[1].activeInHierarchy == true && keyCDActive == false)
            {
                tutorialText[1].SetActive(false);
                keyCDActive = true;
                tutorialText[2].SetActive(true);
            }
            else if (tutorialText[2].activeInHierarchy == true && keyCDActive == false)
            {
                CloseTutorial();
            }
        }
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1;
        Debug.Log("step1");
        //background.LeanAlpha(0, 0.5f);
        Debug.Log("step2");
        //box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInOutExpo().setOnComplete(OnComplete);
    }

    public void OnComplete()
    {

        gameObject.SetActive(false);
    }
}
