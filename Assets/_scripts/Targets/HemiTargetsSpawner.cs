using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HemiTargetsSpawner : MonoBehaviour, ITargetsSpawner
{
    [SerializeField] private Target _targetPrefab;
    [SerializeField] private float _targetDistance;
    [SerializeField] private float _spreadAngle = 180;
    [SerializeField] private float _spreadElevationAngle = 80;
    [SerializeField] private float _targetBaseElevation = 1.8f;
    [SerializeField] private bool _randomizeElevation = false;
    [SerializeField] private Vector3 _basePosition;
    [SerializeField] private Vector3 _baseRotation;
    [SerializeField] private float _spawnDelay = .1f;
    private int _targetsCount;
    private List<Target> _targets = new List<Target>();

    public void Init(List<Target> targets, int targetsCount)
    {
        foreach (var target in targets) if (target != null) Destroy(target.gameObject);
        targets.Clear();
        _targets = targets;
        _targetsCount = targetsCount;
        StartCoroutine(SpawnTargets());
    }

    private IEnumerator SpawnTargets()
    {
        var spread = Mathf.Approximately(360f, _spreadAngle) ? (_spreadAngle - _spreadAngle / _targetsCount) : _spreadAngle;
        var angleBetweenTargets = new Vector3(0, spread, 0) / (_targetsCount - 1);
        var startAngle = new Vector3(0, -spread / 2, 0);
        var elevation = new Vector3(0, 0, 0);
        for (int i = 0; i < _targetsCount; i++)
        {
            elevation.x = _randomizeElevation ? UnityEngine.Random.Range(0, _spreadElevationAngle) : 0;
            var rotation = (_baseRotation + startAngle) + angleBetweenTargets * i + elevation;
            var pos = _basePosition + Quaternion.Euler(rotation) * Vector3.forward * _targetDistance + Vector3.up * _targetBaseElevation;
            var target = Instantiate(_targetPrefab, pos, Quaternion.Euler(rotation));
            _targets.Add(target);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }


}