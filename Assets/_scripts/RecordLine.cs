using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordLine : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNameText, _timeText;
    [SerializeField] private Color _defaultColor, _currentColor;

    internal void Init(float time, string playerName, bool currentRecord)
    {
        _playerNameText.text = playerName;
        _timeText.text = time.TimeToString(true);
        _timeText.color = currentRecord ? _currentColor : _defaultColor;
    }
}
