using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject pause_menu;

    // Start is called before the first frame update
    void Start()
    {
        pause_menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause_menu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    #region PauseMenu
    public void Resume()
    {
        pause_menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        
    }
    #endregion
}