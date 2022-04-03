using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GamesStateId> OnGameStateChange;
    private IGameState _state;

    private int _targetsCounter = 10;

    private void Awake() => Instance = this;
    private void Start() => SetState(new BeforeGameStartState());

    public void SetState(IGameState newState)
    {
        StopAllCoroutines();
        OnGameStateChange?.Invoke(newState.StateId);
        _state = newState;
        StartCoroutine(_state.Start(this));
    }

    public static void Next() => Instance._state.Next(Instance);
    public static void HitTarget() => Instance._state.HitTarget(Instance);
    public static void LoseGame() => Instance._state.Lose(Instance);
    public static void Restart() => Instance._state.Restart(Instance);
    public static void StartGame() => Instance._state.StartGame(Instance);
}