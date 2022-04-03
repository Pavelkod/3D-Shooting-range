using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private bool _isTimerStarted = false;
    private float _currentTime = 0f;
    private float _timeOut = 10f;
    private PlayerRecords _records;
    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameState;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameState;
    }

    private void Start()
    {
        _records = RecordsRepo.Instance.Get();
        _timeOut = SettingsRepo.Instance.Get().TimePerTarget;
    }


    private void OnGameState(GamesStateId stateId)
    {
        if (GamesStateId.StartGame == stateId) _records.LastResult = 0;
        if (GamesStateId.TargetShoot == stateId) StartTimer();
        if (GamesStateId.NextTarget == stateId) StopTimer();
    }

    private void StopTimer()
    {
        StopAllCoroutines();
        _isTimerStarted = false;
        _records.LastResult += _timeOut - _currentTime;
    }

    private void StartTimer()
    {
        _currentTime = _timeOut;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        _isTimerStarted = true;
        while (_currentTime > 0)
        {
            _timerText.SetText(_currentTime.TimeToString());
            _currentTime -= Time.deltaTime;
            yield return null;
        }
        if (!_isTimerStarted) yield break;
        GameManager.LoseGame();
        StopTimer();
    }

}
