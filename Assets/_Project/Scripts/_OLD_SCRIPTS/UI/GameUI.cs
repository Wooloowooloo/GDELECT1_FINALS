using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen, scoreCanvas;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private StrictCameraTest player;
    [SerializeField] private Scoreboard scBoard;
    [SerializeField] private Image reticle;
    //[SerializeField] private FirstPersonController player;

    [SerializeField] private Gun pew;

    public void Start()
    {
        scoreCanvas.SetActive(false);
        //tutorialScreen.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && tutorialScreen.activeInHierarchy == false)
        {
            Pause();
        }

        if (pauseScreen.activeInHierarchy == false && tutorialScreen.activeInHierarchy == false)
        {
            Time.timeScale = 1;
            //player.cameraCanMove = true;
            player.cursorLocked = true;
        }
        
        if (pauseScreen.activeInHierarchy == true || tutorialScreen.activeInHierarchy == true)
        {
            //player.cameraCanMove = false;
            player.cursorLocked = false;
        }

        if(pew.currentTotalBullets == 0)
        {
            reticle.enabled = false;
            Time.timeScale = 0;
            EndGame();
        }
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void EndGame()
    {
        scoreCanvas.SetActive(true);
        scBoard.PageCheck(); 
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
