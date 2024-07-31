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

    private float _currentTime;
    private int _currentScore;

    protected override void Awake()
    {
        base.Awake();

        StartNewGame();
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
            SetGameState(EGameState.PostGameplay);

            //testing to check if time's up
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }
    }

    public void UpdateScore(int scoreValue)
    {
        _currentScore += scoreValue;
        Mathf.Clamp(_currentScore, 0, int.MaxValue);
    }

    public void SetGameState(EGameState targetState)
    {
        CurrentGameState = targetState;
    }

    public void StartNewGame()
    {
        SetGameState(EGameState.Gameplay);
        _currentTime = _timePerRound;
        _currentScore = 0;
        //play audio stinger
        // play persisting bgm
    }

    private void EndGame()
    {
        SetGameState(EGameState.PostGameplay);
        //play audio stinger
        //play persisting bgm
    }
}

public enum EGameState
{
    PreGameplay,
    Gameplay,
    PostGameplay
}
