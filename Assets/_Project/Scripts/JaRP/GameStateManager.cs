using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[DefaultExecutionOrder(-10)]
public class GameStateManager : PersistentSingleton<GameStateManager>
{
    [SerializeField] private float _timePerRound;
    [SerializeField] private Tutorial _tutorial;

    public EGameState CurrentGameState { get; private set; }
    public float CurrentTime { get => _currentTime; private set => _currentTime = value; }
    public float TimePerRound { get => _timePerRound; private set => _timePerRound = value; }
    public int CurrentScore { get => _currentScore; private set => _currentTime = value; }
    public int HighScore { get => _highScore; private set => _highScore = value; }

    private AudioManager _audioManager;
    private UI_Manager _uiManager;
    private PlayerRifle _rifle;
    private float _currentTime;
    private int _currentScore;
    private int _highScore;

    protected override void Awake()
    {
        base.Awake();

        _audioManager = AudioManager.Instance;
        _uiManager = UI_Manager.Instance;
        _rifle = FindObjectOfType<PlayerRifle>();
        _currentTime = _timePerRound;
        _currentScore = 0;

        SetGameState((int)EGameState.PreGameplay);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("High Score"))
        {
            _highScore = PlayerPrefs.GetInt("High Score");
        }

        _audioManager.PlayMusic(EMusicType.NonGameplay);
    }

    private void Update()
    {
        if (CurrentGameState == EGameState.Gameplay)
        {
            HandleTime();
        }
    }

    private void HandleTime()
    {
        _currentTime -= Time.deltaTime;
        Mathf.Clamp(_currentTime, 0, TimePerRound);

        if (_currentTime <= 0f)
        {
            EndGame();
        }
    }

    public void UpdateScore(int scoreValue)
    {
        _currentScore += scoreValue;
        Mathf.Clamp(_currentScore, 0, int.MaxValue);

        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetInt("High Score", _highScore);
            _uiManager.HandleHighScoreText();
        }
    }

    private void SetGameState(int stateIndex)
    {
        CurrentGameState = (EGameState)stateIndex;
    }

    public void StartNewGame()
    {
        SetGameState((int)EGameState.PreGameplay);
        _rifle.ResetRifleLocation();
        _currentTime = _timePerRound;
        _currentScore = 0;
        _tutorial.ResetTutorialPages();
        _audioManager.PlayMusic(EMusicType.NonGameplay);
    }

    public void PlayRound()
    {
        SetGameState((int)EGameState.Gameplay);
        _audioManager.PlayMusic(EMusicType.Gameplay);
    }

    private void EndGame()
    {
        PlayerPrefs.Save();
        SetGameState((int)EGameState.PostGameplay);
        _audioManager.PlayMusic(EMusicType.NonGameplay);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

public enum EGameState
{
    PreGameplay,
    Gameplay,
    PostGameplay
}
