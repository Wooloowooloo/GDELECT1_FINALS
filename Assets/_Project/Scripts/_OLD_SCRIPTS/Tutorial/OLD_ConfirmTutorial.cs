using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OLD_ConfirmTutorial : MonoBehaviour
{
    public Image FadeImage;
    public float FadeTime;

    public float MinLoadTime;

    public GameObject tutorialStart, nextDialogue;
    public OLD_DialogueManager dM;

    // Start is called before the first frame update
    void Start()
    {
        tutorialStart.SetActive(false);
        nextDialogue.SetActive(false);
        FindObjectOfType<OLD_DialogueManager>();
    }

    private void Update()
    {
        if (dM.sentences.Count == 3)
        {
            tutorialStart.SetActive(true);            
        }
        else
        {
            tutorialStart.SetActive(false);
        }
    }

    public void SkipTutorial()
    {
        StopAllCoroutines();

        Time.timeScale = 1.0f;

        nextDialogue.SetActive(true);

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3);

        FadeImage.gameObject.SetActive(true);
        FadeImage.canvasRenderer.SetAlpha(0);

        while (!Fade(1))
            yield return null;

        Debug.Log("teleporting");
        AsyncOperation op = SceneManager.LoadSceneAsync("Level001");
        float elapsedLoadTime = 0f;
        //SceneManager.LoadScene(targetLevel);

        while (!Fade(0))
            yield return null;

        while (!op.isDone)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        while (elapsedLoadTime < MinLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }
    }

    private bool Fade(float target)
    {
        FadeImage.CrossFadeAlpha(target, FadeTime, true);

        if (Mathf.Abs(FadeImage.canvasRenderer.GetAlpha() - target) <= 0.05f)
        {
            FadeImage.canvasRenderer.SetAlpha(target);
            return true;
        }
        return false;
    }
}