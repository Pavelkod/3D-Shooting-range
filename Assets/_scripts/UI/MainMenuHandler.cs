using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _targetsCountText;
    [SerializeField] private TMP_Text _timePerTargetText;
    [SerializeField] private Slider _targetsSlider;
    [SerializeField] private Slider _timePerTargetSlider;
    public void StartGame() => SceneManager.LoadScene("Game");

    private void Start()
    {
        _targetsSlider.value = SettingsRepo.Instance.Get().TargetsCount;
        _timePerTargetSlider.value = SettingsRepo.Instance.Get().TimePerTarget;
    }

    public void OnSliderTargetsCountSlider(Single value)
    {
        _targetsCountText.SetText(value.ToString());
        SettingsRepo.Instance.Get().TargetsCount = (int)value;
        SettingsRepo.Instance.Save();
    }

    public void OnSliderTimePerTarget(Single value)
    {
        _timePerTargetText.SetText(value.ToString());
        SettingsRepo.Instance.Get().TimePerTarget = (int)value;
        SettingsRepo.Instance.Save();
    }

    public void Exit() => Application.Quit();
}
