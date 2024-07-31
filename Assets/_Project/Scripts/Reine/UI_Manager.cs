using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[DefaultExecutionOrder(0)]
public class UI_Manager : PersistentSingleton<UI_Manager>
{
    [SerializeField] GameObject pause_menu;
    [SerializeField] string scene_name;

    [Header("UI Counters")]
    [SerializeField] private TextMeshProUGUI[] _ammoCounter;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _scoreCounter;

    [Header("Game Over Screen")]
    [SerializeField] private GameObject _gameOverScreen;

    private GameStateManager _gameStateManager;
    private PlayerRifle _rifle;

    protected override void Awake()
    {
        base.Awake();

        _gameStateManager = GameStateManager.Instance;
        _rifle = FindFirstObjectByType<PlayerRifle>();
    }

    void Start()
    {
        pause_menu.SetActive(false);
        _gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (_gameStateManager.CurrentGameState == EGameState.Gameplay)
        {
            HandleAmmoCounter();
            HandleScoreCounter();
            HandleTimer();
        }

        if (_gameStateManager.CurrentGameState == EGameState.PostGameplay)
        {
            ShowGameOverScreen();
        }
    }

    public void HandleAmmoCounter()
    {
        _ammoCounter[0].text = $"{_rifle.CurrentAmmo}";
        _ammoCounter[1].text = $"{_rifle.MaxAmmo}";
    }

    private void HandleScoreCounter()
    {
        int score = _gameStateManager.CurrentScore;
        _scoreCounter.text = $"{score}";
    }

    private void HandleTimer()
    {
        float time = _gameStateManager.CurrentTime;
        _timer.text = $"{Mathf.FloorToInt(time / 60): 0}:{Mathf.FloorToInt(time % 60):00}";
    }

    private void ShowGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
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