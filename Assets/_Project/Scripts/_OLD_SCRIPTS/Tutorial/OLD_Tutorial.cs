using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OLD_Tutorial : MonoBehaviour
{
    public Image fadeImage;
    public float loadTime, minLoadTime;

    public GameObject tutorial, endDialogue;

    public OLD_DialogueManager manager;

    void Start()
    {
        tutorial.SetActive(false);

        FindObjectOfType<OLD_DialogueManager>();
    }

    private void Update()
    {
        if (manager.sentences.Count == 4)
        {
            tutorial.SetActive(true);
        }
    }

    public void MoveOn()
    {
        StopAllCoroutines();

        Time.timeScale = 1.0f;

        endDialogue.SetActive(true);

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3);

        fadeImage.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(0);

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

        while (elapsedLoadTime < minLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }
    }

    private bool Fade(float target)
    {
        fadeImage.CrossFadeAlpha(target, loadTime, true);

        if (Mathf.Abs(fadeImage.canvasRenderer.GetAlpha() - target) <= 0.05f)
        {
            fadeImage.canvasRenderer.SetAlpha(target);
            return true;
        }
        return false;
    }
}