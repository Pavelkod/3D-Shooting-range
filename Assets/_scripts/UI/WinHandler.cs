using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private float _resultsShowingTimeout = .3f;
    [SerializeField] private RecordLine _recordLinePrefab;
    [SerializeField] private Transform _recordsParent;
    [SerializeField] private TMP_Text _yourResultText;
    private void OnEnable()
    {
        RecordsRepo.Instance?.Save();
        StartCoroutine(ShowResults());
    }

    private void OnDisable()
    {
        foreach (Transform item in _recordsParent)
        {
            Destroy(item.gameObject);
        }
    }


    private IEnumerator ShowResults()
    {
        if (RecordsRepo.Instance == null) yield break;
        var results = RecordsRepo.Instance.Get();
        _yourResultText.text = "Your result: " + results.LastResult.TimeToString(true);
        foreach (var time in results.RecordsList)
        {
            var line = Instantiate(_recordLinePrefab, _recordsParent);
            line.Init(time, results.PlayerName, results.LastResult == time);
            yield return new WaitForSeconds(_resultsShowingTimeout);
        }
    }

}
