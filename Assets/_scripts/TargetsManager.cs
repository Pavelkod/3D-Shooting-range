using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TargetsManager : MonoBehaviour
{
    [SerializeField] private int _hardcoreAfter = 5;
    public static TargetsManager Instance;
    private int _targetsCount = 10;
    internal List<Vector3> GetPath() => _pathPoints;

    private ITargetsSpawner _targetSpawner;
    private ITargetSelector _targetSelector;
    private ITargetPathBuilder _pathBuilder;

    public int TargetsCount => _targets.Count;
    private Target _currentTarget;
    private int _currentTargetIndex = -1;

    private List<Vector3> _pathPoints = new List<Vector3>();

    private List<Target> _targets = new List<Target>();

    public void RemoveTarget(Target target)
    {
        _pathPoints.Clear();
        _pathPoints.Add(target.transform.position);
        _targets.Remove(target);
        HardcoreMode();             //start moving targets inside this method
        var oldIndex = _currentTargetIndex;
        GetNextTarget();
        _pathPoints = _pathBuilder.GetPath(_targets, oldIndex, _currentTargetIndex);
    }

    private void HardcoreMode()
    {
        if (_targets.Count == _hardcoreAfter)
            foreach (var target in _targets)
            {
                target.ActivateMovement(true);
            }
    }

    private void Awake()
    {
        Instance = this;
        _targetSelector = GetComponent<ITargetSelector>();
        _targetSpawner = GetComponent<ITargetsSpawner>();
        _pathBuilder = GetComponent<ITargetPathBuilder>();
        GameManager.OnGameStateChange += OnGameState;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameState;
    }

    private void OnGameState(GamesStateId stateId)
    {
        if (stateId == GamesStateId.PrepareGame)
        {
            _targetsCount = SettingsRepo.Instance.Get().TargetsCount;
            Reset();
        }
    }

    private void Reset()
    {
        _pathPoints.Clear();
        _currentTarget = null;
        _currentTargetIndex = -1;
        _targetSpawner.Init(_targets, _targetsCount);
    }

    private void GetNextTarget()
    {
        _currentTarget = _targetSelector.GetTarget(_targets);
        if (_currentTarget != null)
        {
            _currentTarget.Select();
            _currentTargetIndex = _targets.IndexOf(_currentTarget);
        }
    }


    public Target GetCurrentTarget()
    {
        if (_currentTarget == null)
        {
            GetNextTarget();
            _pathBuilder.Init(_currentTarget.transform.position);
        }
        return _currentTarget;
    }

}

