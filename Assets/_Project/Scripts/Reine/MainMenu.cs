using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string scene_name;
    [SerializeField] AudioSource _clickSFX;

    public void PlayGame()
    {
        SceneManager.LoadScene(scene_name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnClick()
    {
        _clickSFX.Play();
    }
}