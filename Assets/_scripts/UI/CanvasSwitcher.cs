using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField] GameObject _hudCanvas;
    [SerializeField] GameObject _winCanvas;
    [SerializeField] GameObject _loseCanvas;
    [SerializeField] GameObject _menuCanvas;
    private bool _isGameMenuOpened = false;
    public static CanvasSwitcher Instance;
    private void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChange += OnGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameState;
    }

    private void OnGameState(GamesStateId stateId)
    {
        _hudCanvas.SetActive(!(GamesStateId.Lose == stateId || GamesStateId.Win == stateId));
        _winCanvas.SetActive(GamesStateId.Win == stateId);
        _loseCanvas.SetActive(GamesStateId.Lose == stateId);
    }

    public void UnpauseTime() => Time.timeScale = 1;




    public void GameMenu()
    {
        _isGameMenuOpened = !_isGameMenuOpened;
        Utils.LockCursor(!_isGameMenuOpened);
        Utils.EnableInput(!_isGameMenuOpened);
        _menuCanvas.SetActive(_isGameMenuOpened);
        Time.timeScale = _isGameMenuOpened ? 0 : 1;

    }
}
