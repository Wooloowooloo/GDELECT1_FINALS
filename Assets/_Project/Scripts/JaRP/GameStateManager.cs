using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[DefaultExecutionOrder(-10)]
public class GameStateManager : PersistentSingleton<GameStateManager>
{
    [SerializeField] private float _timePerRound;

    public EGameState CurrentGameState { get; private set; }
    public float CurrentTime { get => _currentTime; private set => _currentTime = value; }
    public float TimePerRound { get => _timePerRound; private set => _timePerRound = value; }
    public int CurrentScore { get => _currentScore; private set => _currentTime = value; }

    private PlayerRifle _rifle;
    private float _currentTime;
    private int _currentScore;

    protected override void Awake()
    {
        base.Awake();

        _rifle = FindObjectOfType<PlayerRifle>();
        _currentTime = _timePerRound;
        _currentScore = 0;

        SetGameState((int)EGameState.PreGameplay);
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
    }

    public void SetGameState(int stateIndex)
    {
        CurrentGameState = (EGameState)stateIndex;
    }

    public void StartNewGame()
    {
        SetGameState((int)EGameState.PreGameplay);
        _rifle.ResetRifleLocation();
        _currentTime = _timePerRound;
        _currentScore = 0;
        //play audio stinger
        // play persisting bgm
    }

    private void EndGame()
    {
        SetGameState((int)EGameState.PostGameplay);
        //play audio stinger
        //play persisting bgm
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
