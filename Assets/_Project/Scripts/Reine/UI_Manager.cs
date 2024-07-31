using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public int Score { get; private set; }

    [SerializeField] GameObject pause_menu;
    [SerializeField] string scene_name;

    [Header("UI Counters")]
    [SerializeField] private TextMeshProUGUI[] _ammoCounter;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _scoreCounter;

    [Header("Game Configs")]
    [SerializeField] private float _timeInRound;

    private PlayerRifle _rifle;
    private float _currentTime;

    private void Awake()
    {
        _rifle = FindFirstObjectByType<PlayerRifle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pause_menu.SetActive(false);
        _currentTime = _timeInRound;
        HandleScoreCounter(0);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    pause_menu.SetActive(true);
        //    Time.timeScale = 0;
        //}
        HandleAmmoCounter();
        HandleTimer();
    }

    public void HandleAmmoCounter()
    {
        _ammoCounter[0].text = $"{_rifle.CurrentAmmo}";
        _ammoCounter[1].text = $"{_rifle.MaxAmmo}";
    }

    public void HandleScoreCounter(int scoreValue)
    {
        Score += scoreValue;
        _scoreCounter.text = $"{Score: 000}";
    }

    public void HandleTimer()
    {
        _currentTime -= Time.deltaTime;
        _timer.text = $"{Mathf.FloorToInt(_currentTime / 60): 0}:{Mathf.FloorToInt(_currentTime % 60):00}";
    }

    #region PauseMenu
    public void Resume()
    {
        pause_menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(scene_name);   
    }
    #endregion


}